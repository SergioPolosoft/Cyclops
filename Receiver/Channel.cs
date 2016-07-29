using System.Net;
using System.Net.Sockets;

namespace Receiver
{
    public class Channel
    {
        private Socket socket;

        public Channel()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 1380);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPoint);
            socket.Listen(1);
            
            socket.Accept();            
        }

        public void Start()
        {
            while (true )
            {
                byte[] buffer = {};
                if (socket.Receive(buffer) > 1)
                {
                    
                }
            }
        }
    }
}
