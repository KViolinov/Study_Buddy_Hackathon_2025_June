using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyBuddy.Data.Models.SubModels.Quiz
{
    public class Quiz
    {
        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonIgnore] // Don't serialize
        public string CorrectAnswer { get; set; }

        [JsonPropertyName("options")]
        public List<string> Options { get; set; }
    }
}
