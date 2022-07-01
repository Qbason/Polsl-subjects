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

    class TabDodajZwrot : ViewModelBase
    {

        #region Składowe prywatne
        //model przechowujacy dane z głównego modelu
        private Model model = null;
        //kolekcje przygotowane na obiekty
        private ObservableCollection<Wypozyczenie> wypozyczenia = null;
        private ObservableCollection<Ksiazka> wypozyczoneksiazki = null;
        private ObservableCollection<Pracownik> pracownicy = null;

        //zmienne przygotowane na propertisy
        private Ksiazka aktualniewybranaksiazka;
        private Pracownik biezacypracownik;

        #endregion

        #region Konstruktory

        public TabDodajZwrot(Model model)
        {
            //'import' danych z modelu i wypozyczoneksiazki,wypozyczenia,pracownicy
            this.model = model;
            wypozyczoneksiazki = model.WypozyczoneKsiazki;
            wypozyczenia = model.Wypozyczenia;
            pracownicy = model.Pracownicy;
        }
        #endregion

        #region Właściwości
        //propertisy umożliwajace dostęp do danych, wraz z ich aktualizowaniem w wygladzie!
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

      





        public ObservableCollection<Ksiazka> WypozyczoneKsiazki
        {
            get { return wypozyczoneksiazki; }
            set
            {
                wypozyczoneksiazki = value;
                onPropertyChanged(nameof(WypozyczoneKsiazki));
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
        //czyszczenie formularza
        private void CzyscFormularz()
        {

            BiezacyPracownik = null;
            AktualnieWybranaKsiazka = null;


        }


        //dodawanie zwrotu
        private ICommand dodajZwrot = null;
        public ICommand DodajZwrot
        {
            get
            {
                if (dodajZwrot == null)
                    dodajZwrot = new RelayCommand(
                        arg =>
                        {
                            //sprawdzenie czy zmienna nie jest pusta
                            if (BiezacyPracownik != null & AktualnieWybranaKsiazka != null)
                            {
                                sbyte id_czytelnika = -1;
                                sbyte id_pracownik_wydajacy_ = -1;
                                string data_wydania = "";

                                foreach(Wypozyczenie wyp in wypozyczenia)
                                {
                                    //szukanie wypozyczenia przypisanego do ksiazki
                                    if(wyp.Id_wypozyczenie == AktualnieWybranaKsiazka.Id_ksiazka)
                                    {
                                        id_czytelnika = wyp.Id_czytelnik;
                                        id_pracownik_wydajacy_ = wyp.Id_pracownik_wydajacy;
                                        data_wydania = wyp.Data_wydania;
                                    }
                                }
                                //parsowanie danych
                                data_wydania = DateTime.Parse(data_wydania).ToString("yyyy-M-dd HH:mm:ss");

                                var zwrot = new Zwrot((int)aktualniewybranaksiazka.Id_ksiazka, id_czytelnika, id_pracownik_wydajacy_,data_wydania,(sbyte)biezacypracownik.Id_pracownik, DateTime.Now.ToString("yyyy-M-dd HH:mm:ss"));
                                
                                //dodawanie do bazy
                                if (model.DodajZwrotDoBazy(zwrot))
                                {

                                    //usuwanie z bazy wypozyczenia
                                    if(model.UsunWypozyczenieZBazy((int)aktualniewybranaksiazka.Id_ksiazka))
                                    {
                                        WypozyczoneKsiazki = model.WypozyczoneKsiazki;
                                        CzyscFormularz();
                                        System.Windows.MessageBox.Show("Zwrot przebiegł pomyślnie");
                                    }
                                    
                                }
                            }
                        }
                        ,
                        arg => true
                        );

                return dodajZwrot;
            }
        }


        #endregion





    }
}
