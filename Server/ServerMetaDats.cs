using System;
using System.Text.Json;

namespace Server
{
    public class ServerMetaDats
    {
        public bool SendMessage { get; set; }

        public string TypeClassSend { get; set; }
        public bool SenderStatus { get; set; }
        
        public string FuncAddBd { get; set; } 
        
        public bool GetItem { get; set; }
        
        public int GetType { get; set; }
        
        public bool UpdateItem { get; set; }
       




    public void GetMetaDats(string sender)
        {
            var acceptData = JsonSerializer.Deserialize<ServerMetaDats>(sender);
            if (acceptData == null) return;
            TypeClassSend = acceptData.TypeClassSend;
            SendMessage = acceptData.SendMessage;
            FuncAddBd = acceptData.FuncAddBd;
            GetItem = acceptData.GetItem;
            GetType = acceptData.GetType;
            UpdateItem = acceptData.UpdateItem;
        }
    }
}