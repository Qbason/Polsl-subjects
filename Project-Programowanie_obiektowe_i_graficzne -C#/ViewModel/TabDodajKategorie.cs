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

    class TabDodajKategorie : ViewModelBase
    {
        #region Składowe prywatne


        private Model model = null;
        //kolecja na kategorie
        private ObservableCollection<Kategoria> kategorie = null;
        //zmienna prywatana używana przez propertisa
        private string nazwa;

        #endregion

        #region Konstruktory

        public TabDodajKategorie(Model model)
        {
            //'import' modelu wraz z kategoria z modelu
            this.model = model;
            kategorie = model.Kategorie;
        }
        #endregion

        #region Właściwości
        //propertisy
        public string Nazwa
        {
            get { return nazwa; }
            set
            {
                nazwa = value;
                onPropertyChanged(nameof(Nazwa));
            }
        }

        //kolecja zawierająca kategorie
        public ObservableCollection<Kategoria> Kategorie
        {
            get { return kategorie; }
            set
            {
                kategorie = value;
                onPropertyChanged(nameof(Kategorie));
            }
        }

        #endregion

        #region Metody

        private void CzyscFormularz()
        {
            Nazwa = "";
        }
        //ładowanie wszystkich kategorii
        private ICommand zaladujWszystkieKategorie = null;
        public ICommand ZaladujWszystkieKategorie
        {
            get
            {
                if (zaladujWszystkieKategorie == null)
                    zaladujWszystkieKategorie = new RelayCommand(
                        arg =>
                        {
                            Kategorie = model.Kategorie;
                        }
                        ,
                        arg => true
                        );

                return zaladujWszystkieKategorie;
            }
        }

        //icommand odpowiedzialny za dodawanie nowej kategorii
        private ICommand dodajKategorie = null;
        public ICommand DodajKategorie
        {
            get
            {
                if (dodajKategorie == null)
                    dodajKategorie = new RelayCommand(
                        arg =>
                        {
                            
                            //sprawdzanie czy nazwa nie jest pusta
                            if (!String.IsNullOrEmpty(Nazwa))
                            {
                               

                            
                                var kategoria = new Kategoria(nazwa);

                                bool is_valid = true;
                                foreach (Kategoria kat in kategorie){
                                    //sprawdzanie czy nie ma już  takiej kategori
                                   if(kat.Nazwa.ToLower() == Nazwa.ToLower())
                                    {
                                        is_valid = false;
                                        break;
                                    }

                                }
                                if(is_valid)
                                {
                                    //dodawanie kategorii do bazy
                                    if (model.DodajKategorieDoBazy(kategoria))
                                    {
                                        CzyscFormularz();
                                        System.Windows.MessageBox.Show("Kategoria została dodana do bazy!");
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("Kategoria nie została dodana do bazy!Błąd 404");
                                    }
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("Taka kategoria już istnieje!");
                                }

                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Pusta Kategoria!");
                            }
                        }
                        ,
                        arg => true
                        );

                return dodajKategorie;
            }
        }
        #endregion
    }
}
