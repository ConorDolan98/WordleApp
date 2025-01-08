namespace WordleGame
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            //To have login page show first
            Shell.Current.GoToAsync("//LoginPage");

        }
    }
}
