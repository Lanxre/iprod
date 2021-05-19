using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace iprod
{
    public static class DataSender
    {
        private static TcpClient _client;
        private static NetworkStream _stream;

        private const string Host = "127.0.0.1";
        private const int Port = 8888;

        public static string Message;

        public static void OperationDataSend(string senderObject)
        {
            _client = new TcpClient();
                
            try
            {
                _client.Connect(Host, Port);
                _stream = _client.GetStream();
                
                SendMessage(senderObject);
                Thread.Sleep(500);
                ReceiveMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }


        private static void SendMessage(string senderObject)
        {
            var data = Encoding.Unicode.GetBytes(senderObject);
            _stream.Write(data, 0, data.Length);
        }

        private static void ReceiveMessage()
        {
            try
            {
                var data = new byte[64];
                var builder = new StringBuilder();
                do
                {
                    var bytes = _stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                } while (_stream.DataAvailable);

                var message = builder.ToString();
                Message = message;
            }
            catch
            {
                Disconnect();
            }
        }

        private static void Disconnect()
        {
            _stream?.Close();
            _client?.Close();
        }
    }
}