using System.Collections.ObjectModel;
using WordleGame.Model;

namespace WordleGame.ViewModel
{
    public class ScoreboardViewModel : BaseViewModel
    {
        private ObservableCollection<ScoreData> scoreboard = new();

        public ObservableCollection<ScoreData> Scoreboard
        {
            get => scoreboard;
            set
            {
                scoreboard = value;
                OnPropertyChanged();
            }
        }

        public ScoreboardViewModel()
        {
            Title = "Scoreboard";
            // Load the scoreboard data here if needed
        }

        public void AddScore(ScoreData scoreData)
        {
            Scoreboard.Add(scoreData);
        }
    }
}


