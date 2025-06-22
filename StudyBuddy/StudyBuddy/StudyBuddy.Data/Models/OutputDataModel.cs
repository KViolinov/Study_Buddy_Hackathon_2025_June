using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyBuddy.Data.Models
{
    public class OutputDataModel
    {
        public int output_id { get; set; }
        public int user_id { get; set; }
        public DateTime time_of_sendind { get; set; }
        public string type { get; set; }
        public string output_text { get; set; }

        [JsonIgnore]
        public object? parsed_object { get; set; }
        public int input_id { get; set; }
    }
}
