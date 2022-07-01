using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Encje
{
    class Czytelnik
    {
        //zmienne stworzone w celu odzwierciedlenia kolumn w tabeli
        public sbyte? Id_czytelnik { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Data_urodzenia { get; set; }
        public int Telefon { get; set; }
        public string Email { get; set; }
        public sbyte Id_adres { get; set; }


        //konstrutor do tworzenia obiektu na podstawie MYSQLDATAREADER

        public Czytelnik(MySqlDataReader reader)
        {
           
            Id_czytelnik = sbyte.Parse(reader["id_czytelnik"].ToString());
            Imie = reader["imie"].ToString();
            Nazwisko = reader["nazwisko"].ToString();
            Data_urodzenia = DateTime.Parse(reader["data_urodzenia"].ToString()).Date.ToString("d");
            Telefon = int.Parse(reader["telefon"].ToString());
            Email = reader["email"].ToString();
            Id_adres = sbyte.Parse(reader["id_adres"].ToString());

        }

        //konstruktor tworzacy obiekt nie dodany jeszcze do bazy z pustym id
        public Czytelnik(string imie, string nazwisko, string data_urodzenia, int telefon, string email, sbyte id_adres)
        {
            Id_czytelnik = null;
            Imie = imie.Trim();
            Nazwisko = nazwisko.Trim();
            Data_urodzenia = data_urodzenia;
            Telefon = telefon;
            Email = email.Trim();
            Id_adres = id_adres;

        }

        //tworzenie obiektu na podstawie innego obieku
        public Czytelnik(Czytelnik czytelnik)
        {
            Id_czytelnik = null;
            Imie = czytelnik.Imie;
            Nazwisko = czytelnik.Nazwisko;
            Data_urodzenia = czytelnik.Data_urodzenia;
            Telefon = czytelnik.Telefon;
            Email = czytelnik.Email;
            Id_adres = czytelnik.Id_adres;

        }

        //funkcja umożliwiająca wyświetlenie obiektu w formie String
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} ";
        }

        //metoda generuje string dla INSERT TO(imie,nazwisko,data_urodzenia,telefon,email,id_adres)

        public string ToInsert()
        {
            return $"('{Imie}', '{Nazwisko}', '{Data_urodzenia}', '{Telefon}', '{Email}', '{Id_adres}')";
        }

        //dzięki przeciążeniu tej metody Contains w liście sprawdzi czy dany obiekt do niej należy
        public override bool Equals(object obj)
        {
            //nie porównujemy ID
            var czytelnik = obj as Czytelnik;
            if (czytelnik is null) return false;
            if (Imie.ToLower() != czytelnik.Imie.ToLower()) return false;
            if (Nazwisko.ToLower() != czytelnik.Nazwisko.ToLower()) return false;
            if (Data_urodzenia != czytelnik.Data_urodzenia) return false;
            if (Telefon != czytelnik.Telefon) return false;
            if (Email.ToLower() != czytelnik.Email.ToLower()) return false;
            if (Id_adres != czytelnik.Id_adres) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
