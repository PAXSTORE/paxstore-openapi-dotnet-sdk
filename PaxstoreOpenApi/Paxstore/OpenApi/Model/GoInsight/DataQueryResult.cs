using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi.Model
{
    public class DataQueryResult
    {
        public string WorksheetName { get; set; }

        public List<Column> Columns { get; set; }

        public List<List<Row>> Rows { get; set; }

        public Nullable<bool> HasNext { get; set; }

        public Nullable<long> Offset { get; set; }

        public Nullable<int> Limit { get; set; }
    }

    public class Row
    {
        public string ColName { get; set; }

        public string Value { get; set; }
    }

    public class Column
    {
        public string ColName { get; set; }

        public string DisplayName { get; set; }

        public string Type { get; set; }
    }
}
