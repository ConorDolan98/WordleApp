using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleGame.Model;
using System.Net.Http.Json;
using System.Text.Json;

namespace WordleGame.Services
{
    public class WordleService
    {
        private readonly HttpClient httpClient;
        private List<Wordle> wordsList = new();
        private int currentWordIndex = -1;

        public WordleService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Wordle>> GetWords()
        {
            // Checks if words are loaded
            if (wordsList.Count > 0)
                //RandomizeWords(wordsList);
            return wordsList;

            var wordUrl = "https://raw.githubusercontent.com/DonH-ITS/jsonfiles/main/words.txt";

            try
            {
                var response = await httpClient.GetAsync(wordUrl);

                if (response.IsSuccessStatusCode)
                {
                    var text = await response.Content.ReadAsStringAsync();
                    var words = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                    // Create Wordle objects for each word
                    foreach (var word in words)
                    {
                        wordsList.Add(new Wordle { Word = word.Trim() });
                    }

                    RandomizeWords(wordsList);
                }
                else
                {
                    throw new Exception("Unable to retrieve words from the API.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching words", ex);
            }

            return wordsList;
        }

        internal object GetNextWord()
        {
            if (wordsList.Count == 0) ;

            currentWordIndex = (currentWordIndex + 1) % wordsList.Count;
            return wordsList[currentWordIndex];
        }

        private void RandomizeWords(List<Wordle> wordsList)
        {
            var random = new Random();
            for (int i = 0; i < wordsList.Count; i++)
            {
                int randomIndex = random.Next(i, wordsList.Count);
                var order = wordsList[i];
                wordsList[i] = wordsList[randomIndex];
                wordsList[randomIndex] = order;
            }
        }
    }
}

