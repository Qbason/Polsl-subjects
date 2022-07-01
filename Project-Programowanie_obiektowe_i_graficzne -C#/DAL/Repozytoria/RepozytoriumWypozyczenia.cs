using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Repozytoria
{
    using Encje;
    class RepozytoriumWypozyczenia
    {

        //zapytania do bazy danych
        #region ZAPYTANIA
        private const string WSZYSTKIE_WYPOZYCZENIA = "SELECT * FROM wypozyczenie";
        private const string DODAJ_WYPOZYCZENIE = "INSERT INTO `wypozyczenie`(`id_wypozyczenie`,`id_czytelnik`,`id_pracownik_wydajacy`,`data_wydania`) VALUES ";
        #endregion

        #region metody CRUD

        //funkcja pobierająca wszystkie wypozyczenia z bazy danych i tworząca na podstawie pobranych danych obiekty typu wypozyczenie
        public static List<Wypozyczenie> PobierzWszystkieWypozyczenia()
        {
            List<Wypozyczenie> wypozyczenia = new List<Wypozyczenie>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKIE_WYPOZYCZENIA, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    wypozyczenia.Add(new Wypozyczenie(reader));

                connection.Close();
            }

            return wypozyczenia;
        }

        //funkcja umożliwająca dodanie obiektu do bazy danych 
        public static bool DodajWypozyczenieDoBazy(Wypozyczenie wypozyczenie)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_WYPOZYCZENIE} {wypozyczenie.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                wypozyczenie.Id_wypozyczenie = (sbyte)command.LastInsertedId;
                connection.Close();

            }
            return stan;

        }

        //funkcja umożliwająca edycję obiektu w bazie danych
        public static bool EdytujWypozyczenieWBazie(Wypozyczenie wypozyczenie, sbyte idwypozyczenie)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                //id_czytelnik`,`id_pracownik_wydajacy`,`data_wydania
                string EDYTUJ_WYPOZYCZENIE = $"UPDATE wypozyczenie set id_czytelnik='{wypozyczenie.Id_czytelnik}', id_pracownik_wydajacy='{wypozyczenie.Id_pracownik_wydajacy}, data_wydania={wypozyczenie.Data_wydania} WHERE id_wypozyczenie = {idwypozyczenie}";

                MySqlCommand command = new MySqlCommand(EDYTUJ_WYPOZYCZENIE, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) stan = true;
                connection.Close();


            }
            return stan;

        }

        //funkcja umozliwiajaca usuniecie wypozyczenia z bazy danych
        public static bool UsunWypozyczenieZBazy(int id_wypozyczenia)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string USUN_WYPOZYCZENIE = "delete from wypozyczenie where id_wypozyczenie = "+id_wypozyczenia;
                MySqlCommand command = new MySqlCommand(USUN_WYPOZYCZENIE, connection);
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
