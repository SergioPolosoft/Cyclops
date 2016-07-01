using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentAdapter.Communication
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("192.168.1.1"), 35758);
            var dmChannelSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            dmChannelSocket.Connect();

        }
    }
}
