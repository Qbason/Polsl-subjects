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

    class TabDodajWydawnictwo : ViewModelBase
    {
        #region Składowe prywatne

        private Model model = null;
        //tworzenie kolekcji wydawnictwa
        private ObservableCollection<Wydawnictwo> wydawnictwa = null;
        //zmienna pod propertis
        private string nazwa;

        #endregion

        #region Konstruktory

        public TabDodajWydawnictwo(Model model)
        {
            //'import' danych:modelu i wydawnictwa
            this.model = model;
            wydawnictwa = model.Wydawnictwa;
        }
        #endregion

        #region Właściwości
        //właściwości umożliwające odniesienie się do zmiennych w programie
        public string Nazwa
        {
            get { return nazwa; }
            set
            {
                nazwa = value;
                onPropertyChanged(nameof(Nazwa));
            }
        }


        public ObservableCollection<Wydawnictwo> Wydawnictwa
        {
            get { return wydawnictwa; }
            set
            {
                wydawnictwa = value;
                onPropertyChanged(nameof(Wydawnictwa));
            }
        }

        #endregion

        #region Metody
        //do czyszczenia formularza
        private void CzyscFormularz()
        {
            Nazwa = "";
        }

        //załadowanie  wydawnictwa
        private ICommand zaladujWszystkieWydawnictwa = null;
        public ICommand ZaladujWszystkieWydawnictwa
        {
            get
            {
                if (zaladujWszystkieWydawnictwa == null)
                    zaladujWszystkieWydawnictwa = new RelayCommand(
                        arg =>
                        {
                            Wydawnictwa = model.Wydawnictwa;
                        }
                        ,
                        arg => true
                        );

                return zaladujWszystkieWydawnictwa;
            }
        }

        //ICommand do dodawania nowego wydawnictwa
        private ICommand dodajWydawnictwo = null;
        public ICommand DodajWydawnictwo
        {
            get
            {
                if (dodajWydawnictwo == null)
                    dodajWydawnictwo = new RelayCommand(
                        arg =>
                        {

                            //sprawdzenie czy nazwa nie jest pusta
                            if (!String.IsNullOrEmpty(Nazwa))
                            {


                                //tworzenie nowego obiektu
                                var wydawnictwo = new Wydawnictwo(nazwa);

                                bool is_valid = true;
                                foreach (Wydawnictwo wyd in wydawnictwa)
                                {
                                    //sprawdzenie czy już takie nie istnieje
                                    if (wyd.Nazwa.ToLower() == Nazwa.ToLower())
                                    {
                                        is_valid = false;
                                        break;
                                    }

                                }
                                if (is_valid)
                                {
                                    //dodawanie do bazy nowego wydawnictwa
                                    if (model.DodajWydawnictwoDoBazy(wydawnictwo))
                                    {
                                        CzyscFormularz();
                                        System.Windows.MessageBox.Show("Wydawnictwo zostało dodana do bazy!");
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("Wydawnictwo nie zostało dodana do bazy!Błąd 404");
                                    }
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("Takie wydawnictwo już istnieje!");
                                }

                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Puste wydawnictwo!");
                            }
                        }
                        ,
                        arg => true
                        );

                return dodajWydawnictwo;
            }
        }
        #endregion
    }
}
