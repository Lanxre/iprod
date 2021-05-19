using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using Server.Entity;

namespace Server
{
    internal static class Program
    {
        private static Thread _listenThread;

        private static void Main(string[] args)
        {
            
            try
            {
                _listenThread = new Thread((ServerObject.Listen));
                _listenThread.Start(); 
            }
            catch (Exception ex)
            {
                ServerObject.Disconnect();
                Console.WriteLine(ex.Message);
            }
        }
    }
}