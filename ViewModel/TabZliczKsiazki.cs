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

    class TabZliczKsiazki : ViewModelBase
    {

        #region Składowe prywatne

        private Model model = null;

        //kolekcja na obiekty
        private ObservableCollection<GrupowaneKsiazki> gksiazki = null;

        //zmienne przechowywane na rzecz propertisow
        private string tytul;
        private int ilosc;
        private string aktualnytytul = "";

        #endregion

        #region Konstruktory

        public TabZliczKsiazki(Model model)
        {
            //'import' modelu i Ksiazki
            this.model = model;
            gksiazki = model.GrupowaneKsiazki;
        }
        #endregion

        #region Właściwości
        //propersity umożliwające dostęp do danych wraz z aktualizacją w intefejsie graficznym
        public string AktualnyTytul
        {
            get { return aktualnytytul; }
            set
            {
                aktualnytytul = value;

                onPropertyChanged(nameof(AktualnyTytul));
            }


        }
        public string Tytul
        {
            get { return tytul; }
            set
            {
                tytul = value;
                onPropertyChanged(nameof(Tytul));
            }
        }
        public int Ilosc
        {
            get { return ilosc; }
            set
            {
                ilosc = value;
                onPropertyChanged(nameof(Ilosc));
            }
        }



        public ObservableCollection<GrupowaneKsiazki> Gksiazki
        {
            get { return gksiazki; }
            set
            {
                gksiazki = value;
                onPropertyChanged(nameof(Gksiazki));
            }
        }


        #endregion



        #region Metody

        //metoda umożliwająca załadowanie z grupowanychksiazek
        private ICommand zaladujZgrupowaneKsiazki = null;
        public ICommand ZaladujZgrupowaneKsiazki
        {
            get
            {
                if (zaladujZgrupowaneKsiazki == null)
                    zaladujZgrupowaneKsiazki = new RelayCommand(
                        arg =>
                        {
                            Gksiazki = model.GrupowaneKsiazki;
                        }
                        ,
                        arg => true
                        );

                return zaladujZgrupowaneKsiazki;
            }
        }

        //funkcja do resetowania wyszukanych wcześniej książek
        private ICommand reset;
        public ICommand Reset
        {
            get
            {
                if (reset == null)
                    reset = new RelayCommand(
                        arg =>
                        {
                            Gksiazki = model.GrupowaneKsiazki;
                            AktualnyTytul = "";
                        }
                        ,
                        arg => true
                        );

                return reset;
            }

        }
        //metoda umożliwająca szukanie
        private ICommand szukaj;
        public ICommand Szukaj
        {
            get
            {
                if (szukaj == null)
                    szukaj = new RelayCommand(
                        arg =>
                        {


                            Gksiazki = new ObservableCollection<GrupowaneKsiazki>();
                            foreach (var k in model.GrupowaneKsiazki)
                            {
                                if (k.Tytul.ToLower().IndexOf(AktualnyTytul.ToLower()) != -1)
                                {

                                    Gksiazki.Add(k);
                                }
                            }

                        }
                        ,
                        arg => true
                        );

                return szukaj;
            }

        }

        #endregion

    }
}

