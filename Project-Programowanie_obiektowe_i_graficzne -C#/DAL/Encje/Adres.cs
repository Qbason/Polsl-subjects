using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Encje
{
    class Adres
    {
        
        //Zmienne odpowiadające danym z bazy danych
        public sbyte? Id_adres { get; set; }
        public string Ulica { get; set; }
        public string Numer_domu { get; set; }//do poprawy numer domu powinnien byc  string, bo np. 23A
        public sbyte Numer_mieszkania { get; set; }
        public string Miejscowosc { get; set; }
        public string Kod_pocztowy { get; set; }

        //konstrutor do tworzenia obiektu na podstawie MYSQLDATAREADER

        public Adres(MySqlDataReader reader)
        {
            Id_adres = sbyte.Parse(reader["id_adres"].ToString());
            Ulica = reader["ulica"].ToString();
            Numer_domu = reader["numer_domu"].ToString();
            Numer_mieszkania = sbyte.Parse(reader["numer_mieszkania"].ToString());
            Miejscowosc = reader["miejscowosc"].ToString();
            Kod_pocztowy = reader["kod_pocztowy"].ToString();

        }

        //konstruktor  do tworzenia nowego obiektu na podstawie danych
        public Adres(string ulica, string numer_domu, sbyte numer_mieszkania, string miejscowosc, string kod_pocztowy)
        {
            Id_adres = null;
            Ulica = ulica.Trim();
            Numer_domu = numer_domu.Trim();
            Numer_mieszkania = numer_mieszkania;
            Miejscowosc = miejscowosc.Trim();
            Kod_pocztowy = kod_pocztowy.Trim();

        }

        //tworzenie obiektu na podstawie już istniejącego
        public Adres(Adres adres)
        {
            Id_adres = null;
            Ulica = adres.Ulica;
            Numer_domu = adres.Numer_domu;
            Numer_mieszkania = adres.Numer_mieszkania;
            Miejscowosc = adres.Miejscowosc;
            Kod_pocztowy = adres.Kod_pocztowy;

        }
        //możliwość wyświetlnia obiektu w formie stringu
        public override string ToString()
        {
            return $"{Ulica} {Numer_domu} {Numer_mieszkania} {Miejscowosc} {Kod_pocztowy}";
        }

        //metoda generuje string dla INSERT TO(ulica,numer domu, numer_mieszkania, miejscowosc, kod_pocztowy)

        public string ToInsert()
        {
            return $"('{Ulica}', {Numer_domu},{Numer_mieszkania},'{Miejscowosc}','{Kod_pocztowy}')";
        }

        //dzięki przeciążeniu tej metody Contains w liście sprawdzi czy dany obiekt do niej należy
        public override bool Equals(object obj)
        {
            //nie porównujemy ID (klucza glownego)
            var adres = obj as Adres;
            if (adres is null) return false;
            if (Ulica.ToLower() != adres.Ulica.ToLower()) return false;
            if (Numer_domu.ToLower() != adres.Numer_domu.ToLower()) return false;
            if (Numer_mieszkania != adres.Numer_mieszkania) return false;
            if (Miejscowosc.ToLower() != adres.Miejscowosc.ToLower()) return false;
            if (Kod_pocztowy.ToLower() != adres.Kod_pocztowy.ToLower()) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
