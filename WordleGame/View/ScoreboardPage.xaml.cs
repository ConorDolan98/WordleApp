using WordleGame.ViewModel;
using WordleGame.Model;

namespace WordleGame.View
{
    public partial class ScoreboardPage : ContentPage
    {
        public ScoreboardPage()
        {
            InitializeComponent();
            BindingContext = new ScoreboardViewModel();
        }
    }
}
