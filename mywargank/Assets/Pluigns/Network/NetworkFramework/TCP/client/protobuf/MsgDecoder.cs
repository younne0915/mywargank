using System;
using System.Text;
using LitJson;
using System.Collections.Generic;

namespace Pomelo.Protobuf
{
    public class MsgDecoder
    {
        private JsonData protos { set; get; }//The message format(like .proto file)
        private int offset { set; get; }
        private byte[] buffer { set; get; }//The binary message from server.
        private Util util { set; get; }

        public MsgDecoder(JsonData protos)
        {
            if (protos == null) protos = new JsonData();

            this.protos = protos;
            this.util = new Util();
        }

        /// <summary>
        /// Decode message from server.
        /// </summary>
        /// <param name='route'>
        /// Route.
        /// </param>
        /// <param name='buf'>
        /// JsonObject.
        /// </param>
        public JsonData decode(string route, byte[] buf)
        {
            this.buffer = buf;
            this.offset = 0;
            if (this.protos.ContainsKey(route))
            {
                JsonData proto = this.protos[route];
                return this.decodeMsg(proto, this.buffer.Length);
            }
            return null;
        }


        /// <summary>
        /// Decode the message.
        /// </summary>
        /// <returns>
        /// The message.
        /// </returns>
        /// <param name='msg'>
        /// JsonObject.
        /// </param>
        /// <param name='proto'>
        /// JsonObject.
        /// </param>
        /// <param name='length'>
        /// int.
        /// </param>
        private JsonData decodeMsg(JsonData proto, int length)
        {
            JsonData msg = new JsonData();
            while (this.offset < length)
            {
                Dictionary<string, int> head = this.getHead();
                int tag;
                if (head.TryGetValue("tag", out tag))
                {
                    if (proto.ContainsKey("__tags"))
                    {
                        JsonData _tags = proto["__tags"];
                        if (_tags.ContainsKey(tag.ToString()))
                        {
                            JsonData name = _tags[tag.ToString()];
                            if (proto.ContainsKey(name.ToJson()))
                            {
                                JsonData value = proto[name.ToJson()];
                                if (value.ContainsKey("option"))
                                {
                                    JsonData option = value["option"];
                                    switch (option.ToJson())
                                    {
                                        case "optional":
                                        case "required":
                                            if (value.ContainsKey("type"))
                                            {
                                                JsonData type = value["type"];
                                                msg[name.ToJson()] = this.decodeProp(type.ToString(), proto);
                                            }

                                            break;
                                        case "repeated":
                                            if (!msg.ContainsKey(name.ToJson()))
                                            {
                                                JsonData array = new JsonData();

                                                msg[name.ToJson()] = array;
                                            }

                                            JsonData _nameArray = msg[name.ToJson()];
                                            if (value.ContainsKey("type"))
                                            {
                                                JsonData value_type = value["type"];
                                                decodeArray(_nameArray, value_type.ToJson(), proto);
                                            }


                                            break;

                                    }
                                }
                            }
                        }
                    }
                }
            }

            return msg;
        }

        /// <summary>
        /// Decode array in message.
        /// </summary>
        private void decodeArray(JsonData array, string type, JsonData proto)
        {
            if (this.util.isSimpleType(type))
            {
                int length = (int)Decoder.decodeUInt32(this.getBytes());
                for (int i = 0; i < length; i++)
                {
                    array.Add(this.decodeProp(type, null));
                }
            }
            else
            {
                array.Add(this.decodeProp(type, proto));
            }
        }

        /// <summary>
        /// Decode each simple type in message.
        /// </summary>
        private JsonData decodeProp(string type, JsonData proto)
        {
            switch (type)
            {
                case "uInt32":
                    return Decoder.decodeUInt32(this.getBytes());
                case "int32":
                case "sInt32":
                    return Decoder.decodeSInt32(this.getBytes());
                case "float":
                    return this.decodeFloat();
                case "double":
                    return this.decodeDouble();
                case "string":
                    return this.decodeString();
                default:
                    return this.decodeObject(type, proto);
            }
        }

        //Decode the user-defined object type in message.
        private JsonData decodeObject(string type, JsonData proto)
        {
            if (proto != null)
            {
                if (proto.ContainsKey("__messages"))
                {
                    JsonData __messages = proto["__messages"];
                    bool a = __messages.ContainsKey(type);
                    bool b = protos.ContainsKey("message " + type);
                    if (a)
                    {
                        JsonData _type = __messages[type];
                        int l = (int)Decoder.decodeUInt32(this.getBytes());
                        return this.decodeMsg(_type, this.offset + l);
                    }

                }
            }

            return new JsonData();
        }

        //Decode string type.
        private string decodeString()
        {
            int length = (int)Decoder.decodeUInt32(this.getBytes());
            string msg_string = Encoding.UTF8.GetString(this.buffer, this.offset, length);
            this.offset += length;
            return msg_string;
        }

        //Decode double type.
        private double decodeDouble()
        {
            double msg_double = BitConverter.Int64BitsToDouble((long)this.ReadRawLittleEndian64());
            this.offset += 8;
            return msg_double;
        }

        //Decode float type
        private float decodeFloat()
        {
            float msg_float = BitConverter.ToSingle(this.buffer, this.offset);
            this.offset += 4;
            return msg_float;
        }

        //Read long in littleEndian
        private ulong ReadRawLittleEndian64()
        {
            ulong b1 = buffer[this.offset];
            ulong b2 = buffer[this.offset + 1];
            ulong b3 = buffer[this.offset + 2];
            ulong b4 = buffer[this.offset + 3];
            ulong b5 = buffer[this.offset + 4];
            ulong b6 = buffer[this.offset + 5];
            ulong b7 = buffer[this.offset + 6];
            ulong b8 = buffer[this.offset + 7];
            return b1 | (b2 << 8) | (b3 << 16) | (b4 << 24)
               | (b5 << 32) | (b6 << 40) | (b7 << 48) | (b8 << 56);
        }

        //Get the type and tag.
        private Dictionary<string, int> getHead()
        {
            int tag = (int)Decoder.decodeUInt32(this.getBytes());
            Dictionary<string, int> head = new Dictionary<string, int>();
            head.Add("type", tag & 0x7);
            head.Add("tag", tag >> 3);
            return head;
        }

        //Get bytes.
        private byte[] getBytes()
        {
            List<byte> arrayList = new List<byte>();
            int pos = this.offset;
            byte b;
            do
            {
                b = this.buffer[pos];
                arrayList.Add(b);
                pos++;
            } while (b >= 128);
            this.offset = pos;
            int length = arrayList.Count;
            byte[] bytes = new byte[length];
            for (int i = 0; i < length; i++)
            {
                bytes[i] = arrayList[i];
            }
            return bytes;
        }
    }
}

