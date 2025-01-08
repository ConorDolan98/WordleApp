using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WordleGame.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private bool isBusy;
        private string title;
        //Busy indicator
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged();
                }
            }
        }


        //Allows ui to update
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Navigation method
        public async Task NavigateToAsync(string route)
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    await Shell.Current.GoToAsync(route);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation Error: {ex.Message}");
                await Shell.Current.DisplayAlert("Navigation Error", $"Error navigating to {route}. {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
