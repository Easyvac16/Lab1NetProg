using Core;
using System.Net;
using System.Net.Sockets;

namespace Lab1NetProg
{
    internal class client
    {
        private static IPAddress _address => IPAddress.Parse(Service.IpStr);
        static void Main()
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            string msg;

            Console.WriteLine("Trying to connect to the server...");
            try
            {
                client.Connect(_address, Service.Port);

                if (!client.Connected)
                {
                    throw new Exception("Error connection");
                }

                Console.WriteLine(Service.RecieveMsg(client));

                while (true)
                {
                    if (!ReadMsg(out msg))
                    {
                        break;
                    }

                    Service.SendMsg(client, msg);

                    if (Service.RecieveMsg(client) == "t")
                    {
                        Console.WriteLine("Correct");
                        break;
                    }
                    Console.WriteLine("Inccorect");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine("Connection closed...");
        }

        public static bool ReadMsg(out string msg)
        {
            while (true)
            {
                Console.WriteLine("Write your message:");
                msg = Console.ReadLine() ?? "";

                if (msg.Length > 0)
                {
                    return msg != "exit";
                }
                Console.WriteLine("Message can't be empty");
            }


        }
    }
}
