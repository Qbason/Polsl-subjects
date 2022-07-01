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
-- Table structure for table `pracownik`
--

DROP TABLE IF EXISTS `pracownik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pracownik` (
  `id_pracownik` int unsigned NOT NULL AUTO_INCREMENT,
  `imie` varchar(30) DEFAULT NULL,
  `nazwisko` varchar(40) DEFAULT NULL,
  `data_urodzenia` date DEFAULT NULL,
  `wynagrodzenie` int DEFAULT NULL,
  `telefon` int DEFAULT NULL,
  `email` varchar(60) DEFAULT NULL,
  `id_adres` int unsigned DEFAULT NULL,
  PRIMARY KEY (`id_pracownik`),
  KEY `id_adres` (`id_adres`),
  CONSTRAINT `pracownik_ibfk_2` FOREIGN KEY (`id_adres`) REFERENCES `adres` (`id_adres`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pracownik`
--

LOCK TABLES `pracownik` WRITE;
/*!40000 ALTER TABLE `pracownik` DISABLE KEYS */;
INSERT INTO `pracownik` VALUES (1,'Jagoda','Mięta','1967-02-02',3400,753342175,'jagodka69@gmail.com',15),(2,'Jadzia','Pala','1973-03-21',3300,823342175,'jadzia82@gmail.com',16),(3,'Tadeusz','Milik','1984-11-27',3800,643342175,'tadziumilik@gmail.com',17),(4,'Daniel','Bebech','2000-01-13',2700,531342175,'gapiszsienabebech@onet.pl',18),(5,'Genowefa','Mika','1955-04-03',4000,571246312,'genia55@interia.pl',19),(6,'Monika','Galos','2000-05-25',2800,475246312,'galomonia@o2.pl',20),(7,'Marcin','Wiatrak','1998-07-20',2600,571246312,'wiatrak98@interia.pl',21),(8,'Michał','Penerowski','1995-06-12',3000,871246212,'pener@interia.pl',22),(9,'Maria','Twardowska','1975-09-07',3200,971246212,'twardamaria@onet.pl',23),(10,'Natalia','Cias','1999-08-08',2600,451242212,'ciasnatalia@onet.pl',24),(11,'Barbara','Pędzel','1967-10-14',2900,752246123,'basiap@o2.pl',25);
/*!40000 ALTER TABLE `pracownik` ENABLE KEYS */;
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
