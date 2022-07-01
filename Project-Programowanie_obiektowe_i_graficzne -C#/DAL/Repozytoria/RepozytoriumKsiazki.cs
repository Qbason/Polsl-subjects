using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektgrupowy.DAL.Repozytoria
{
    using Encje;
    class RepozytoriumKsiazki
    {

        //zapytania do bazy danych
        #region ZAPYTANIA
        private const string WSZYSTKIE_KSIAZKI = "SELECT * FROM ksiazka";
        private const string DODAJ_KSIAZKE = "INSERT INTO `ksiazka`(`tytul`,`id_autor`,`id_wydawnictwo`,`id_kategoria`,`rok_wydania`) VALUES ";
        private const string GRUPOWANE_KSIAZKI = "select tytul, count(*) as ilosc from ksiazka where ksiazka.id_ksiazka not in (select id_wypozyczenie from wypozyczenie) group by tytul";
        private const string KSIAZKI_WOLNE = "select * from ksiazka where ksiazka.id_ksiazka not in (select id_wypozyczenie from wypozyczenie)";
        private const string KSIAZKI_WYPOZYCZONE = "select * from ksiazka where ksiazka.id_ksiazka in (select id_wypozyczenie from wypozyczenie)";

        #endregion

        #region metody CRUD

        //funkcja pobierająca wszystkie wypozyczone ksiazki z bazy danych i tworząca na podstawie pobranych danych obiekty typu ksiazka
        public static List<Ksiazka> PobierzWszystkieWypozyczoneKsiazki()
        {
            List<Ksiazka> ksiazki = new List<Ksiazka>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(KSIAZKI_WYPOZYCZONE, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    ksiazki.Add(new Ksiazka(reader));

                connection.Close();
            }

            return ksiazki;
        }

        //funkcja pobierająca wszystkie niewypozyczone ksiazki z bazy danych i tworząca na podstawie pobranych danych obiekty typu ksiazka
        public static List<Ksiazka> PobierzWszystkieWolneKsiazki()
        {
            List<Ksiazka> ksiazki = new List<Ksiazka>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(KSIAZKI_WOLNE, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    ksiazki.Add(new Ksiazka(reader));

                connection.Close();
            }

            return ksiazki;
        }

        //funkcja pobierająca wszystkie ksiazki z bazy danych i tworząca na podstawie pobranych danych obiekty typu ksiazka
        public static List<Ksiazka> PobierzWszystkieKsiazki()
        {
            List<Ksiazka> ksiazki = new List<Ksiazka>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKIE_KSIAZKI, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    ksiazki.Add(new Ksiazka(reader));

                connection.Close();
            }

            return ksiazki;
        }

        //funkcja wczytujaca zgrupowane ksiazki i liczbe ksiazek o danych tytulach, a nastepnie tworzaca na podstawie pobranych danych obiekty typu ksiazka
        public static List<GrupowaneKsiazki> PobierzZgrupowane()
        {
            List<GrupowaneKsiazki> grupowaneksiazki = new List<GrupowaneKsiazki>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GRUPOWANE_KSIAZKI, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    grupowaneksiazki.Add(new GrupowaneKsiazki(reader));
                

                connection.Close();
            }

            return grupowaneksiazki;
        }

        //funkcja umożliwająca dodanie obiektu do bazy danych
        public static bool DodajKsiazkeDoBazy(Ksiazka ksiazka)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ_KSIAZKE} {ksiazka.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                ksiazka.Id_ksiazka = (sbyte)command.LastInsertedId;
                connection.Close();

            }
            return stan;

        }

        //funkcja umożliwająca edycję obiektu w bazie danych
        public static bool EdytujKsiazkeWBazie(Ksiazka ksiazka, int idksiazka)
        {
            bool stan = false;
            using (var connection = DBConnection.Instance.Connection)
            {

                string EDYTUJ_KSIAZKE = $"UPDATE ksiazka set tytul='{ksiazka.Tytul}', id_autor='{ksiazka.Id_autor}', id_wydawnictwo='{ksiazka.Id_wydawnictwo}', " +
                    $"id_kategoria='{ksiazka.Id_kategoria}', rok_wydania = '{ksiazka.Rok_wydania}' WHERE id_ksiazka = {idksiazka}";

                MySqlCommand command = new MySqlCommand(EDYTUJ_KSIAZKE, connection);
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
