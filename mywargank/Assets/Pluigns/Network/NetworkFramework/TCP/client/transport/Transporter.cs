using System;
using System.Net.Sockets;

namespace Pomelo.DotNetClient
{
    class StateObject
    {
        public const int BufferSize = 1024;
        internal byte[] buffer = new byte[BufferSize];
    }

    public class Transporter
    {
        private Action<Exception> onSendError;
        private Action<Exception> onReceiveError;
        private Action onDisconnect;
        public const int HeadLength = 4;

        private Socket socket;
        private Action<byte[]> messageProcesser;

        //Used for get message
        private StateObject stateObject = new StateObject();
        private TransportState transportState;
        private IAsyncResult asyncReceive;
        private IAsyncResult asyncSend;
        private byte[] headBuffer = new byte[4];
        private byte[] buffer;
        private int bufferOffset = 0;
        private int pkgLength = 0;

        public Transporter(Socket socket, Action<byte[]> processer)
        {
            this.socket = socket;
            this.messageProcesser = processer;
            transportState = TransportState.readHead;
        }

        public void start(Action onDisconnect, Action<Exception> onSendError, Action<Exception> onReceiveError)
        {
            this.onSendError = onSendError;
            this.onReceiveError = onReceiveError;
            this.onDisconnect = onDisconnect;
            this.receive();
        }

        public void stop()
        {
            this.transportState = TransportState.closed;
        }

        public void send(byte[] buffer)
        {
            try
            {
                if (this.transportState != TransportState.closed)
                {
                    this.asyncSend = socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(sendCallback), socket);
                }
            }
            catch (Exception e)
            {
                this.onSendError.Invoke(e);
            }
        }

        private void sendCallback(IAsyncResult asyncSend)
        {
            try
            {
                if (this.transportState == TransportState.closed)
                    return;
                socket.EndSend(asyncSend);
            }
            catch (Exception e)
            {
                this.onSendError.Invoke(e);
            }
        }

        private void receive()
        {
            try
            {
                this.asyncReceive = socket.BeginReceive(stateObject.buffer, 0, stateObject.buffer.Length, SocketFlags.None, new AsyncCallback(endReceive), stateObject);
            }
            catch (Exception e)
            {
                this.onReceiveError.Invoke(e);
            }
        }



        private void endReceive(IAsyncResult asyncReceive)
        {
            try
            {
                if (this.transportState == TransportState.closed)
                    return;
                StateObject state = (StateObject)asyncReceive.AsyncState;
                int length = this.socket.EndReceive(asyncReceive);

                if (length > 0)
                {
                    processBytes(state.buffer, 0, length);
                    //Receive next message
                    if (this.transportState != TransportState.closed)
                        receive();
                }
                else
                {
                    this.onDisconnect();
                }
            }
            catch (Exception e)
            {
                this.onReceiveError.Invoke(e);
            }
        }

        private void processBytes(byte[] bytes, int offset, int limit)
        {
            if (this.transportState == TransportState.readHead)
            {
                readHead(bytes, offset, limit);
            }
            else if (this.transportState == TransportState.readBody)
            {
                readBody(bytes, offset, limit);
            }
        }

        private bool readHead(byte[] bytes, int offset, int limit)
        {
            int length = limit - offset;
            int headNum = HeadLength - bufferOffset;

            if (length >= headNum)
            {
                //Write head buffer
                writeBytes(bytes, offset, headNum, bufferOffset, headBuffer);
                //Get package length
                pkgLength = (headBuffer[1] << 16) + (headBuffer[2] << 8) + headBuffer[3];

                //Init message buffer
                buffer = new byte[HeadLength + pkgLength];
                writeBytes(headBuffer, 0, HeadLength, buffer);
                offset += headNum;
                bufferOffset = HeadLength;
                this.transportState = TransportState.readBody;

                if (offset <= limit)
                    processBytes(bytes, offset, limit);
                return true;
            }
            else
            {
                writeBytes(bytes, offset, length, bufferOffset, headBuffer);
                bufferOffset += length;
                return false;
            }
        }

        private void readBody(byte[] bytes, int offset, int limit)
        {
            int length = pkgLength + HeadLength - bufferOffset;
            if ((offset + length) <= limit)
            {
                writeBytes(bytes, offset, length, bufferOffset, buffer);
                offset += length;

                //Invoke the protocol api to handle the message
                this.messageProcesser.Invoke(buffer);
                this.bufferOffset = 0;
                this.pkgLength = 0;

                if (this.transportState != TransportState.closed)
                    this.transportState = TransportState.readHead;
                if (offset < limit)
                    processBytes(bytes, offset, limit);
            }
            else
            {
                writeBytes(bytes, offset, limit - offset, bufferOffset, buffer);
                bufferOffset += limit - offset;
                this.transportState = TransportState.readBody;
            }
        }

        private void writeBytes(byte[] source, int start, int length, byte[] target)
        {
            writeBytes(source, start, length, 0, target);
        }

        private void writeBytes(byte[] source, int start, int length, int offset, byte[] target)
        {
            for (int i = 0; i < length; i++)
            {
                target[offset + i] = source[start + i];
            }
        }
    }
}