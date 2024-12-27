using WordleGame.Services;
using WordleGame.ViewModel;
using System;

namespace WordleGame
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var wordleService = new WordleService();
            BindingContext = new WordleViewModel(wordleService);
        }
    }

}
