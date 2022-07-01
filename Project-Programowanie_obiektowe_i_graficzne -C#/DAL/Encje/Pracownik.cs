using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Encje
{
    class Pracownik
    {
        //zmienne pod kolumny z tabeli
        public sbyte? Id_pracownik { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Data_urodzenia { get; set; }
        public int Wynagrodzenie { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public sbyte Id_adres { get; set; }
        //konstrutor do tworzenia obiektu na podstawie MYSQLDATAREADER

        public Pracownik(MySqlDataReader reader)
        {
            Id_pracownik = sbyte.Parse(reader["id_pracownik"].ToString());
            Imie = reader["imie"].ToString();
            Nazwisko = reader["nazwisko"].ToString();
            Data_urodzenia = DateTime.Parse(reader["data_urodzenia"].ToString()).Date.ToString("d");
            Wynagrodzenie = int.Parse(reader["wynagrodzenie"].ToString());
            Telefon = (reader["telefon"].ToString());
            Email = reader["email"].ToString();
            Id_adres = sbyte.Parse(reader["id_adres"].ToString());

        }

        //konstruktor tworzacy obiekt nie dodany jeszcze do bazy z pustym id
        public Pracownik(string imie, string nazwisko, string data_urodzenia, int wynagrodzenie, string telefon, string email, sbyte id_adres)
        {
            Id_pracownik = null;
            Imie = imie.Trim();
            Imie = imie.Trim();
            Imie = imie.Trim();
            Wynagrodzenie = wynagrodzenie;
            Telefon = telefon;
            Email = email.Trim();
            Id_adres = id_adres;

        }

        //tworzenie obiektu pracownik na podstawie obiektu pracownik
        public Pracownik(Pracownik pracownik)
        {
            Id_pracownik = null;
            Imie = pracownik.Imie;
            Nazwisko = pracownik.Nazwisko;
            Data_urodzenia = pracownik.Data_urodzenia;
            Wynagrodzenie = pracownik.Wynagrodzenie;
            Telefon = pracownik.Telefon;
            Email = pracownik.Email;
            Id_adres = pracownik.Id_adres;

        }
        //funkcja umożliwjąca wyświetlenie obiektu w formie String
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} ";
        }

        
        public string ToInsert()
        {
            return $"('{Imie}', '{Nazwisko}', '{Data_urodzenia}', {Wynagrodzenie}, {Telefon}, '{Email}', {Id_adres})";
        }

        //dzięki przeciążeniu tej metody Contains w liście sprawdzi czy dany obiekt do niej należy
        public override bool Equals(object obj)
        {
            //nie porównujemy ID (klucza glownego)
            var pracownik = obj as Pracownik;
            if (pracownik is null) return false;
            if (Imie.ToLower() != pracownik.Imie.ToLower()) return false;
            if (Nazwisko.ToLower() != pracownik.Nazwisko.ToLower()) return false;
            if (Data_urodzenia.ToLower() != pracownik.Data_urodzenia.ToLower()) return false;
            if (Wynagrodzenie != pracownik.Wynagrodzenie) return false;
            if (Telefon != pracownik.Telefon) return false;
            if (Email.ToLower() != pracownik.Email.ToLower()) return false;
            if (Id_adres != pracownik.Id_adres) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
