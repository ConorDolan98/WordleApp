using Microsoft.Maui.Controls;

namespace WordleGame
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register the route for MainPage and GamePage
            Routing.RegisterRoute("main", typeof(View.MainPage));
            Routing.RegisterRoute("game", typeof(View.GamePage));
        }
    }
}
