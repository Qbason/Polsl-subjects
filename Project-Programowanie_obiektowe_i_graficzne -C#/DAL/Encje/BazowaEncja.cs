using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektgrupowy.Encje
{
    using projektgrupowy.ViewModel;
    using projektgrupowy.Model;
    using projektgrupowy.DAL.Encje;
    
    //pobranie składowych z funkcji nadrzędnej poprzez dziedziczenie
    //Klasa została stworzona w celu odczytania przetworzenia informacji id na konkrętną daną
    class BazowaEncja:MainViewModel
    {
        //Tworzenie modelu
        private static Model model = null;
        //Zmienna stworzona w celu tworzenia tylko jednego obietku na poczet SINGLETONU
        private static BazowaEncja _obiekt;

        //konstruktor prywatny, umożliwający wczytanie modelu na poczet Bazowej encji
        
        private BazowaEncja()
        {
           model = returnmodel();
        }

        //przechwycenie modelu, który zawiera tablice
        //funkcja stworzona w celu działania tylko na jednym obiekcie
        public static BazowaEncja utworzObiekt()
        {
            if(_obiekt == null)
            {
                _obiekt = new BazowaEncja();
            }
            return _obiekt;

        }
        
        //funkcja szukająca imie wraz z nazwiskiem po numerze id_autora
        public string ZnajdzImie(int id)
        {
            
            foreach (var a in model.Autorzy)
            {
                if (a.Id_autor == id)
                    return a.Imie +" "+ a.Nazwisko;
            }
            return "";


        }

        //funkcja zwracająca nazwę wydawnictwa po jego id
        public string ZnajdzWydawnictwo(int id)
        {
            foreach (var a in model.Wydawnictwa)
            {
                if (a.Id_wydawnictwo == id)
                    return a.Nazwa;
            }
            return "";


        }
        // funkcja szuka nazwy kategori po wcześniej podanym id_kategorii
        public string ZnajdzKategorie(int id)
        {
            foreach (var a in model.Kategorie)
            {
                if (a.Id_kategoria == id)
                    return a.Nazwa;
            }
            return "";


        }












    }
    


}
