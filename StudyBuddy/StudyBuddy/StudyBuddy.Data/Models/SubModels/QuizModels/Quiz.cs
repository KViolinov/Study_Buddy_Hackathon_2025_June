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
        public string Question { get; set; }

        public string CorrectAnswer { get; set; }

        public List<string> Options { get; set; }
    }
}
