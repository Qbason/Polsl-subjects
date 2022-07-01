using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Encje
{
    class Zwrot

    {
        //zmienne stworzone w celu odzwierciedlenia kolumn z tabeli Zwrot
        public sbyte? Id_zwrot { get; set; }
        public int Id_ksiazka { get; set; }
        public sbyte Id_czytelnik { get; set; }
        public sbyte Id_pracownik_wydajacy { get; set; }
        public string Data_wydania { get; set; }
        public sbyte Id_pracownik_zwrot { get; set; }
        public string Data_zwrotu  { get; set; }

        //konstrutor do tworzenia obiektu na podstawie MYSQLDATAREADER

        public Zwrot(MySqlDataReader reader)
        {
            Id_zwrot = sbyte.Parse(reader["id_zwrot"].ToString());
            Id_ksiazka = int.Parse(reader["id_ksiazka"].ToString());
            Id_czytelnik = sbyte.Parse(reader["id_czytelnik"].ToString());
            Id_pracownik_wydajacy = sbyte.Parse(reader["id_pracownik_wydajacy"].ToString());
            Data_wydania = reader["data_wydania"].ToString();
            Id_pracownik_zwrot = sbyte.Parse(reader["id_pracownik_zwrot"].ToString());
            Data_zwrotu = reader["data_zwrotu"].ToString();
        }

        //konstrukto tworzacy obiekt nie dodany jeszcze do bazy z pustym id
        public Zwrot(int id_ksiazka, sbyte id_czytelnik, sbyte id_pracownik_wydajacy, string data_wydania, sbyte id_pracownik_zwrot, string data_zwrotu)
        {
            Id_zwrot = null;
            Id_ksiazka = id_ksiazka;
            Id_czytelnik = id_czytelnik;
            Id_pracownik_wydajacy = id_pracownik_wydajacy;
            Data_wydania = data_wydania.Trim();
            Id_pracownik_zwrot = id_pracownik_zwrot;
            Data_zwrotu = data_zwrotu.Trim();
        }

        //tworzenia obiektu Zwrot na podstawie innego obiektu Zwrot
        public Zwrot(Zwrot zwrot)
        {
            Id_zwrot = null;
            Id_ksiazka = zwrot.Id_ksiazka;
            Id_czytelnik = zwrot.Id_czytelnik;
            Id_pracownik_wydajacy = zwrot.Id_pracownik_wydajacy;
            Data_wydania = zwrot.Data_wydania;
            Id_pracownik_zwrot = zwrot.Id_pracownik_zwrot;
            Data_zwrotu = zwrot.Data_zwrotu;
        }

        //wyświetlanie obiektu w formie String'a
        public override string ToString()
        {
            return $"{Id_ksiazka} {Id_czytelnik} {Id_pracownik_wydajacy} {Data_wydania} {Id_pracownik_zwrot} {Data_zwrotu}";
        }
        //funckja przydatna, gdy relizujemy na niej polecenie w bazie danych
        public string ToInsert()
        {
            return $"({Id_ksiazka}, {Id_czytelnik}, {Id_pracownik_wydajacy}, '{Data_wydania}', {Id_pracownik_zwrot}, '{Data_zwrotu}')";
        }

        public override bool Equals(object obj)
        {
            var zwrot = obj as Zwrot;
            if (zwrot is null) return false;
            if (Id_ksiazka != zwrot.Id_ksiazka) return false;
            if (Id_czytelnik != zwrot.Id_czytelnik) return false;
            if (Id_pracownik_wydajacy != zwrot.Id_pracownik_wydajacy) return false;
            if (Data_wydania.ToLower() != zwrot.Data_wydania.ToLower()) return false;
            if (Id_pracownik_zwrot != zwrot.Id_pracownik_zwrot) return false;
            if (Data_zwrotu.ToLower() != zwrot.Data_zwrotu.ToLower()) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
