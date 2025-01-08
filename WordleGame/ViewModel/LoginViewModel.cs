using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace WordleGame.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string playerName;
        public string PlayerName
        {
            get => playerName;
            set
            {
                if (playerName != value)
                {
                    playerName = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
            LoadUsername();
        }

        private async void OnLogin()
        {
            SaveUsername();
            await Shell.Current.GoToAsync("//main");
        }

        private void SaveUsername()
        {
            Preferences.Set("PlayerName", PlayerName);
        }

        private void LoadUsername()
        {
            PlayerName = Preferences.Get("PlayerName", string.Empty);
        }
    }
}
