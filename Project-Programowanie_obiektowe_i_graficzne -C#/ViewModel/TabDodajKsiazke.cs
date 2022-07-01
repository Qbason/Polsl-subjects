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

    class TabDodajKsiazke: ViewModelBase
    {

        #region Składowe prywatne
        //stworzenie modelu
        private Model model = null;

        //tworzenie kolecji obiektow
        private ObservableCollection<Ksiazka> ksiazki = null;
        private ObservableCollection<Autor> autorzy = null;
        private ObservableCollection<Wydawnictwo> wydawnictwa = null;
        private ObservableCollection<Kategoria> kategorie = null;

        //zmienne pod propertisy
        private string rok_wydania="2000"; // domyślna wartosc dla roku_wydania ->slider
        private string tytul;
        private Autor biezacyautor = null;
        private Wydawnictwo biezacewydawnictwo = null;
        private Kategoria biezacakategoria = null;
        private Ksiazka aktualniewybranaksiazka = null;
        private int id_zaznaczenia=-1;

        #endregion

        #region Konstruktory

        public TabDodajKsiazke(Model model)
        {
            //'import' modelu wraz z odpowiednimi kolecjami
            this.model = model;
            ksiazki = model.Ksiazki;
            autorzy = model.Autorzy;
            wydawnictwa = model.Wydawnictwa;
            kategorie = model.Kategorie;
        }
        #endregion

        #region Właściwości
        //propertisy umożliwają odniesienie do danych
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


        public string Rok_wydania
        {
            get { return rok_wydania; }
            set
            {
                rok_wydania = value;
                onPropertyChanged(nameof(Rok_wydania));
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



        public ObservableCollection<Ksiazka> Ksiazki
        {
            get { return ksiazki; }
            set
            {
                ksiazki = value;
                onPropertyChanged(nameof(Ksiazki));
            }
        }
        public ObservableCollection<Autor> Autorzy
        {
            get { return autorzy; }
            set
            {
                autorzy = value;
                onPropertyChanged(nameof(Autorzy));
            }
        }
        public ObservableCollection<Wydawnictwo> Wydawnicwta
        {
            get { return wydawnictwa; }
            set
            {
                wydawnictwa = value;
                onPropertyChanged(nameof(Wydawnicwta));
            }
        }
        public ObservableCollection<Kategoria> Kategorie
        {
            get { return kategorie; }
            set
            {
                kategorie = value;
                onPropertyChanged(nameof(Kategorie));
            }
        }


        public Autor BiezacyAutor {
            get
            {
                return biezacyautor;
            }

            set
            {
                biezacyautor = value;
                onPropertyChanged(nameof(BiezacyAutor));

            }
        
        }

        public Wydawnictwo BiezaceWydawnictwo
        {
            get
            {
                return biezacewydawnictwo;
            }

            set
            {
                biezacewydawnictwo = value;
                onPropertyChanged(nameof(BiezaceWydawnictwo));

            }

        }

        public Kategoria BiezacaKategoria
        {
            get
            {
                return biezacakategoria;
            }

            set
            {
                biezacakategoria = value;
                onPropertyChanged(nameof(BiezacaKategoria));

            }

        }
        





        #endregion

        #region Metody
        //czyszcenie formularza
        private void CzyscFormularz()
        {
            
            Tytul = "";
            BiezacyAutor = null;
            BiezaceWydawnictwo = null;
            BiezacaKategoria = null;
            Rok_wydania = "2000";


        }
        //umożliwia załadowanie ksiązek
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
                        }
                        ,
                        arg => true
                        );

                return zaladujWszystkieKsiazki;
            }
        }

        //umożliwia dodanie ksiązki
        private ICommand dodajKsiazke = null;
        public ICommand DodajKsiazke
        {
            get
            {
                if (dodajKsiazke == null)
                    dodajKsiazke = new RelayCommand(
                        arg =>
                        {
                            //jeżeli któreś z pól jest puste
                            if((BiezacyAutor != null) && (BiezaceWydawnictwo != null) && (BiezacaKategoria != null) && (Tytul != "") )
                            {

                                //utworzenie obiektu ksiazka na podstawie podanych danych
                                var ksiazka = new Ksiazka(tytul, (sbyte)BiezacyAutor.Id_autor, (sbyte)BiezaceWydawnictwo.Id_wydawnictwo, (sbyte)BiezacaKategoria.Id_kategoria, rok_wydania);

                                if (model.DodajKsiazkeDoBazy(ksiazka))
                                {
                                    CzyscFormularz();
                                    System.Windows.MessageBox.Show("Książka została dodana do bazy!");
                                }
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Uzupełnij dane!");
                            }
                        }
                        ,
                        arg => (BiezacyAutor != null) && (BiezaceWydawnictwo != null) && (BiezacaKategoria != null) && (Tytul != "")
                        );

                return dodajKsiazke;
            }
        }

        //umożliwia edycję ksiązki w bazie
        private ICommand edytujksiazke = null;
        public ICommand EdytujKsiazke
        {
            get
            {
                if (edytujksiazke == null)
                    edytujksiazke = new RelayCommand(
                        arg =>
                        {
                            //jeżeli dane nie są puste
                            if((BiezacyAutor != null) && (BiezaceWydawnictwo != null) && (BiezacaKategoria != null) && (Tytul != "") && (AktualnieWybranaKsiazka != null))
                            {
                                    //tworzenie obiektu
                                    var ksiazka = new Ksiazka(tytul, (sbyte)BiezacyAutor.Id_autor, (sbyte)BiezaceWydawnictwo.Id_wydawnictwo, (sbyte)BiezacaKategoria.Id_kategoria, rok_wydania);
                                    
                                    //edycja obiektu poprzez zastąpienie go innym
                                    if (model.EdytujKsiazkeWBazie(ksiazka, (int)AktualnieWybranaKsiazka.Id_ksiazka))
                                    {
                                        CzyscFormularz();
                                        System.Windows.MessageBox.Show("Edytcja ksiązki przebiegła pomyślnie");
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("Edytcja ksiązki przebiegła niepomyślnie");
                                }
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Uzupełnij dane!");
                            }
                            
                        }
                        ,
                        arg => (BiezacyAutor != null) && (BiezaceWydawnictwo != null) && (BiezacaKategoria != null) && (Tytul != "")
                        );

                return edytujksiazke;
            }
        }
        //umożliwia wczytanie formularza na podstawie danych z  klikniętego obiektu
        private ICommand wczytajformularz = null;
        public ICommand WczytajFormularz
        {

            get
            {
                if (wczytajformularz == null)
                    wczytajformularz = new RelayCommand(
                        arg =>
                        {
                            if (AktualnieWybranaKsiazka !=null)
                            {
                                id_zaznaczenia = (int)AktualnieWybranaKsiazka.Id_ksiazka;
                                Tytul = AktualnieWybranaKsiazka.Tytul;
                                BiezacyAutor = model.ZnajdzAutoraPoId(AktualnieWybranaKsiazka.Id_autor);
                                BiezaceWydawnictwo = model.ZnajdzWydawnictwoPoId(AktualnieWybranaKsiazka.Id_wydawnictwo);
                                BiezacaKategoria = model.ZnajdzKategoriePoId(AktualnieWybranaKsiazka.Id_kategoria);
                                Rok_wydania = AktualnieWybranaKsiazka.Rok_wydania;
                                
                                
                            }
                        }
                        ,
                        arg => true
                        );


                return wczytajformularz;
            }

        }












        #endregion





    }
}
