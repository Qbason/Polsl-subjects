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
-- Table structure for table `adres`
--

DROP TABLE IF EXISTS `adres`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adres` (
  `id_adres` int unsigned NOT NULL AUTO_INCREMENT,
  `ulica` varchar(60) DEFAULT NULL,
  `numer_domu` varchar(10) DEFAULT NULL,
  `numer_mieszkania` int DEFAULT NULL,
  `miejscowosc` varchar(50) DEFAULT NULL,
  `kod_pocztowy` varchar(6) DEFAULT NULL,
  PRIMARY KEY (`id_adres`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adres`
--

LOCK TABLES `adres` WRITE;
/*!40000 ALTER TABLE `adres` DISABLE KEYS */;
INSERT INTO `adres` VALUES (1,'Św. Jana Pawła II','21',37,'Studzienice','43-215'),(2,'Św. Jana Pawła II','100',34,'Studzienice','43-215'),(3,'Św. Jana Pawła II','25',54,'Studzienice','43-215'),(4,'Sarenek','40',63,'Studzienice','43-215'),(5,'Sarenek','70',1,'Studzienice','43-215'),(6,'Wilcza','29',6,'Studzienice','43-215'),(7,'Wilcza','10',4,'Studzienice','43-215'),(8,'Magnolii','12',34,'Kaloszyce','43-417'),(9,'Magnolii','30',9,'Kaloszyce','43-417'),(10,'Jana III Sobieskiego','13',5,'Kaloszyce','43-417'),(11,'Jana III Sobieskiego','89',23,'Kaloszyce','43-417'),(12,'Jabłoni','14',75,'Kaloszyce','43-417'),(13,'Jabłoni','7',86,'Kaloszyce','43-417'),(14,'Spokojna','69',34,'Kaloszyce','43-417'),(15,'Spokojna','9',75,'Kaloszyce','43-417'),(16,'Konopnickiej','1',12,'Kaloszyce','43-417'),(17,'Konopnickiej','3',53,'Kaloszyce','43-417'),(18,'Mieszka I','2',64,'Knurów','44-190'),(19,'Mieszka I','15',12,'Knurów','44-190'),(20,'Jęczmienna','12',78,'Knurów','44-190'),(21,'Jęczmienna','88',2,'Knurów','44-190'),(22,'Szpitalna','27',76,'Knurów','44-190'),(23,'Szpitalna','49',45,'Knurów','44-190'),(24,'Pistacjowa','21',65,'Knurów','44-190'),(25,'Pistacjowa','90',32,'Knurów','44-190');
/*!40000 ALTER TABLE `adres` ENABLE KEYS */;
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
