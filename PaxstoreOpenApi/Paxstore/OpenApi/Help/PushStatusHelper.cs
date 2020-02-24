using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Help
{
    public class PushStatusHelper
    {
        public static string GetPushStatusVal(PushStatus pushStatus) {
            switch (pushStatus)
            {
                case PushStatus.Active:
                    return "A";
                case PushStatus.Suspend:
                    return "S";
                case PushStatus.All:
                    return null;
            }
            return null;
        }
    }
}
