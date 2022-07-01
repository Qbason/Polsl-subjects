using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using projektgrupowy.Encje;

namespace projektgrupowy.DAL.Encje
{
   

    class Ksiazka
    {
        //zmienne odzwierciedlające kolumny w tabeli
        public int? Id_ksiazka { get; set; }
        public string Tytul { get; set; }
        public sbyte Id_autor { get; set; }
        public sbyte Id_wydawnictwo { get; set; }
        public sbyte Id_kategoria { get; set; }
        public string Rok_wydania { get; set; }

        //wyświetlenie nazwy autora
        public string Nazwa_autora
        {
            get
            {
                //korzystanie z bazowej encji w celu wyświetlenia nazwy autora zmiast id
                BazowaEncja BE = BazowaEncja.utworzObiekt();
                
                return BE.ZnajdzImie(Id_autor);
                
            }
        }
        //wyświetlenei nazwy wydawnictwa
        public string Nazwa_wydawnictwa
        {
            get
            {   //korzystanie z bazowej encji w celu wyświetlenia nazwy wydawnictwa zamiast id
                BazowaEncja BE = BazowaEncja.utworzObiekt();
                return BE.ZnajdzWydawnictwo(Id_wydawnictwo);
            }
        }
        //wyświetlenie nazwy kategori zamiast id_kategoria
        public string Nazwa_kategorii
        {
            get
            {
                //korzystanie z bazowej encji w celu wyświetlenia nazwy kategorii zamiast id
                BazowaEncja BE = BazowaEncja.utworzObiekt();
                return BE.ZnajdzKategorie(Id_kategoria);
            }
        }



        // ksiazka ma sie  odwołać do funkcji z encjibazowej i tam mu przekazać Id_autora

        //konstrutor do tworzenia obiektu na podstawie MYSQLDATAREADER

        public Ksiazka(MySqlDataReader reader)
        {
            Id_ksiazka = int.Parse(reader["id_ksiazka"].ToString());
            Tytul = reader["tytul"].ToString();
            Id_autor = sbyte.Parse(reader["id_autor"].ToString());
            Id_wydawnictwo = sbyte.Parse(reader["id_wydawnictwo"].ToString());
            Id_kategoria = sbyte.Parse(reader["id_kategoria"].ToString());
            Rok_wydania = (reader["rok_wydania"].ToString());

        }

        //konstruktor tworzacy obiekt nie dodany jeszcze do bazy z pustym id
        public Ksiazka(string tytul, sbyte id_autor, sbyte id_wydawnictwo, sbyte id_kategoria, string rok_wydania)
        {
            Id_ksiazka = null;
            Tytul = tytul.Trim();
            Id_autor = id_autor;
            Id_wydawnictwo = id_wydawnictwo;
            Id_kategoria = id_kategoria;
            Rok_wydania = rok_wydania;

        }
        //tworzenie obiektu ksiązka na podstawie innego obiektu typu ksiazka
        public Ksiazka(Ksiazka ksiazka)
        {
            Id_ksiazka = null;
            Tytul = ksiazka.Tytul;
            Id_autor = ksiazka.Id_autor;
            Id_wydawnictwo = ksiazka.Id_wydawnictwo;
            Id_kategoria = ksiazka.Id_kategoria;
            Rok_wydania = ksiazka.Rok_wydania;

        }

        //wyświetlenia obiektu książka w formie String
        public override string ToString()
        {
            return $"{Id_ksiazka} {Tytul} {Id_autor} {Id_wydawnictwo} {Id_kategoria} {Rok_wydania}";
        }

        //metoda generuje string dla INSERT TO(ulica,numer domu, numer_mieszkania, miejscowosc, kod_pocztowy)

        public string ToInsert()
        {
            return $"('{Tytul}', '{Id_autor}', '{Id_wydawnictwo}', '{Id_kategoria}', '{Rok_wydania}')";
        }

        //dzięki przeciążeniu tej metody Contains w liście sprawdzi czy dany obiekt do niej należy
        public override bool Equals(object obj)
        {
            //nie porównujemy ID
            var ksiazka = obj as Ksiazka;
            if (ksiazka is null) return false;
            if (Tytul.ToLower() != ksiazka.Tytul.ToLower()) return false;
            if (Id_autor != ksiazka.Id_autor) return false;
            if (Id_wydawnictwo != ksiazka.Id_wydawnictwo) return false;
            if (Id_kategoria != ksiazka.Id_kategoria) return false;
            if (Rok_wydania != ksiazka.Rok_wydania) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
