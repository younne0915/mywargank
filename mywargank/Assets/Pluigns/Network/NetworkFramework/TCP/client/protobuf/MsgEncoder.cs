using System;
using System.Text;
using LitJson;
using System.Collections.Generic;

namespace Pomelo.Protobuf
{
    public class MsgEncoder
    {
        private JsonData protos { set; get; }//The message format(like .proto file)

        private Util util { set; get; }

        public MsgEncoder(JsonData protos)
        {
            if (protos == null) protos = new JsonData();

            this.protos = protos;
            this.util = new Util();
        }

        /// <summary>
        /// Encode the message from server.
        /// </summary>
        /// <param name='route'>
        /// Route.
        /// </param>
        /// <param name='msg'>
        /// Message.
        /// </param>
        public byte[] encode(string route, JsonData msg)
        {
            byte[] returnByte = null;

            if (this.protos.ContainsKey(route))
            {
                JsonData proto = this.protos[route];
                if (!checkMsg(msg, proto))
                {
                    return null;
                }
                int length = Encoder.byteLength(msg.ToJson()) * 2;
                int offset = 0;
                byte[] buff = new byte[length];
                offset = encodeMsg(buff, offset, proto, msg);
                returnByte = new byte[offset];
                for (int i = 0; i < offset; i++)
                {
                    returnByte[i] = buff[i];
                }
            }

            return returnByte;
        }

        /// <summary>
        /// Check the message.
        /// </summary>
        private bool checkMsg(JsonData msg, JsonData proto)
        {
            ICollection<string> protoKeys = proto.Keys;
            foreach (string key in protoKeys)
            {
                JsonData value = proto[key];
                if (value.ContainsKey("option"))
                {
                    JsonData proto_option = value["option"];
                    switch (proto_option.ToJson())
                    {
                        case "required":
                            if (!msg.ContainsKey(key))
                            {
                                return false;
                            }
                            else
                            {

                            }
                            break;
                        case "optional":

                            JsonData messages = proto["__messages"];

                            JsonData value_type = value["type"];

                            if (msg.ContainsKey(key))
                            {
                                bool a = messages.ContainsKey(value_type.ToJson());
                                bool b = protos.ContainsKey("message " + value_type.ToJson());
                                if (a)
                                {
                                    JsonData value_proto = messages[value_type.ToJson()];
                                    checkMsg(msg[key], value_proto);
                                }
                                if (b)
                                {
                                    JsonData value_proto = protos["message " + value_type.ToJson()];
                                    checkMsg(msg[key], value_proto);
                                }
                            }
                            break;

                        case "repeated":
                            if (value.ContainsKey("type") && msg.ContainsKey(key))
                            {
                                JsonData msg_name = msg[key];
                                JsonData value_type2 = value["type"];
                                bool a = proto["__messages"].ContainsKey(value_type2.ToJson());
                                bool b = protos.ContainsKey("message " + value_type2.ToJson());
                                if (a)
                                {
                                    JsonData msg_type = proto["__messages"][value_type2.ToJson()];
                                    foreach (JsonData item in msg_name)
                                    {
                                        if (!checkMsg(item, msg_type))
                                        {
                                            return false;
                                        }
                                    }
                                }
                                if (b)
                                {
                                    JsonData msg_type = protos["message " + value_type2.ToJson()];
                                    foreach (JsonData item in msg_name)
                                    {
                                        if (!checkMsg(item, msg_type))
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }

                            break;
                    }
                }

            }
            return true;
        }

        /// <summary>
        /// Encode the message.
        /// </summary>
        private int encodeMsg(byte[] buffer, int offset, JsonData proto, JsonData msg)
        {
            ICollection<string> msgKeys = msg.Keys;
            foreach (string key in msgKeys)
            {
                if (proto.ContainsKey(key))
                {
                    JsonData value = proto[key];
                    if (value.ContainsKey("option"))
                    {
                        JsonData value_option = value["option"];
                        switch (value_option.ToJson())
                        {
                            case "required":
                            case "optional":
                                if (value.ContainsKey("type") && value.ContainsKey("tag"))
                                {
                                    JsonData value_type = value["type"];
                                    JsonData value_tag = value["tag"];
                                    offset = this.writeBytes(buffer, offset, this.encodeTag(value_type.ToString(), Convert.ToInt32(value_tag.ToJson())));
                                    offset = this.encodeProp(msg[key], value_type.ToString(), offset, buffer, proto);
                                }
                                break;
                            case "repeated":
                                if (msg.ContainsKey(key))
                                {
                                    JsonData msg_key = msg[key];
                                    if (msg_key.Count > 0)
                                    {
                                        offset = this.encodeArray(msg_key, value, offset, buffer, proto);
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            return offset;
        }

        /// <summary>
        /// Encode the array type.
        /// </summary>
        private int encodeArray(JsonData msg, JsonData value, int offset, byte[] buffer, JsonData proto)
        {
            if (value.ContainsKey("type") && value.ContainsKey("tag"))
            {
                JsonData value_type = value["type"];
                JsonData value_tag = value["tag"];
                if (this.util.isSimpleType(value_type.ToJson()))
                {
                    offset = this.writeBytes(buffer, offset, this.encodeTag(value_type.ToJson(), Convert.ToInt32(value_tag.ToJson())));
                    offset = this.writeBytes(buffer, offset, Encoder.encodeUInt32((uint)msg.Count));
                    for (int i = 0; i < msg.Count; i++)
                    {
                        offset = this.encodeProp(msg[i], value_type.ToJson(), offset, buffer, null);
                    }
                    //foreach (JsonData item in msg)
                    //{
                    //    offset = this.encodeProp(item, value_type.ToJson(), offset, buffer, null);
                    //}
                }
                else
                {
                    for (int i = 0; i < msg.Count; i++)
                    {
                        offset = this.writeBytes(buffer, offset, this.encodeTag(value_type.ToString(), Convert.ToInt32(value_tag.ToJson())));
                        offset = this.encodeProp(msg[i], value_type.ToString(), offset, buffer, proto);
                    }
                    //foreach (JsonData item in msg)
                    //{
                    //    offset = this.writeBytes(buffer, offset, this.encodeTag(value_type.ToString(), Convert.ToInt32(value_tag.ToJson())));
                    //    offset = this.encodeProp(item, value_type.ToString(), offset, buffer, proto);
                    //}
                }
            }

            return offset;
        }

        /// <summary>
        /// Encode each item in message.
        /// </summary>
        private int encodeProp(JsonData value, string type, int offset, byte[] buffer, JsonData proto)
        {
            switch (type)
            {
                case "uInt32":
                    this.writeUInt32(buffer, ref offset, value);
                    break;
                case "int32":
                case "sInt32":
                    this.writeInt32(buffer, ref offset, value);
                    break;
                case "float":
                    this.writeFloat(buffer, ref offset, value);
                    break;
                case "double":
                    this.writeDouble(buffer, ref offset, value);
                    break;
                case "string":
                    this.writeString(buffer, ref offset, value);
                    break;
                default:
                    if (proto.ContainsKey("__messages"))
                    {
                        JsonData __messages = proto["__messages"];
                        if (__messages.ContainsKey(type))
                        {
                            JsonData __message_type = __messages[type];
                            byte[] tembuff = new byte[Encoder.byteLength(value.ToString()) * 3];
                            int length = 0;
                            length = this.encodeMsg(tembuff, length, __message_type, value);
                            offset = writeBytes(buffer, offset, Encoder.encodeUInt32((uint)length));
                            for (int i = 0; i < length; i++)
                            {
                                buffer[offset] = tembuff[i];
                                offset++;
                            }
                        }
                    }

                    break;
            }
            return offset;
        }

        //Encode string.
        private void writeString(byte[] buffer, ref int offset, object value)
        {
            int le = Encoding.UTF8.GetByteCount(value.ToString());
            offset = writeBytes(buffer, offset, Encoder.encodeUInt32((uint)le));
            byte[] bytes = Encoding.UTF8.GetBytes(value.ToString());
            this.writeBytes(buffer, offset, bytes);
            offset += le;
        }

        //Encode double.
        private void writeDouble(byte[] buffer, ref int offset, object value)
        {
            WriteRawLittleEndian64(buffer, offset, (ulong)BitConverter.DoubleToInt64Bits(double.Parse(value.ToString())));
            offset += 8;
        }

        //Encode float.
        private void writeFloat(byte[] buffer, ref int offset, object value)
        {
            this.writeBytes(buffer, offset, Encoder.encodeFloat(float.Parse(value.ToString())));
            offset += 4;
        }

        ////Encode UInt32.
        private void writeUInt32(byte[] buffer, ref int offset, object value)
        {
            offset = writeBytes(buffer, offset, Encoder.encodeUInt32(value.ToString()));
        }

        //Encode Int32
        private void writeInt32(byte[] buffer, ref int offset, object value)
        {
            offset = writeBytes(buffer, offset, Encoder.encodeSInt32(value.ToString()));
        }

        //Write bytes to buffer.
        private int writeBytes(byte[] buffer, int offset, byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                buffer[offset] = bytes[i];
                offset++;
            }
            return offset;
        }

        //Encode tag.
        private byte[] encodeTag(string type, int tag)
        {
            int flag = this.util.containType(type);
            return Encoder.encodeUInt32((uint)(tag << 3 | flag));
        }


        private void WriteRawLittleEndian64(byte[] buffer, int offset, ulong value)
        {
            buffer[offset++] = ((byte)value);
            buffer[offset++] = ((byte)(value >> 8));
            buffer[offset++] = ((byte)(value >> 16));
            buffer[offset++] = ((byte)(value >> 24));
            buffer[offset++] = ((byte)(value >> 32));
            buffer[offset++] = ((byte)(value >> 40));
            buffer[offset++] = ((byte)(value >> 48));
            buffer[offset++] = ((byte)(value >> 56));
        }
    }
}

