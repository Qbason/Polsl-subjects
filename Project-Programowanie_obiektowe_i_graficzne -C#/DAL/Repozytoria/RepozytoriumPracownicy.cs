using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Repozytoria
{
    using Encje;
    class RepozytoriumPracownicy
    {

        //zapytania do bazy danych
        #region ZAPYTANIA
        private const string WSZYSCY_PRACOWNICY = "SELECT * FROM pracownik"; 
        private const string DODAJ_PRACOWNIK = "INSERT INTO `pracownik`(`imie`,`nazwisko`,`data_urodzenia`,`wynagrodzenie`,`telefon`,`email`,`id_adres`) VALUES ";
        #endregion

        #region metody CRUD

        //funkcja pobierająca wszystkich pracownikow z bazy danych i tworząca na podstawie pobranych danych obiekty typu pracownik
        public static List<Pracownik> PobierzWszystkiePracownicy()
        {
            List<Pracownik> pracownicy = new List<Pracownik>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSCY_PRACOWNICY, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    pracownicy.Add(new Pracownik(reader));

                connection.Close();
            }

            return pracownicy;
        }

        //funkcja umożliwająca dodanie obiektu do bazy danych 
        public static bool DodajPracownikDoBazy(Pracownik pracownik)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_PRACOWNIK} {pracownik.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                pracownik.Id_pracownik = (sbyte)command.LastInsertedId;
                connection.Close();

            }
            return stan;

        }

        //funkcja umożliwająca edycję obiektu w bazie danych
        public static bool EdytujPracownikWBazie(Pracownik pracownik, sbyte idpracownik)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection) 
            {

                string EDYTUJ_PRACOWNIK = $"UPDATE pracownik set imie='{pracownik.Imie}', nazwisko='{pracownik.Nazwisko}', data_urodzenia='{pracownik.Data_urodzenia}', " +
                    $"wynagrodzenie='{pracownik.Wynagrodzenie}', telefon = '{pracownik.Telefon}', email = '{pracownik.Email}', id_adres = '{pracownik.Id_adres}' WHERE id_pracownik = {idpracownik}";

                MySqlCommand command = new MySqlCommand(EDYTUJ_PRACOWNIK, connection);
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
