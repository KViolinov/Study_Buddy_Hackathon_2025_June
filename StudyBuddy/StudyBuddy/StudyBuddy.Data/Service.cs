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

            //Test model 
            //input = new InputModel
            //{
            //    userId = 1,
            //    type = "quiz",
            //    inputSourceType = "docx",
            //    inputText = "NEW NEW NEW TEXT"
            //};

            var json = JsonSerializer.Serialize(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<OutputModel>(responseBody);
            Console.WriteLine(output);
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
                        var flashcardsCollection = JsonSerializer.Deserialize<FlashcardsCollection>(cleaned);
                        output.data.parsed_object = flashcardsCollection;
                        return output;
                           
                    }

                    else if (output.data.type == "quiz")
                    {
                        try
                        {
                            List<Quiz> model = JsonSerializer.Deserialize<List<Quiz>>(cleaned);

                            if (model != null)
                            {
                                output.data.parsed_object = model;
                            }

                            //output.data.parsed_object = model;
                        }
                        catch
                        {
                            // raw if parsing fails
                        }
                    }
                    else
                    {
                        // If type is unknown, => output_text raw
                    }
                }
                catch
                {
                    // raw if top-level parsing fails
                }
            }

            return output;
        }



    }

}

//TODO: Error handlings!! (If there is ny time left)
