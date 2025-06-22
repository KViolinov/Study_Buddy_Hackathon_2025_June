using StudyBuddy.Data.Models.SubModels.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyBuddy.Data.Models.SubModels.QuizModels
{
    public class QuizModel
    {
        [JsonPropertyName("quizTitle")]
        public string QuizTitle { get; set; }

        [JsonPropertyName("questions")]
        public List<QuizQuestion> Questions { get; set; }
    }
}
