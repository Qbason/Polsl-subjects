using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Encje
{
    class Wypozyczenie
    {
        //zmienne stworzone na poczet kolumn z tabeli Wypozyczenie
        public int? Id_wypozyczenie { get; set; }
        public sbyte Id_czytelnik { get; set; }
        public sbyte Id_pracownik_wydajacy { get; set; }
        public string Data_wydania { get; set; }

        //konstrutor do tworzenia obiektu na podstawie MYSQLDATAREADER

        public Wypozyczenie(MySqlDataReader reader)
        {
            Id_wypozyczenie = int.Parse(reader["id_wypozyczenie"].ToString());
            Id_czytelnik = sbyte.Parse(reader["id_czytelnik"].ToString());
            Id_pracownik_wydajacy = sbyte.Parse(reader["id_pracownik_wydajacy"].ToString());
            Data_wydania = reader["data_wydania"].ToString();

        }

        //konstruktor tworzacy obiekt nie dodany jeszcze do bazy z pustym id
        public Wypozyczenie(int id_wypozyczenie, sbyte id_czytelnik, sbyte id_pracownik_wydajacy, string data_wydania)
        {
            Id_wypozyczenie = null;
            Id_czytelnik = id_czytelnik;
            Id_pracownik_wydajacy = id_pracownik_wydajacy;
            Data_wydania = data_wydania.Trim();

        }
        //tworzenie obiektu wypozyczenie na podstawie innego obiektu wypozyczenie
        public Wypozyczenie(Wypozyczenie wypozyczenie)
        {
            Id_wypozyczenie = null;
            Id_czytelnik = wypozyczenie.Id_czytelnik;
            Id_pracownik_wydajacy = wypozyczenie.Id_pracownik_wydajacy;
            Data_wydania = wypozyczenie.Data_wydania;

        }
        //funkcja odpowiadająca na wyświetlenie obiektu na String
        public override string ToString()
        {
            return $"{Id_czytelnik} {Id_pracownik_wydajacy} {Data_wydania}";
        }
        //funckja przydatna w momencie tworzenia zapytania do bazy danych
        public string ToInsert()
        {
            return $"({Id_wypozyczenie},{Id_czytelnik}, {Id_pracownik_wydajacy}, '{Data_wydania}')";
        }

        //dzięki przeciążeniu tej metody Contains w liście sprawdzi czy dany obiekt do niej należy
        public override bool Equals(object obj)
        {
            //nie porównujemy ID (klucza glownego)
            var wypozyczenie = obj as Wypozyczenie;
            if (wypozyczenie is null) return false;
            if (Id_czytelnik != wypozyczenie.Id_czytelnik) return false;
            if (Id_pracownik_wydajacy != wypozyczenie.Id_pracownik_wydajacy) return false;
            if (Data_wydania.ToLower() != wypozyczenie.Data_wydania.ToLower()) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
