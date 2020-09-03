using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class PagedApp{
        public long ID { get; set; }
        public string Name { get; set; }                // 应用名字
        public string PackageName { get; set; }         // 应用包名,唯一
        public string status { get; set; }              // 应用状态
        public string OsType { get; set; }              // 操作系统
        public Nullable<bool> SpecificReseller { get; set; }   // 是否定向发布至代理商
        public Nullable<int> ChargeType { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<long> Downloads { get; set; }             // 下载次数
        public IList<Apk> ApkList { get; set; }
    }
}
