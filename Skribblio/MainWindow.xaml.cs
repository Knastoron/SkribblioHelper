using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace Skribblio
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        List<string> _allChamps = new List<string>();
        public string AllChamps
        {
            set
            {
                _allChamps = value.Split(',').ToList();
                UpdatePossibles();
                OnPropertyChanged("AllChamps");
            }
        }
        private int _anzahl;
        public int Anzahl {
            get
            {
                return _anzahl;
            }
            set
            {
                if(value!= _anzahl)
                {
                    _anzahl = value;

                    OnPropertyChanged("Anzahl");
                    UpdatePossibles();
                }
            }
        }

        private string _regex;

        public string Regex
        {
            get { return _regex; }
            set
            {
                _regex = value;
                char[] letters = value.ToCharArray();
                regexString = ".*";
                foreach(var s in letters)
                {
                    regexString += s;
                    regexString += ".*";
                }

                OnPropertyChanged("Regex");
                UpdatePossibles();
            }
        }

        private string regexString = string.Empty;

        public string SelectedChamps { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        public void UpdatePossibles()
        {
            StringBuilder s = new StringBuilder();
            Regex R = new Regex(regexString, RegexOptions.IgnoreCase);
            foreach (var champ in _allChamps)
            {
                Console.WriteLine(champ);
                Match m = R.Match(champ);
                if (champ.Length == Anzahl && m.Success)
                {

                    s.Append(champ);
                    s.Append(", ");
                }
                
            }
            SelectedChamps = s.ToString();

            OnPropertyChanged("SelectedChamps");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
