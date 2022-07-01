using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Repozytoria
{
    using Encje;
    class RepozytoriumAutorzy
    {
        //zapytania do bazy danych
        #region ZAPYTANIA
        private const string WSZYSCY_AUTORZY = "SELECT * FROM autor";
        private const string DODAJ_AUTORA = "INSERT INTO `autor`(`imie`,`nazwisko`,`data_urodzenia`) VALUES ";
        #endregion

        #region metody CRUD
        //funkcja pobierająca wszystkich autorow z bazy danych i tworząca na podstawie pobranych danych obiekty typu autor
        public static List<Autor> PobierzWszystkichAutorow()
        {
            List<Autor> autorzy = new List<Autor>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSCY_AUTORZY, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    autorzy.Add(new Autor(reader));

                connection.Close();
            }

            return autorzy;
        }

        //funkcja umożliwająca dodanie obiektu do bazy danych 
        public static bool DodajAutoraDoBazy(Autor autor)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_AUTORA} {autor.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                autor.Id_autor = (sbyte)command.LastInsertedId;
                connection.Close();

            }
            return stan;

        }
        //funkcja umożliwająca edycję obiektu w bazie danych
        public static bool EdytujAutoraWBazie(Autor autor, sbyte idautor)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {

                string EDYTUJ_AUTORA = $"UPDATE autor set imie='{autor.Imie}', nazwisko='{autor.Nazwisko}', data_urodzenia={autor.Data_urodzenia} WHERE id_autor = {idautor}";

                MySqlCommand command = new MySqlCommand(EDYTUJ_AUTORA, connection);
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
