using Microsoft.Maui.Controls;
using WordleGame.ViewModel;

namespace WordleGame.View
{
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
            BindingContext = new WordleViewModel(new Services.WordleService());
        }
    }
}