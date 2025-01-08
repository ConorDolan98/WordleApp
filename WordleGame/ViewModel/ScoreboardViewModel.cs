using System.Collections.ObjectModel;
using System.Text.Json;
using WordleGame.Model;

namespace WordleGame.ViewModel
{
    public class ScoreboardViewModel : BaseViewModel
    {
        private const string ScoreFileName = "scores.json";

        public ObservableCollection<ScoreBoard> Scores { get; set; } = new ObservableCollection<ScoreBoard>();

        public ScoreboardViewModel()
        {
            LoadScores();
            ResetScoresCommand = new Command(ResetScores);
        }

        public Command ResetScoresCommand { get; }

        // Reset scores
        public void ResetScores()
        {
            Scores.Clear();

            var filePath = Path.Combine(FileSystem.AppDataDirectory, "scores.json");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            Console.WriteLine("Scores reset successfully.");
        }

        // Load scores 
        private void LoadScores()
        {
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, ScoreFileName);
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var loadedScores = JsonSerializer.Deserialize<List<ScoreBoard>>(json);
                    if (loadedScores != null)
                    {
                        Scores.Clear();
                        foreach (var score in loadedScores)
                        {
                            Scores.Add(score);
                        }
                        Console.WriteLine($"Loaded {Scores.Count} scores.");
                    }
                }
                else
                {
                    Console.WriteLine("No scores file found. Starting fresh.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading scores: {ex.Message}");
            }
        }

        // Add a new score and save it
        public void AddScore(string playerName, string word, int attempts)
        {
            var newScore = new ScoreBoard
            {
                PlayerName = playerName,
                Word = word,
                Attempts = attempts
            };

            Scores.Add(newScore);
            SaveScores();
        }

        // Save scores
        private void SaveScores()
        {
            try
            {
                var json = JsonSerializer.Serialize(Scores.ToList());
                var filePath = Path.Combine(FileSystem.AppDataDirectory, ScoreFileName);
                File.WriteAllText(filePath, json);
                Console.WriteLine($"Scores saved to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving scores: {ex.Message}");
            }
        }

        // ScoreBoard class
        public class ScoreBoard
        {
            public string PlayerName { get; set; }
            public string Word { get; set; }
            public int Attempts { get; set; }
        }
    }
}
