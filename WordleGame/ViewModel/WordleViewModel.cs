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
        private WordleService wordleService;
        private string selectWord = "";
        private char[] selectWordArray;
        private string playerAnswer;
        private ObservableCollection<string> guessResult;
        private int maxAttempts = 6;

        //track player attempts
        public int MaxAttempts
        {
            get => maxAttempts;
            set
            {
                if (maxAttempts != value)
                {
                    maxAttempts = value;
                    OnPropertyChanged();
                }
            }
        }

        // SelectWord holds a word from the list in GetWordsAsync method
        public string SelectWord
        {
            get => selectWord;
            set
            {
                if (selectWord != value)
                {
                    selectWord = value;
                    OnPropertyChanged();

                    //selectWordArray stores the word in an array for comparison
                    selectWordArray = selectWord?.ToCharArray();
                }
            }
        }

        //Users guess
        public string PlayerAnswer
        {
            get => playerAnswer;
            set
            {
                if (playerAnswer != value)
                {
                    playerAnswer = value;
                    OnPropertyChanged();
                }
            }
        }

        // ObservableCollection to store feedback results
        public ObservableCollection<string> GuessResult
        {
            get => guessResult;
            set
            {
                guessResult = value;
                OnPropertyChanged();
            }
        }

        public Command GetWordsCommand { get; }
        public Command SubmitAnswerCommand { get; }

        //WordleViewModel Default Constructor
        public WordleViewModel()
        {
            
        }

        //WordleViewModel Constructor takes wordleService parameter
        public WordleViewModel(WordleService wordleService)
        {
            Title = "Wordle";
            this.wordleService = wordleService;
            GetWordsCommand = new Command(async () => await GetWordsAsync());
            SubmitAnswerCommand = new Command(async () => await SubmitAnswerAsync());

            
            Debug.WriteLine("WordleViewModel initialized");
            //initiaing the GetWordsAsync method
            _ = GetWordsAsync();
        }

        // Method to get the list of words and select a random word
        async Task GetWordsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var words = await wordleService.GetWords();

                if (words.Any())
                {
                    SelectWord = words[0].Word;
                    Debug.WriteLine($"Selected Word: {SelectWord}");
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

        //method to submit the player's answer and provide feedback
        async Task SubmitAnswerAsync()
        {
            if (IsBusy|| MaxAttempts <= 0)
                return;

            try
            {
                IsBusy = true;
                var playerAnswerArray = PlayerAnswer.ToCharArray(); //puts user's answer into an array
                var result = new ObservableCollection<string>();

                if (selectWordArray == null)
                {
                    result.Add("Error: The selected word has not been initialized.");
                    GuessResult = result;
                    return;
                }

                if (playerAnswerArray.Length != 5)
                {
                    result.Add("Please enter exactly 5 characters.");
                }
                else
                {
                    var gameFeedback = new char[5];

                    // Loop compares user's input to the selected word
                    for (int i = 0; i < 5; i++)
                    {
                        if (playerAnswerArray[i] == selectWordArray[i])
                        {
                            gameFeedback[i] = 'G';
                            result.Add($"{playerAnswerArray[i]} is in the correct position");
                        }
                        else if (selectWordArray.Contains(playerAnswerArray[i]))
                        {
                            gameFeedback[i] = 'Y';
                            result.Add($"{playerAnswerArray[i]} is correct but wrong position.");
                        }
                        else
                        {
                            gameFeedback[i] = 'X';
                            result.Add($"{playerAnswerArray[i]} is incorrect");
                        }
                    }

                    maxAttempts--;
                    //displaying results
                    if (new string(playerAnswerArray) == new string(selectWordArray))
                    {
                        result.Add("Congratulations! You guessed the word!");
                        GuessResult = result;

                        // Show a popup for winning
                        await Shell.Current.DisplayAlert("You Win!", "Congratulations! You've guessed the word correctly.", "OK");
                        MaxAttempts = 0; // Game ends
                    }
                    else if (MaxAttempts == 0)
                    {
                        result.Add($"Game over! The word was: {new string(selectWordArray)}");
                        GuessResult = result;

                        // Show a popup for losing
                        await Shell.Current.DisplayAlert("Game Over", $"You've run out of attempts. The word was: {new string(selectWordArray)}.", "OK");
                    }
                    else
                    {
                        // Result stored in GuessResult observable collection
                        GuessResult = result;
                    }
                    // Clears the input
                    PlayerAnswer = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Unable to submit answer", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
