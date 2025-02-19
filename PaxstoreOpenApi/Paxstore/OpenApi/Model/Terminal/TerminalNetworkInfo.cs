using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Paxstore.OpenApi.Model
{
    public class TerminalNetworkInfo
    {
        public long ID { get; set; }
        public string TID;
        public string SerialNo;
        public string Status;
        public Nullable<float> Battery;
        public int OnlineStatus;
        public string Network;
        public string macAddress;
    }
}
