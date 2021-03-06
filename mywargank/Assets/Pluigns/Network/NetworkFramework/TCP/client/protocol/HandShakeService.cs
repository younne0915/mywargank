using System;
using System.Text;
using LitJson;

namespace Pomelo.DotNetClient
{
    public class HandShakeService
    {
        private Protocol protocol;
        private Action<JsonData> callback;

        public const string Version = "0.3.0";
        public const string Type = "unity-socket";


        public HandShakeService(Protocol protocol)
        {
            this.protocol = protocol;
        }

        public void request(JsonData user, Action<JsonData> callback)
        {
            string data = buildMsg(user).ToJson();
            byte[] body = Encoding.UTF8.GetBytes(data);

            protocol.sendPackage(PackageType.PKG_HANDSHAKE, body);

            this.callback = callback;
        }

        internal void invokeCallback(JsonData data)
        {
            //Invoke the handshake callback
            if (callback != null) callback.Invoke(data);
        }

        public void ack()
        {
            protocol.sendPackageWithoutContent(PackageType.PKG_HANDSHAKE_ACK);
        }

        private JsonData buildMsg(JsonData user)
        {
            if (user == null)
            {
                user = new JsonData();
                user.SetJsonType(JsonType.Object);
            }

            JsonData msg = new JsonData();

            //Build sys option
            JsonData sys = new JsonData();
            sys["version"] = Version;
            sys["type"] = Type;

            //Build handshake message
            msg["sys"] = sys;
            msg["user"] = user;

            return msg;
        }
    }
}