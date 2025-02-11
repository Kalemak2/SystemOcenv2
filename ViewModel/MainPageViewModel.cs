using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using SystemOcen.Model;

namespace SystemOcen.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Uczen> Uczniowie { get; set; }
        private Uczen _selectedUczen;

        public Ocena newOcena { get; set; } = new Ocena();

        public Uczen SelectedUczen
        {
            get => _selectedUczen;
            set
            {
                _selectedUczen = value;
                OnPropertyChanged(nameof(SelectedUczen));
                OnPropertyChanged(nameof(SelectedUczen.Oceny));
            }
        }

        public ICommand selectUczenCmd { get; }
        public ICommand AddUczenCmd { get; }
        public ICommand ObliczSredniaCmd { get; }

        public MainPageViewModel()
        {
            Uczniowie = new ObservableCollection<Uczen>()
            {
                new Uczen() { Id=1, Name="Kornel", Surname="Pakulski", Oceny=new ObservableCollection<Ocena> { new Ocena(){Id=0, Value=5} }},
                new Uczen() { Id=2, Name="Bartek", Surname="Mastalerz", Oceny=new ObservableCollection<Ocena> { new Ocena(){Id=1, Value=4} }},
                new Uczen() { Id=3, Name="Bartek", Surname="Klimczak", Oceny=new ObservableCollection<Ocena> { new Ocena(){Id=2, Value=3} }},
            };

            AddUczenCmd = new Command(AddOcena);
            selectUczenCmd = new Command<Uczen>(SelectUczen);
            ObliczSredniaCmd = new Command(ObliczSrednia);
        }

        public void SelectUczen(Uczen uczen)
        {
            if (uczen != null)
                SelectedUczen = uczen;
        }

        private void AddOcena()
        {
            if (SelectedUczen == null || newOcena.Value <= 0 || newOcena.Value > 6) return;

            SelectedUczen.Oceny.Add(new Ocena
            {
                Id = SelectedUczen.Oceny.Count + 1,
                Value = newOcena.Value
            });

            newOcena = new Ocena();
            OnPropertyChanged(nameof(newOcena));
            OnPropertyChanged(nameof(SelectedUczen.Oceny));

        }

        public void ObliczSrednia()
        {
            if (SelectedUczen?.Oceny == null || SelectedUczen.Oceny.Count == 0) return;

            double suma = SelectedUczen.Oceny.Sum(o => o.Value);
            SelectedUczen.Srednia = suma / SelectedUczen.Oceny.Count;

            OnPropertyChanged(nameof(SelectedUczen));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}