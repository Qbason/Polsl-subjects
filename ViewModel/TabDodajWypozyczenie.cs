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

    class TabDodajWypozyczenie : ViewModelBase
    {

        #region Składowe prywatne
        //przygotowane pod import z modelu głownego
        private Model model = null;

        //kolekcje do przechowywania obiektow
        private ObservableCollection<Wypozyczenie> wypozyczenia = null;
        private ObservableCollection<Ksiazka> wolneksiazki = null;
        private ObservableCollection<Czytelnik> czytelnicy = null;
        private ObservableCollection<Pracownik> pracownicy = null;
       
        //zmienne do trzymania aktualnie wybranych obiektow
        private Ksiazka aktualniewybranaksiazka;
        private Czytelnik biezacyczytelnik;
        private Pracownik biezacypracownik;

        #endregion

        #region Konstruktory

        public TabDodajWypozyczenie(Model model)
        {
            //'import' danych z głownego modelu
            this.model = model;
            wolneksiazki = model.WolneKsiazki;
            wypozyczenia = model.Wypozyczenia;
            czytelnicy = model.Czytelnicy;
            pracownicy = model.Pracownicy;
        }
        #endregion

        #region Właściwości
        //propertisy dające możliwośc odczytu danych z Main
        public Ksiazka AktualnieWybranaKsiazka
        {
            get
            {
                return aktualniewybranaksiazka;
            }
            set
            {
                aktualniewybranaksiazka = value;
                onPropertyChanged(nameof(AktualnieWybranaKsiazka));
            }
        }

       



        public ObservableCollection<Ksiazka> WolneKsiazki
        {
            get { return wolneksiazki; }
            set
            {
                wolneksiazki = value;
                onPropertyChanged(nameof(WolneKsiazki));
            }
        }


        public ObservableCollection<Czytelnik> Czytelnicy
        {
            get { return czytelnicy; }
            set
            {
            czytelnicy = value;
                onPropertyChanged(nameof(Czytelnicy));
            }
        }

        public ObservableCollection<Pracownik> Pracownicy
        {
            get { return pracownicy; }
            set
            {
                pracownicy = value;
                onPropertyChanged(nameof(Pracownicy));
            }
        }

        public Czytelnik BiezacyCzytelnik
        {
            get
            {
                return biezacyczytelnik;
            }

            set
            {
                biezacyczytelnik = value;
                onPropertyChanged(nameof(BiezacyCzytelnik));

            }

        }

        public Pracownik BiezacyPracownik
        {
            get
            {
                return biezacypracownik;
            }

            set
            {
                biezacypracownik = value;
                onPropertyChanged(nameof(BiezacyPracownik));

            }

        }


        #endregion

        #region Metody
        //metoda czyszcząca formularz
        private void CzyscFormularz()
        {

            BiezacyCzytelnik = null;
            BiezacyPracownik = null;
            AktualnieWybranaKsiazka = null;


        }

       
        //Icommand umożliwający dodanie nowego wypożyczenia
        private ICommand dodajWypozyczenie = null;
        public ICommand DodajWypozyczenie
        {
            get
            {
                if (dodajWypozyczenie == null)
                    dodajWypozyczenie = new RelayCommand(
                        arg =>
                        {
                        //sprawenie czy któreś z pól nie jest puste
                        if (BiezacyCzytelnik != null && BiezacyPracownik != null & AktualnieWybranaKsiazka != null)
                        {                           
                                //tworzenie nowego obiektu
                                var wypozyczenie = new Wypozyczenie((int)aktualniewybranaksiazka.Id_ksiazka, (sbyte)biezacyczytelnik.Id_czytelnik, (sbyte)biezacypracownik.Id_pracownik, DateTime.Now.ToString("yyyy-M-dd HH:mm:ss"));
                                wypozyczenie.Id_wypozyczenie = AktualnieWybranaKsiazka.Id_ksiazka;
                               
                                //tworzenie nowego wypozyczenia
                                if (model.DodajWypozyczenieDoBazy(wypozyczenie))
                                {
                                    wypozyczenia = model.Wypozyczenia;
                                    CzyscFormularz();
                                    System.Windows.MessageBox.Show("Wypożyczenie zostało dodane do bazy!");
                                }
                            }                            
                        }
                        ,
                        arg => true
                        );

                return dodajWypozyczenie;
            }
        }


        #endregion





    }
}
