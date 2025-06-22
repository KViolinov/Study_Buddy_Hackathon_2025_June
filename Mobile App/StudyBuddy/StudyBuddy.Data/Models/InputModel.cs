using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Data.Models
{
    public class InputModel
    {
        public int userId { get; set; }
        public string type { get; set; }
        public string inputSourceType { get; set; }
        public string inputText { get; set; }
    }
}
