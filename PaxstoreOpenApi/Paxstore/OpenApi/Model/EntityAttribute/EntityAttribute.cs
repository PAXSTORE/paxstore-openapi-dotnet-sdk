using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class EntityAttribute
    {
        public long ID { get; set; }
        public string EntityType { get; set; }
        public string InputType { get; set; }
        public Nullable<int> MinLength { get; set; }
        public Nullable<int> MaxLength { get; set; }
        public bool Required { get; set; }
        public string Regex { get; set; }
        public string Selector { get; set; }
        public string Key { get; set; }
        public Nullable<int> Index { get; set; }
        public string DefaultLabel { get; set; }
        public IList<EntityAttributeLabelInfo> EntityAttributeLabelList { get; set; }
    }

}
