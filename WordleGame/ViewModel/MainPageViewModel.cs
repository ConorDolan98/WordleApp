using System.Windows.Input;

namespace WordleGame.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private string playerName;


        public ICommand NavigateToGameCommand { get; }
        public ICommand NavigateToSettingsCommand { get; }

        public string PlayerName
        {
            get => playerName;
            set
            {
                playerName = value;
                OnPropertyChanged();
            }
        }

        public MainPageViewModel()
        {
            Title = "Main Page";
            NavigateToGameCommand = new Command(async () => await NavigateToAsync("game"));
            NavigateToSettingsCommand = new Command (async () => await NavigateToAsync("settings"));
        }
    }
}
