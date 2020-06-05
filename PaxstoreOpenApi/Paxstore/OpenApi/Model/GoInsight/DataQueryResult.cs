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
        [JsonProperty("worksheetName")]
        public string WorksheetName { get; set; }

        [JsonProperty("columns")]
        public List<Column> Columns { get; set; }

        [JsonProperty("rows")]
        public List<List<Row>> Rows { get; set; }

        [JsonProperty("hasNext")]
        public Nullable<bool> HasNext { get; set; }

        [JsonProperty("offset")]
        public Nullable<long> Offset { get; set; }

        [JsonProperty("limit")]
        public Nullable<int> Limit { get; set; }
    }

    public class Row
    {
        [JsonProperty("colName")]
        public string ColName { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Column
    {
        [JsonProperty("colName")]
        public string ColName { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
