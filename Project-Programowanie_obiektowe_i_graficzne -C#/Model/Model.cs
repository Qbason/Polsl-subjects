using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektgrupowy.Model
{
    using DAL.Encje;
    using DAL.Repozytoria;
    using System.Collections.ObjectModel;
    class Model
    {
        //stan bazy
        public ObservableCollection<Adres> Adresy { get; set; } = new ObservableCollection<Adres>();
        public ObservableCollection<Autor> Autorzy { get; set; } = new ObservableCollection<Autor>();
        public ObservableCollection<Czytelnik> Czytelnicy { get; set; } = new ObservableCollection<Czytelnik>();
        public ObservableCollection<Kategoria> Kategorie { get; set; } = new ObservableCollection<Kategoria>();
        public ObservableCollection<Ksiazka> Ksiazki { get; set; } = new ObservableCollection<Ksiazka>();
        public ObservableCollection<Ksiazka> WolneKsiazki { get; set; } = new ObservableCollection<Ksiazka>();
        public ObservableCollection<GrupowaneKsiazki> GrupowaneKsiazki { get; set; } = new ObservableCollection<GrupowaneKsiazki>();
        public ObservableCollection<Ksiazka> WypozyczoneKsiazki { get; set; } = new ObservableCollection<Ksiazka>();
        public ObservableCollection<Pracownik> Pracownicy { get; set; } = new ObservableCollection<Pracownik>();
        public ObservableCollection<Wydawnictwo> Wydawnictwa { get; set; } = new ObservableCollection<Wydawnictwo>();
        public ObservableCollection<Wypozyczenie> Wypozyczenia { get; set; } = new ObservableCollection<Wypozyczenie>();
        public ObservableCollection<Zwrot> Zwroty { get; set; } = new ObservableCollection<Zwrot>();



        public Model()
        {
            //pobranie dabych z bazy do kolekcji
            var adresy = RepozytoriumAdresy.PobierzWszystkieAdresy();
            foreach (var o in adresy)
                Adresy.Add(o);

            var autorzy = RepozytoriumAutorzy.PobierzWszystkichAutorow();
            foreach (var o in autorzy)
                Autorzy.Add(o);

            var czytelnicy = RepozytoriumCzytelnicy.PobierzWszystkichCzytelnikow();
            foreach (var o in czytelnicy)
                Czytelnicy.Add(o);

            var kategorie = RepozytoriumKategorie.PobierzWszystkieKategorie();
            foreach (var o in kategorie)
                Kategorie.Add(o);

            var ksiazki = RepozytoriumKsiazki.PobierzWszystkieKsiazki();
            foreach (var o in ksiazki)
                Ksiazki.Add(o);

            var wolneksiazki = RepozytoriumKsiazki.PobierzWszystkieWolneKsiazki();
            foreach (var o in wolneksiazki)
                WolneKsiazki.Add(o);

            var grupowaneksiazki = RepozytoriumKsiazki.PobierzZgrupowane();
            foreach (var o in grupowaneksiazki)
                GrupowaneKsiazki.Add(o);

            var wypozyczoneksiazki = RepozytoriumKsiazki.PobierzWszystkieWypozyczoneKsiazki();
            foreach (var o in wypozyczoneksiazki)
                WypozyczoneKsiazki.Add(o);

            var pracownicy = RepozytoriumPracownicy.PobierzWszystkiePracownicy();
            foreach (var o in pracownicy)
                Pracownicy.Add(o);

            var wydawnictwa = RepozytoriumWydawnictwa.PobierzWszystkieWydawnictwa();
            foreach (var o in wydawnictwa)
                Wydawnictwa.Add(o);

            var wypozyczenia = RepozytoriumWypozyczenia.PobierzWszystkieWypozyczenia();
            foreach (var o in wypozyczenia)
                Wypozyczenia.Add(o);

            var zwroty = RepozytoriumZwroty.PobierzWszystkieZwroty();
            foreach (var o in zwroty)
                Zwroty.Add(o);

        }




        private Ksiazka ZnajdzKsiazkePoId(int id)
        {
            foreach (var t in Ksiazki)
            {
                if (t.Id_ksiazka == id)
                    return t;
            }
            return null;
        }

        private Czytelnik ZnajdzCzytelnikaPoId(sbyte id)
        {
            foreach (var o in Czytelnicy)
            {
                if (o.Id_czytelnik == id)
                    return o;
            }
            return null;
        }

        public Autor ZnajdzAutoraPoId(sbyte id)
        {
            foreach (var o in Autorzy)
            {
                if (o.Id_autor == id)
                    return o;
            }
            return null;
        }

        public Wydawnictwo ZnajdzWydawnictwoPoId(sbyte id)
        {
            foreach (var o in Wydawnictwa)
            {
                if (o.Id_wydawnictwo == id)
                    return o;
            }
            return null;
        }

        public Kategoria ZnajdzKategoriePoId(sbyte id)
        {
            foreach (var o in Kategorie)
            {
                if (o.Id_kategoria == id)
                    return o;
            }
            return null;
        }

        public ObservableCollection<Ksiazka> PobierzKsiazkiCzytelnika(Czytelnik czytelnik)
        {
            var ksiazki = new ObservableCollection<Ksiazka>();
          
            foreach (var wypozyczenie in Wypozyczenia)
            {
                if (wypozyczenie.Id_czytelnik == czytelnik.Id_czytelnik )
                {
                    ksiazki.Add(ZnajdzKsiazkePoId((int)wypozyczenie.Id_wypozyczenie));
                }
            }

            return ksiazki;
        }

        public bool DodajKsiazkeDoBazy(Ksiazka ksiazka)
        {
            if (RepozytoriumKsiazki.DodajKsiazkeDoBazy(ksiazka))
            {
                Ksiazki.Add(ksiazka);
                return true;
            }
            return false;
        }

        public bool DodajKategorieDoBazy(Kategoria kategoria)
        {
            if (RepozytoriumKategorie.DodajKategorieDoBazy(kategoria))
            {
                Kategorie.Add(kategoria);
                return true;
            }
            return false;
        }




        public bool DodajWydawnictwoDoBazy(Wydawnictwo wydawnictwo)
        {
            if (RepozytoriumWydawnictwa.DodajWydawnictwoDoBazy(wydawnictwo))
            {
                Wydawnictwa.Add(wydawnictwo);
                return true;
            }
            return false;
        }

        public bool DodajAutoraDoBazy(Autor autor)
        {
            if (RepozytoriumAutorzy.DodajAutoraDoBazy(autor))
            {
                Autorzy.Add(autor);
                return true;
            }
            return false;
        }

        public bool DodajWypozyczenieDoBazy(Wypozyczenie wypozyczenie)
        {
            if (RepozytoriumWypozyczenia.DodajWypozyczenieDoBazy(wypozyczenie))
            {
                Wypozyczenia.Add(wypozyczenie);
                var wolneksiazki = RepozytoriumKsiazki.PobierzWszystkieWolneKsiazki();
                foreach (var o in wolneksiazki)
                    WolneKsiazki.Add(o);
                return true;
            }
            return false;
        }

        public bool DodajZwrotDoBazy(Zwrot zwrot)
        {
            if (RepozytoriumZwroty.DodajZwrotDoBazy(zwrot))
            {
                Zwroty.Add(zwrot);
                
                return true;
            }
            return false;
        }

        public bool EdytujKsiazkeWBazie(Ksiazka ksiazka, int idksiazki)
        {
            if (RepozytoriumKsiazki.EdytujKsiazkeWBazie(ksiazka, idksiazki))
            {
                for(int i=0; i<Ksiazki.Count;i++)
                {
                    if(Ksiazki[i].Id_ksiazka == idksiazki)
                    {
                        ksiazka.Id_ksiazka = idksiazki;
                        Ksiazki[i] = new Ksiazka(ksiazka);
                    }

                }
                return true;
            }
            return false;


        }

        public bool UsunWypozyczenieZBazy(int id_wypozyczenia)
        {
            if (RepozytoriumWypozyczenia.UsunWypozyczenieZBazy(id_wypozyczenia))
            {
                var wypozyczoneksiazki = RepozytoriumKsiazki.PobierzWszystkieWypozyczoneKsiazki();
                foreach (var o in wypozyczoneksiazki)
                    WypozyczoneKsiazki.Add(o);

                return true;
            }
            return false;


        }









    }
}
