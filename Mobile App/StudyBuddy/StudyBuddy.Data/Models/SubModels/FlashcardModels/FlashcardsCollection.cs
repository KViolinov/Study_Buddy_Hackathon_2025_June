using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyBuddy.Data.Models.SubModels.FlashcardModels
{
    public class FlashcardsCollection
    {
        [JsonPropertyName("flashcards")]
        public List<FlashcardItem> Flashcards { get; set; }
    }
}
