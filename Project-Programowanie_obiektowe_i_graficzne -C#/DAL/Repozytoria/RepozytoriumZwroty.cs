using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace projektgrupowy.DAL.Repozytoria
{
    using Encje;
    class RepozytoriumZwroty
    {
        //zapytania do bazy danych
        #region ZAPYTANIA
        private const string WSZYSTKIE_ZWROTY = "SELECT * FROM zwrot";
        private const string DODAJ_ZWROT = "INSERT INTO `zwrot`(`id_ksiazka`,`id_czytelnik`,`id_pracownik_wydajacy`,`data_wydania`,`id_pracownik_zwrot`,`data_zwrotu`) VALUES ";
        #endregion

        #region metody CRUD

        //funkcja pobierająca wszystkie zwroty z bazy danych i tworząca na podstawie pobranych danych obiekty typu zwrot
        public static List<Zwrot> PobierzWszystkieZwroty()
        {
            List<Zwrot> zwroty = new List<Zwrot>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKIE_ZWROTY, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    zwroty.Add(new Zwrot(reader));

                connection.Close();
            }

            return zwroty;
        }

        //funkcja umożliwająca dodanie obiektu do bazy danych 
        public static bool DodajZwrotDoBazy(Zwrot zwrot)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_ZWROT} {zwrot.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                zwrot.Id_zwrot = (sbyte)command.LastInsertedId;
                connection.Close();

            }
            return stan;

        }

        //funkcja umożliwająca edycję obiektu w bazie danych
        public static bool EdytujZwrotWBazie(Zwrot zwrot, sbyte idzwrot)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {

                string EDYTUJ_ZWROT = $"UPDATE zwrot set id_czytelnik='{zwrot.Id_czytelnik}', id_pracownik_wydajacy='{zwrot.Id_pracownik_wydajacy}, data_wydania={zwrot.Data_wydania}, " +
                    $"id_pracownik_zwrot='{zwrot.Id_pracownik_zwrot}', data_zwrotu = '{zwrot.Data_zwrotu} WHERE id_zwrot = {idzwrot}";

                MySqlCommand command = new MySqlCommand(EDYTUJ_ZWROT, connection);
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
