using System.Collections.ObjectModel;
using WordleGame.Model;

namespace WordleGame.ViewModel
{
    public class ScoreboardViewModel : BaseViewModel
    {
        private ObservableCollection<ScoreBoard> scores;

        public ObservableCollection<ScoreBoard> Scores
        {
            get => scores;
            set
            {
                if (scores != value)
                {
                    scores = value;
                    OnPropertyChanged();
                }
            }
        }

        public ScoreboardViewModel()
        {
            Scores = new ObservableCollection<ScoreBoard>();
        }

        public class ScoreBoard
        {
            public string PlayerName { get; set; }
            public string Word { get; set; }
            public int Attempts { get; set; }
        }

        public void AddResult(string playerName, string word, int attempts)
        {
            Scores.Add(new ScoreBoard
            {
                PlayerName = playerName,
                Word = word,
                Attempts = attempts
            });
        }

    }
}


