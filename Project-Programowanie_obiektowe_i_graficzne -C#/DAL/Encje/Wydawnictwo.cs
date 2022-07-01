using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Encje
{
    class Wydawnictwo
    {
        //nazwy zmiennych odzwierciedlające nazwy zmiennych
        public sbyte? Id_wydawnictwo { get; set; }
        public string Nazwa { get; set; }

        //konstrutor do tworzenia obiektu na podstawie MYSQLDATAREADER

        public Wydawnictwo(MySqlDataReader reader)
        {
            Id_wydawnictwo = sbyte.Parse(reader["id_wydawnictwo"].ToString());
            Nazwa = reader["nazwa"].ToString();

        }

        //konstruktor tworzacy obiekt nie dodany jeszcze do bazy z pustym id
        public Wydawnictwo(string nazwa)
        {
            Id_wydawnictwo = null;
            Nazwa = nazwa.Trim();
        }

        //tworzenie obiektu wydawnictwo na podstawie już istniejącego
        public Wydawnictwo(Wydawnictwo wydawnictwo)
        {
            Id_wydawnictwo = null;
            Nazwa = wydawnictwo.Nazwa;
        }

        //wyświtlenie obiektu w formie String
        public override string ToString()
        {
            return $"{Nazwa}";
        }

        //funkcja stworzona w celu ułatwienia dodawnia polecenia
        public string ToInsert()
        {
            return $"('{Nazwa}')";
        }

        //dzięki przeciążeniu tej metody Contains w liście sprawdzi czy dany obiekt do niej należy
        public override bool Equals(object obj)
        {
            //nie porównujemy ID (klucza glownego)
            var wydawnictwo = obj as Wydawnictwo;
            if (wydawnictwo is null) return false;
            if (Nazwa.ToLower() != wydawnictwo.Nazwa.ToLower()) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
