using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projektgrupowy.ViewModel
{
    using projektgrupowy.Model;
    using BaseClass;
    using DAL;

    class MainViewModel
    {
        //stworzenie instacji model
        private Model model = new Model();
        //tworzenie propertisow realizujących funkcje dla poszczególnych zakładek
        public TabListaViewModel TabListaVM { get; set; }
        public TabDodajKsiazke TabDodKsiazke { get; set; }
        public TabDodajKategorie TabDodKategorie { get; set; }
        public TabDodajAutora TabDodAutora { get; set; }
        public TabDodajWydawnictwo TabDodWydawnictwo { get; set; }
        public TabZliczKsiazki TabZliKsiazki { get; set; }
        public TabDodajWypozyczenie TabDodWypozyczenie { get; set; }
        public TabDodajZwrot TabDodZwrot{ get; set; }

       
        //funckja umożliwajaca zwracanie modelu
        protected Model returnmodel()
        {
            return model;
        }

        public MainViewModel()
        {
            //stworzenie viemodeli pomocniczych - dla każdej karty
            //przekazanie referencji do instancji modelu tak
            //aby wszystkie obiekty modeli widoków pracowały na tym samym modelu
            TabListaVM = new TabListaViewModel(model);
            TabDodKsiazke = new TabDodajKsiazke(model);
            TabDodKategorie = new TabDodajKategorie(model);
            TabDodWydawnictwo = new TabDodajWydawnictwo(model);
            TabDodAutora = new TabDodajAutora(model);
            TabZliKsiazki = new TabZliczKsiazki(model);
            TabDodWypozyczenie = new TabDodajWypozyczenie(model);
            TabDodZwrot = new TabDodajZwrot(model);

        }







    }
}
