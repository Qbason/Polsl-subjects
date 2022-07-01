-- MySQL dump 10.13  Distrib 8.0.23, for Win64 (x86_64)
--
-- Host: 78.157.187.16    Database: biblioteka
-- ------------------------------------------------------
-- Server version	8.0.25

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `czytelnik`
--

DROP TABLE IF EXISTS `czytelnik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `czytelnik` (
  `id_czytelnik` int unsigned NOT NULL AUTO_INCREMENT,
  `imie` varchar(30) DEFAULT NULL,
  `nazwisko` varchar(40) DEFAULT NULL,
  `data_urodzenia` date DEFAULT NULL,
  `telefon` int DEFAULT NULL,
  `email` varchar(60) DEFAULT NULL,
  `id_adres` int unsigned DEFAULT NULL,
  PRIMARY KEY (`id_czytelnik`),
  KEY `id_adres` (`id_adres`),
  CONSTRAINT `czytelnik_ibfk_1` FOREIGN KEY (`id_adres`) REFERENCES `adres` (`id_adres`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `czytelnik`
--

LOCK TABLES `czytelnik` WRITE;
/*!40000 ALTER TABLE `czytelnik` DISABLE KEYS */;
INSERT INTO `czytelnik` VALUES (1,'Karol','Cieciura','1998-03-03',502352234,'karol@gmail.com',1),(2,'Zbigniew','Stonoga','1987-03-08',654231623,'ludzkas@o2.pl',12),(3,'Damian','Ludwig','1998-10-10',552341249,'damian@gmail.com',2),(4,'Jakub','Kościelny','1999-04-02',612351731,'jakub@gmail.com',3),(5,'Marcin','Grajoszek','1985-02-05',213704020,'marcin@gmail.com',4),(6,'Karina','Biała','2000-03-28',602352952,'babajaga@gmail.com',5),(7,'Marian','Rogal','1970-06-06',123564321,'rogalddl@interia.pl',6),(8,'Robert','Prawondowski','1963-12-31',643132156,'prawy@02.pl',8),(9,'Johnny','Sins','1988-10-15',212313414,'łysy@interia.pl',9),(10,'Maria','Konopna','1955-04-13',961942932,'marysiazakazana@onet.pl',10),(11,'Maciej','Szeryf','1995-05-03',997997997,'szeryfmaciej@o2.pl',11),(12,'Jarosław','Ziobro','1965-02-12',634132132,'rodzina@onet.pl',13),(13,'Mateusz','Tusk','1958-07-14',745234632,'miskaryzu@interia.pl',2),(14,'Sebastian','Fabian','1986-06-23',612351673,'fabis@gmail.com',5),(15,'Roland','Szpakowski','1988-08-13',642523412,'rolex@o2.pl',7),(16,'Mikołaj','Patyk','2009-08-17',123613417,'mikus@o2.pl',4),(17,'Szymon','Czerwiński','1999-05-23',612361236,'szymczer@interia.pl',9),(18,'Rafał','Naczos','2002-11-25',512367123,'naczosowyrafcio@gmail.com',11),(19,'Mateusz','Najman','1985-02-26',323523553,'vipgala@onet.pl',7),(20,'Amelia','Pazdan','1995-02-26',323326253,'amcia@onet.pl',8),(21,'Monika','Tytoń','2000-01-06',643326253,'monia@o2.pl',12),(22,'Patrycja','Bekon','2001-04-20',743326223,'pati@o2.pl',1),(23,'Barbara','Baron','1980-03-18',843356233,'basia@onet.pl',3),(24,'Tatiana','Talon','1998-08-30',443346233,'tania@gmail.com',6),(25,'Karolina','Bilon','1994-07-23',743323229,'karbilon@interia.com',14);
/*!40000 ALTER TABLE `czytelnik` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-10-25 16:06:30
