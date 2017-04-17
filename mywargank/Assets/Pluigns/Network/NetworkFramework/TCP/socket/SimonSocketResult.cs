namespace Simon.CustomSocket
{
    using System.Net.Sockets;

    public struct SimonSocketResult
    {
        public SocketError result;
        public string errorMsg;

        public SimonSocketResult(SocketError result, string errorMsg)
        {
            this.result = result;
            this.errorMsg = errorMsg;
        }

        public SimonSocketResult(SocketError result)
        {
            this.result = result;
            this.errorMsg = string.Empty;
        }
    }
}
