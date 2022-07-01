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
-- Table structure for table `autor`
--

DROP TABLE IF EXISTS `autor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `autor` (
  `id_autor` int unsigned NOT NULL AUTO_INCREMENT,
  `imie` varchar(30) DEFAULT NULL,
  `nazwisko` varchar(40) DEFAULT NULL,
  `data_urodzenia` date DEFAULT NULL,
  PRIMARY KEY (`id_autor`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `autor`
--

LOCK TABLES `autor` WRITE;
/*!40000 ALTER TABLE `autor` DISABLE KEYS */;
INSERT INTO `autor` VALUES (1,'Henryk','Sienkiewicz','1846-05-05'),(2,'Bolesław','Prus','1912-05-19'),(3,'Krzysztof Kamil','Baczyński','1944-08-04'),(4,'Jan','Brzechwa','1898-08-15'),(5,'Aleksander','Fredro','1793-06-20'),(6,'Konstanty Ildefons','Gałczyński','1905-01-23'),(7,'Witold','Gombrowicz','1904-08-04'),(8,'Zbigniew','Herbert','1924-10-29'),(9,'Ryszard','Kapuściński','1932-03-04'),(10,'Jan','Kochanowski','1530-06-20'),(11,'Maria','Konopnicka','1842-05-23'),(12,'Hanna','Krall','1935-05-20'),(13,'Ignacy','Krasicki','1735-02-03'),(14,'Zygmunt','Krasiński','1812-02-19'),(15,'Stanisław','Lem','1921-09-12'),(16,'Bolesław','Leśmian','1877-02-22'),(17,'Adam','Mickiewicz','1798-12-24'),(18,'Czesław','Miłosz','1911-06-30'),(19,'Jan Andrzej','Morsztyn','1612-06-24'),(20,'Sławomir','Mrożek','1930-06-29'),(21,'Cyprian Kamil','Norwid','1821-09-24'),(22,'Eliza','Orzeszkowa','1841-06-06'),(23,'Władysław','Reymont','1867-05-07'),(24,'Juliusz','Słowacki','1809-09-04'),(25,'Wisława','Szymborska','1923-07-02'),(26,'Julian','Tuwim','1894-09-13'),(27,'Stanisław Ignacy','Witkiewicz','1885-02-24'),(28,'Stefan','Żeromski','1864-10-14'),(29,'Steven','King','1947-09-21'),(30,'Karool','Cieciura','2006-06-19'),(31,'Jakub','Kosc','2005-05-20'),(32,'Zbiegniew','Dwusetnoga','2005-05-20'),(33,'Jan','Kowalski','1973-02-12'),(34,'Kuba','Irek','1963-01-11'),(35,'Kuba','Kościelny','1999-12-20'),(36,'Karol','Kościelny','2010-03-12'),(37,'Karol','C','2000-12-20');
/*!40000 ALTER TABLE `autor` ENABLE KEYS */;
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
