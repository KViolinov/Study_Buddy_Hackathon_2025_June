using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyBuddy.Data.Models.SubModels.Quiz
{
    public class QuizOption
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("isCorrect")]
        public bool IsCorrect { get; set; }
    }
}
