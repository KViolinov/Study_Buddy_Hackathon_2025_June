using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyBuddy.Data.Models.SubModels.Quiz
{
    public class QuizQuestion
    {
        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("options")]
        public List<QuizOption> Options { get; set; }

        [JsonPropertyName("explanation")]
        public string Explanation { get; set; }
    }
}
