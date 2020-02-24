using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class ApkParameter
    {
        public long Id { get; set; }
        public Apk Apk { get; set; }
        public string Name { get; set; }
        public string ParamTemplateName { get; set; }
        public long CreatedDate { get; set; }
        public long UpdatedDate { get; set; }
        public Nullable<bool> ApkAvailable { get; set; }
    }
}
