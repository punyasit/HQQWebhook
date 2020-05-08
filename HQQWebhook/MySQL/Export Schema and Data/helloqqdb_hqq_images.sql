-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: helloqqdb
-- ------------------------------------------------------
-- Server version	5.6.40-log

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
-- Table structure for table `hqq_images`
--

DROP TABLE IF EXISTS `hqq_images`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hqq_images` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `entity_id` int(11) NOT NULL,
  `filename` varchar(100) NOT NULL,
  `path` varchar(255) NOT NULL,
  `file_type` varchar(16) DEFAULT NULL,
  `location` varchar(255) NOT NULL,
  `created_on` datetime DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `status` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hqq_images`
--

LOCK TABLES `hqq_images` WRITE;
/*!40000 ALTER TABLE `hqq_images` DISABLE KEYS */;
INSERT INTO `hqq_images` VALUES (1,1,'admin.','~/Content/Images/Product/00001//PRODUCT_MANUAL/20180609-Falcon-X2-GR.png','PRODUCT_MANUAL','/Content/Images/Product/00001//PRODUCT_MANUAL/20180609-Falcon-X2-GR.png','2018-06-09 23:58:25',1,1),(3,1,'admin.','~//Content/Images/Product/00001//PRODUCT_MANUAL/20180610114049-FalconX2-GR-002.png','PRODUCT_MANUAL','/Content/Images/Product/00001//PRODUCT_MANUAL/20180610114049-FalconX2-GR-002.png','2018-06-10 11:40:50',1,1),(4,1,'admin.','~//Content/Images/Product/00001//PRODUCT_MANUAL/20180610114051-FalconX2-GR-003.png','PRODUCT_MANUAL','/Content/Images/Product/00001//PRODUCT_MANUAL/20180610114051-FalconX2-GR-003.png','2018-06-10 11:40:52',1,1),(5,1,'admin.','~//Content/Images/Product/00001//PRODUCT_MANUAL/20180610114053-FalconX2-GR-005.png','PRODUCT_MANUAL','/Content/Images/Product/00001//PRODUCT_MANUAL/20180610114053-FalconX2-GR-005.png','2018-06-10 11:40:53',1,1);
/*!40000 ALTER TABLE `hqq_images` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-09  3:19:48
