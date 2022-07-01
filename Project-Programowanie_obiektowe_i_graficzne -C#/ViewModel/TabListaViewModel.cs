using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektgrupowy.ViewModel
{
    using Model;
    using DAL.Encje;
    using BaseClass;
    using System.Windows.Input;

    class TabListaViewModel : ViewModelBase
    {

        #region Składowe prywatne

        private Model model = null;
        //kolekcje na obiekty
        private ObservableCollection<Czytelnik> czytelnicy = null;
        private ObservableCollection<Ksiazka> ksiazki = null;

       
        private int indeksZaznaczonegoCzytelnika = -1;
        private int indeksZaznaczonejKsiazki = -1;

        #endregion

        #region Konstruktory
        public TabListaViewModel(Model model)
        {
            //'import' danych z glownego modelu
            this.model = model;
            czytelnicy = model.Czytelnicy;
            ksiazki = model.Ksiazki;
        }
        #endregion

        #region Właściwości


        public int IndeksZaznaczonegoCzytelnika
        {
            get => indeksZaznaczonegoCzytelnika;
            set
            {
                indeksZaznaczonegoCzytelnika = value;
                onPropertyChanged(nameof(indeksZaznaczonegoCzytelnika));
            }
        }

        public int IndeksZaznaczonejKsiazki
        {
            get => indeksZaznaczonejKsiazki;
            set
            {
                indeksZaznaczonejKsiazki = value;
                onPropertyChanged(nameof(indeksZaznaczonejKsiazki));
            }
        }

        public Czytelnik BiezacyCzytelnik { get; set; }

        public Ksiazka BiezacaKsiazka { get; set; }

        public ObservableCollection<Czytelnik> Czytelnicy
        {
            get { return czytelnicy; }
            set
            {
                czytelnicy = value;
                onPropertyChanged(nameof(Czytelnicy));
            }
        }

        public ObservableCollection<Ksiazka> Ksiazki
        {
            get { return ksiazki; }
            set
            {
                ksiazki = value;
                onPropertyChanged(nameof(Ksiazki));
            }
        }
        #endregion

        #region Metody
        public void OdswiezCzytelnicy() => Czytelnicy = model.Czytelnicy;
        #endregion

        #region Polecenia





        //funkcja umożliwająca załadowanie Ksiazek od danego czytelnika
        private ICommand zaladujKsiazki = null;
        public ICommand ZaladujKsiazki
        {
            get
            {
                if (zaladujKsiazki == null)
                    zaladujKsiazki = new RelayCommand(
                        arg => {
                            if (BiezacyCzytelnik != null)
                                Ksiazki = model.PobierzKsiazkiCzytelnika(BiezacyCzytelnik);
                        },
                        arg => true
                        );

                return zaladujKsiazki;
            }
        }

        
        //metoda do załadowanie wszystkich ksiazek
        private ICommand zaladujWszystkieKsiazki = null;
        public ICommand ZaladujWszystkieKsiazki
        {
            get
            {
                if (zaladujWszystkieKsiazki == null)
                    zaladujWszystkieKsiazki = new RelayCommand(
                        arg =>
                        {
                            Ksiazki = model.Ksiazki;
                            indeksZaznaczonejKsiazki = -1;
                        }
                        ,
                        arg => true
                        );

                return zaladujWszystkieKsiazki;
            }
        }


        //funckja do zaladowania wszystkich czytelnikow
        private ICommand zaladujWyszystkichCzytelnikow = null;
        public ICommand ZaladujWyszystkichCzytelnikow
        {
            get
            {
                if (zaladujWyszystkichCzytelnikow == null)
                    zaladujWyszystkichCzytelnikow = new RelayCommand(
                        arg =>
                        {
                            Czytelnicy = model.Czytelnicy;
                            indeksZaznaczonejKsiazki = -1;
                        },
                        arg => true
                        );

                return zaladujWyszystkichCzytelnikow;
            }
        }
        #endregion








    }
}
