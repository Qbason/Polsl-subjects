using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Encje
{
    class Autor
    {

        //tworzenie zmiennych odzwierciedlajace kolumny w tabeli
        public sbyte? Id_autor { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Data_urodzenia { get; set; }

        //konstrutor do tworzenia obiektu na podstawie MYSQLDATAREADER

       
        //umożliwia przypisanie danych na podstawie obiektu MYsqlDataReader
        public Autor(MySqlDataReader reader)
        {
            Id_autor = sbyte.Parse(reader["id_autor"].ToString());
            Imie = reader["imie"].ToString();
            Nazwisko = reader["nazwisko"].ToString();
            Data_urodzenia = DateTime.Parse(reader["data_urodzenia"].ToString()).Date.ToString("yyyy.MM.dd");

        }

        //konstruktor tworzacy obiekt nie dodany jeszcze do bazy z pustym id
        public Autor(string imie, string nazwisko, string data_urodzenia)
        {
            Id_autor = null;
            Imie = imie.Trim();
            Nazwisko = nazwisko.Trim();
            Data_urodzenia = data_urodzenia;

        }
        //tworzenie obiektu tylko z dwoma danymi
        public Autor(string imie, string nazwisko)
        {
            Id_autor = null;
            Imie = imie.Trim();
            Nazwisko = nazwisko.Trim();
           

        }
        //tworzenie obiektu za podstawie już istniejącego
        public Autor(Autor autor)
        {
            Id_autor = null;
            Imie = autor.Imie;
            Nazwisko = autor.Nazwisko;
            Data_urodzenia = autor.Data_urodzenia;

        }
        //metoda realizująca wyświetlenie obiektu 
        public override string ToString()
        {
            return $"{Imie} {Nazwisko}";
        }

        //metoda generuje string dla INSERT TO(ulica,numer domu, numer_mieszkania, miejscowosc, kod_pocztowy)

        public string ToInsert()
        {
            return $"('{Imie}', '{Nazwisko}', '{Data_urodzenia}')";
        }

        //dzięki przeciążeniu tej metody Contains w liście sprawdzi czy dany obiekt do niej należy
        public override bool Equals(object obj)
        {
            //nie porównujemy ID
            var autor = obj as Autor;
            if (autor is null) return false;
            if (Imie.ToLower() != autor.Imie.ToLower()) return false;
            if (Nazwisko.ToLower() != autor.Nazwisko.ToLower()) return false;
            if (Data_urodzenia != autor.Data_urodzenia) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
