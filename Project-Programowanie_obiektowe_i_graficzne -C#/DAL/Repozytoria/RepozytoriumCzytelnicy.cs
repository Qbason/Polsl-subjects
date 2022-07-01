using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Repozytoria
{
    using Encje;
    class RepozytoriumCzytelnicy
    {
        //zapytania do bazy danych
        #region ZAPYTANIA
        private const string WSZYSCY_CZYTELNICY = "SELECT * FROM czytelnik";
        private const string DODAJ_CZYTELNIKA = "INSERT INTO `czytelnik`(`imie`,`nazwisko`,`data_urodzenia`,`telefon`,`email`,`id_adres`) VALUES ";
        #endregion

        #region metody CRUD
        //funkcja pobierająca wszystkich czytelnikow z bazy danych i tworząca na podstawie pobranych danych obiekty typu czytelnik
        public static List<Czytelnik> PobierzWszystkichCzytelnikow()
        {
            List<Czytelnik> czytelnicy = new List<Czytelnik>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSCY_CZYTELNICY, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    czytelnicy.Add(new Czytelnik(reader));

                connection.Close();
            }

            return czytelnicy;
        }

        //funkcja umożliwająca dodanie obiektu do bazy danych 
        public static bool DodajCzytelnikaDoBazy(Czytelnik czytelnik)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_CZYTELNIKA} {czytelnik.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                czytelnik.Id_czytelnik = (sbyte)command.LastInsertedId;
                connection.Close();

            }
            return stan;

        }

        //funkcja umożliwająca edycję obiektu w bazie danych
        public static bool EdytujCzytelnikaWBazie(Czytelnik czytelnik, sbyte idczytelnik)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {

                string EDYTUJ_CZYTELNIKA = $"UPDATE czytelnik set imie='{czytelnik.Imie}', nazwisko='{czytelnik.Nazwisko}', data_urodzenia={czytelnik.Data_urodzenia}, " +
                    $"telefon='{czytelnik.Telefon}', email = '{czytelnik.Email}', id_adres = {czytelnik.Id_adres} WHERE id_czytelnik = {idczytelnik}";

                MySqlCommand command = new MySqlCommand(EDYTUJ_CZYTELNIKA, connection);
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
