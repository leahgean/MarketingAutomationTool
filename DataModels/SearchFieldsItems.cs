using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class SearchFieldsItems
    {
        [JsonProperty("field")]
        public string Field { get; set; }
        [JsonProperty("operator")]
        public string Operator { get; set; }
        [JsonProperty("searchvalue")]
        public string SearchValue { get; set; }
        [JsonProperty("logicaloperator")]
        public string LogicalOperator { get; set; }
    }
}
