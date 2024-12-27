using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WordleGame.Model;
using WordleGame.Services;

namespace WordleGame.ViewModel
{
    public class WordleViewModel : BaseViewModel
    {
        WordleService wordleService;
        public ObservableCollection<Wordle> Words { get; } = new();

        public Command GetWordsCommand { get; }
        public WordleViewModel(WordleService wordleService)
        {
            Title = "Wordle";
            this.wordleService = wordleService;
            GetWordsCommand = new Command(async () => await GetWordsAsync());
        }

        async Task GetWordsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var words = await wordleService.GetWords();

                if (Words.Count != 0)
                    Words.Clear();

                foreach (var word in words)
                {
                    Words.Add(word);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Unable to get words", "OK");

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}