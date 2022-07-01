using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Repozytoria
{
    using Encje;
    class RepozytoriumWydawnictwa
    {

        //zapytania do bazy danych
        #region ZAPYTANIA
        private const string WSZYSTKIE_WYDAWNICTWA = "SELECT * FROM wydawnictwo";
        private const string DODAJ_WYDAWNICTWO = "INSERT INTO `wydawnictwo`(`nazwa`) VALUES ";
        #endregion

        #region metody CRUD

        //funkcja pobierająca wszystkie wydawnictwa z bazy danych i tworząca na podstawie pobranych danych obiekty typu wydawnictwo
        public static List<Wydawnictwo> PobierzWszystkieWydawnictwa()
        {
            List<Wydawnictwo> wydawnictwa = new List<Wydawnictwo>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKIE_WYDAWNICTWA, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    wydawnictwa.Add(new Wydawnictwo(reader));

                connection.Close();
            }

            return wydawnictwa;
        }

        //funkcja umożliwająca dodanie obiektu do bazy danych 
        public static bool DodajWydawnictwoDoBazy(Wydawnictwo wydawnictwo)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_WYDAWNICTWO} {wydawnictwo.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                wydawnictwo.Id_wydawnictwo = (sbyte)command.LastInsertedId;
                connection.Close();

            }
            return stan;

        }

        //funkcja umożliwająca edycję obiektu w bazie danych
        public static bool EdytujWydawnictwoWBazie(Wydawnictwo wydawnictwo, sbyte idwydawnictwo)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {

                string EDYTUJ_WYDAWNICTWO = $"UPDATE wydawnictwo set nazwa='{wydawnictwo.Nazwa}' WHERE id_wydawnictwo = {idwydawnictwo}";

                MySqlCommand command = new MySqlCommand(EDYTUJ_WYDAWNICTWO, connection);
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
