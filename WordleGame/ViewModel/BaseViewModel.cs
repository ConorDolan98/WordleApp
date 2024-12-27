﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WordleGame.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy;
        string title;

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy = value)
                    return;
                isBusy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        public string Title
        {
            get => title;
            set
            {
                if (title == value)
                    return;
                title = value;
                OnPropertyChanged();
            }
        }

        public bool IsNotBusy => !isBusy;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string word = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(word));
        }
    }
}
