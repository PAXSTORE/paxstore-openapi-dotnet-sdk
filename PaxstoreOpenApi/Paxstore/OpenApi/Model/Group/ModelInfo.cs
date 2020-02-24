using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class ModelInfo
    {
        public long Id { get; set; }
        public FactoryInfo Factory { get; set; }
        public string Name { get; set; }
        public string FactoryModelName { get; set; }
        public string Platform { get; set; }
        public string AndroidType { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
