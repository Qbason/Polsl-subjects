using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Encje
{
    class Kategoria
    {
        //zmienne tworzone w celu odzwierciedlenia kolumn z tabeli Kategoria
        public sbyte? Id_kategoria { get; set; }
        public string Nazwa { get; set; }


        //konstrutor do tworzenia obiektu na podstawie MYSQLDATAREADER

        public Kategoria(MySqlDataReader reader)
        {
            Id_kategoria = sbyte.Parse(reader["id_kategoria"].ToString());
            Nazwa = reader["nazwa"].ToString();


        }

        //konstruktor tworzacy obiekt nie dodany jeszcze do bazy z pustym id
        public Kategoria(string nazwa)
        {
            Id_kategoria = null;
            Nazwa = nazwa.Trim();

        }

        //tworznie obiektu kategoria na podstawie innego
        public Kategoria(Kategoria kategoria)
        {
            Id_kategoria = null;
            Nazwa = kategoria.Nazwa;

        }

        //wyświetlanie obiektu w formie String
        public override string ToString()
        {
            return $"{Nazwa}";
        }

        //metoda generuje string dla INSERT TO(ulica,numer domu, numer_mieszkania, miejscowosc, kod_pocztowy)

        public string ToInsert()
        {
            return $"('{Nazwa}')";
        }

        //dzięki przeciążeniu tej metody Contains w liście sprawdzi czy dany obiekt do niej należy
        public override bool Equals(object obj)
        {
            //nie porównujemy ID
            var kategoria = obj as Kategoria;
            if (kategoria is null) return false;
            if (Nazwa.ToLower() != kategoria.Nazwa.ToLower()) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
