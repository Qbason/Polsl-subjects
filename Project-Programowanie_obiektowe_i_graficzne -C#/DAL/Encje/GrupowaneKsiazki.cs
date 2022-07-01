using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using projektgrupowy.Encje;

namespace projektgrupowy.DAL.Encje
{


    class GrupowaneKsiazki
    {
        //zmienne pod kolumny z pogrupowanych ksiazek wraz z iloscą ich wystąpień pod względem tytułu
        public string Tytul { get; set; }
        public int Ilosc { get; set; }


        //konstrutor do tworzenia obiektu na podstawie MYSQLDATAREADER

        public GrupowaneKsiazki(MySqlDataReader reader)
        {
            Tytul = reader["tytul"].ToString();
            Ilosc = int.Parse(reader["ilosc"].ToString());

        }

        //konstruktor tworzacy obiekt nie dodany jeszcze do bazy z pustym id
        public GrupowaneKsiazki(string tytul, int ilosc)
        {
            Tytul = tytul.Trim();
            Ilosc = ilosc;

        }

        //wyświetlenie obiektu w formie String
        public override string ToString()
        {
            return $"{Tytul}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
