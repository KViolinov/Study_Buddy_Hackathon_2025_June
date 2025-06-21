using StudyBuddy.Data.Models;
using StudyBuddy.Data.Models.SubModels;
using StudyBuddy.Data.Models.SubModels.FlashcardModels;
using StudyBuddy.Data.Models.SubModels.Quiz;
using StudyBuddy.Data.Models.SubModels.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudyBuddy.Data
{
    public class Service
    {
        private readonly HttpClient _httpClient = new();

        public async Task<OutputModel> InputOutputTransfer(InputModel input)
        {
            var url = "https://study-buddy-hackathon-2025-june.onrender.com/api/inputs";

            //var input = new InputModel
            //{
            //    userId = 1,
            //    type = "flashcards",
            //    inputSourceType = "docx",
            //    inputText = "NEW NEW NEW TEXT"
            //};

            var json = JsonSerializer.Serialize(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<OutputModel>(responseBody);

            if (output?.data?.output_text is string raw)
            {
                try
                {
                    string cleaned = raw.Replace("```json", "").Replace("```", "").Trim();

                    if (output.data.type == "summary")
                    {
                        Summary? parsed = JsonSerializer.Deserialize<Summary>(cleaned);
                        output.data.parsed_object = parsed;
                    }
                    else if (output.data.type == "flashcards")
                    {
                        // Try to parse as FlashcardsCollection
                        try
                        {
                            var flashcardsCollection = JsonSerializer.Deserialize<FlashcardsCollection>(cleaned);
                            if (flashcardsCollection != null && flashcardsCollection.Flashcards?.Count > 0)
                            {
                                var allAnswers = flashcardsCollection.Flashcards
                                    .Select(f => f.Answer)
                                    .Where(a => !string.IsNullOrEmpty(a))
                                    .Distinct()
                                    .ToList();

                                var result = new List<Quiz>();

                                foreach (var item in flashcardsCollection.Flashcards)
                                {
                                    var correct = item.Answer;
                                    var options = allAnswers
                                        .Where(a => a != correct)
                                        .OrderBy(_ => Guid.NewGuid())
                                        .Take(3)
                                        .ToList();

                                    options.Add(correct);
                                    options = options.OrderBy(_ => Guid.NewGuid()).ToList();

                                    result.Add(new Quiz
                                    {
                                        Question = item.Question,
                                        CorrectAnswer = correct,
                                        Options = options
                                    });
                                }

                                output.data.parsed_object = result;
                                return output;
                            }
                        }
                        catch
                        {
                            // ignored - try manual fallback
                        }

                        // Manual fallback using JsonDocument parsing
                        try
                        {
                            using var doc = JsonDocument.Parse(cleaned);
                            var root = doc.RootElement;
                            var flashcards = root.GetProperty("flashcards");

                            var allAnswers = flashcards.EnumerateArray()
                                .Select(f => f.GetProperty("answer").GetString())
                                .Where(a => !string.IsNullOrEmpty(a))
                                .Distinct()
                                .ToList();

                            var result = new List<Quiz>();

                            foreach (var item in flashcards.EnumerateArray())
                            {
                                var question = item.GetProperty("question").GetString();
                                var correct = item.GetProperty("answer").GetString();

                                var options = allAnswers
                                    .Where(a => a != correct)
                                    .OrderBy(_ => Guid.NewGuid())
                                    .Take(3)
                                    .ToList();

                                options.Add(correct);
                                options = options.OrderBy(_ => Guid.NewGuid()).ToList();

                                result.Add(new Quiz
                                {
                                    Question = question,
                                    CorrectAnswer = correct,
                                    Options = options
                                });
                            }

                            output.data.parsed_object = result;
                        }
                        catch
                        {
                            // leave raw if parsing fails
                        }
                    }
                    else if (output.data.type == "quiz")
                    {
                        try
                        {
                            QuizModel? quizModel = JsonSerializer.Deserialize<QuizModel>(cleaned);
                            if (quizModel != null)
                            {
                                output.data.parsed_object = quizModel;
                            }
                        }
                        catch
                        {
                            // leave raw if parsing fails
                        }
                    }
                    else
                    {
                        // If type is unknown, leave output_text raw
                    }
                }
                catch
                {
                    // leave raw if top-level parsing fails
                }
            }

            return output;
        }



    }
}
