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
        [JsonPropertyName("quiz")]
        public List<Quiz.Quiz> Quiz { get; set; }
    }
}
