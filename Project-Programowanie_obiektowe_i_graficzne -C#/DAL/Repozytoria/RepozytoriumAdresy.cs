using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Repozytoria
{
    using Encje;
    class RepozytoriumAdresy
    {
        //zapytania do bazy danych
        #region ZAPYTANIA
        private const string WSZYSTKIE_ADRESY = "SELECT * FROM adres";
        private const string DODAJ_ADRES = "INSERT INTO `adres`(`ulica`,`numer_domu`,`numer_mieszkania`,`miejscowosc`,`kod_pocztowy`) VALUES ";
        #endregion

        #region metody CRUD
        //funkcja pobierająca wszystkie adresy z bazy danych i tworząca na podstawie pobranych danych obiekty typu adres
        public static List<Adres> PobierzWszystkieAdresy()
        {
            List<Adres> adresy = new List<Adres>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKIE_ADRESY, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    adresy.Add(new Adres(reader));

                connection.Close();
            }

            return adresy;
        }

        //funkcja umożliwająca dodanie obiektu do bazy danych 
        public static bool DodajAdresDoBazy(Adres adres)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_ADRES} {adres.ToInsert()}",connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                adres.Id_adres = (sbyte)command.LastInsertedId;
                connection.Close();

            }
            return stan;

        }
        //funkcja umożliwająca edycję obiektu w bazie danych
        public static bool EdytujAdresWBazie(Adres adres, sbyte idadres)
        {
            bool stan = false;
            using(var connection = DBConnection.Instance.Connection)
            {

                string EDYTUJ_ADRES = $"UPDATE adres set ulica='{adres.Ulica}', numer_domu='{adres.Numer_domu}', numer_mieszkania='{adres.Numer_mieszkania}', " +
                    $"miejscowosc='{adres.Miejscowosc}', kod_pocztowy = '{adres.Kod_pocztowy}' WHERE id_adres = {idadres}";

                MySqlCommand command = new MySqlCommand(EDYTUJ_ADRES, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) stan = true;
                connection.Close();


            }
            return stan;

        }


        #endregion







    }
}
