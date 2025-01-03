using Microsoft.Maui.Controls;
using WordleGame.ViewModel;

namespace WordleGame.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        private void OnScoreboardClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScoreboardPage());
        }
    }
}