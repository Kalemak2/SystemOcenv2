using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOcen.Model
{
    public class Uczen : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }
        public ObservableCollection<Ocena> Oceny { get; set; } = new ObservableCollection<Ocena>();

        private double _srednia;
        public double Srednia
        {
            get => _srednia;
            set
            {
                _srednia = value;
                OnPropertyChanged(nameof(Srednia));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}

