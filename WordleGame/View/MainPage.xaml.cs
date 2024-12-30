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
    }
}