using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class Apk{
        public string Status { get; set; }       // 状态
        public Nullable<long> VersionCode { get; set; }
        public string VersionName { get; set; }
        public string ApkType { get; set; }       // 应用类型（参数、非参数)
        public string ApkFileType { get; set; }   // A,P,B
        public ApkFile ApkFile { get; set; }
        public string OSType { get; set; }
    }
}
