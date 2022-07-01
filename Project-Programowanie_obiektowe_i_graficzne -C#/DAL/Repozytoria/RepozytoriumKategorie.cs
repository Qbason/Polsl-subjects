using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Repozytoria
{
    using Encje;
    class RepozytoriumKategorie
    {
        //zapytania do bazy danych
        #region ZAPYTANIA
        private const string WSZYSTKIE_KATEGORIE = "SELECT * FROM kategoria";
        private const string DODAJ_KATEGORIE = "INSERT INTO `kategoria`(`nazwa`) VALUES ";
        #endregion

        #region metody CRUD

        //funkcja pobierająca wszystkie kategorie z bazy danych i tworząca na podstawie pobranych danych obiekty typu kategoria
        public static List<Kategoria> PobierzWszystkieKategorie()
        {
            List<Kategoria> kategorie = new List<Kategoria>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKIE_KATEGORIE, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    kategorie.Add(new Kategoria(reader));

                connection.Close();
            }

            return kategorie;
        }

        //funkcja umożliwająca dodanie obiektu do bazy danych 
        public static bool DodajKategorieDoBazy(Kategoria kategoria)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_KATEGORIE} {kategoria.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                kategoria.Id_kategoria = (sbyte)command.LastInsertedId;
                connection.Close();

            }
            return stan;

        }

        //funkcja umożliwająca edycję obiektu w bazie danych
        public static bool EdytujKategorieWBazie(Kategoria kategoria, sbyte idkategoria)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {

                string EDYTUJ_KATEGORIE = $"UPDATE kategoria set nazwa='{kategoria.Nazwa}' WHERE id_kategoria = {idkategoria}";

                MySqlCommand command = new MySqlCommand(EDYTUJ_KATEGORIE, connection);
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
