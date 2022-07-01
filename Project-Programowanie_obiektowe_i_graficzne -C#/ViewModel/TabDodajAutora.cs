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

    class TabDodajAutora : ViewModelBase
    {
        #region Składowe prywatne
        //tworzenie obiektu typu model
        private Model model = null;

        //kolekcja przechowująca autorów
        private ObservableCollection<Autor> autorzy = null;

        //zmienne stworzone na potrzeby propertisow
        private string imie;
        private string nazwisko;
        private string data_urodzenia;

        #endregion

        #region Konstruktory
        //konstuktor 
        public TabDodajAutora(Model model)
        {
            //'import' modelu wraz z autorami z głównego modelu
            this.model = model;
            autorzy = model.Autorzy;
        }
        #endregion

        #region Właściwości
        //właściwości umożliwiają odwołanie do naszych składowych

        //propertis do imienia
        public string Imie
        {
            get { return imie; }
            set
            {
                imie = value;
                onPropertyChanged(nameof(Imie));
            }
        }
        //propertis do nazwiska
        public string Nazwisko
        {
            get { return nazwisko; }
            set
            {
                nazwisko = value;
                onPropertyChanged(nameof(Nazwisko));
            }
        }
        //propetis do daty urodzenia
        public string Data_urodzenia
        {
            get { return data_urodzenia; }
            set
            {
                data_urodzenia = value;
                onPropertyChanged(nameof(Data_urodzenia));
            }
        }

        //propertis kolekcji autorzy 
        public ObservableCollection<Autor> Autorzy
        {
            get { return autorzy; }
            set
            {
                autorzy = value;
                onPropertyChanged(nameof(Autorzy));
            }
        }

        #endregion

        #region Metody
        //metoda resetująca forumlarz do wartości początkowych
        private void CzyscFormularz()
        {
            Imie = "";
            Nazwisko = "";
            Data_urodzenia = "";
        }

        //Icommand realizujący załadowanie wszystkich autorow na nowo
        private ICommand zaladujWszystkieAutorzy = null;
        public ICommand ZaladujWszystkieAutorzy
        {
            get
            {
                if (zaladujWszystkieAutorzy == null)
                    zaladujWszystkieAutorzy = new RelayCommand(
                        arg =>
                        {
                            Autorzy = model.Autorzy;
                        }
                        ,
                        arg => true
                        );

                return zaladujWszystkieAutorzy;
            }
        }

        //icommand realizujący dodawanienowego autora
        private ICommand dodajAutora = null;
        public ICommand DodajAutora
        {
            get
            {
                if (dodajAutora == null)
                    dodajAutora = new RelayCommand(
                        arg =>
                        {

                            //sprawdzamy czy podane dane nie są puste lub czy nie są nulem
                            if (!String.IsNullOrEmpty(Imie) && !String.IsNullOrEmpty(Nazwisko) && !String.IsNullOrEmpty(Data_urodzenia))
                            {

                                //parsowanie czasu, sprawdzamy czy użytkownik popranie podaj data
                                if (DateTime.TryParse(data_urodzenia, out DateTime result))
                                {
                                    string data_urodzenia_result = result.Date.ToString("yyyy.MM.dd");
                                   


                                    //tworzenie obiektu na podstawie danych
                                    var autor = new Autor(imie, nazwisko, data_urodzenia_result);

                                    bool is_valid = true;
                                    foreach (Autor aut in autorzy)
                                    {
                                        //sprawdzanie, czy już nie ma takiego autora
                                        if (aut.Imie.ToLower() == Imie.ToLower() && aut.Nazwisko.ToLower() == Nazwisko.ToLower() && aut.Data_urodzenia == Data_urodzenia)
                                        {
                                            is_valid = false;
                                            break;
                                        }

                                    }
                                    if (is_valid)
                                    {
                                            //dodawanie autora do bazy
                                            if (model.DodajAutoraDoBazy(autor))
                                            {   

                                                CzyscFormularz();
                                                System.Windows.MessageBox.Show("Autor został dodany do bazy!");
                                            }
                                            else
                                            {
                                                System.Windows.MessageBox.Show("Autor nie został dodany do bazy!Błąd 404");
                                            }
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("Taki autor już istnieje!");
                                    }
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("Zły format daty");
                                }
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Puste pole imię lub nazwisko!");
                            }
                        }
                        ,
                        arg => true
                        );

                return dodajAutora;
            }
        }
        #endregion
    }
}
