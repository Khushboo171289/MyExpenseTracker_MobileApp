using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MyExpenseTracker
{
    public class MainViewModel : INotifyPropertyChanged
    {
        string name = string.Empty;
        public string Name
        {
            get => name;
            set
            {
                if (name == value)
                    return;
                name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(DisplayName));

            }
        }
        

        public string DisplayName => $"{Name}";

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
