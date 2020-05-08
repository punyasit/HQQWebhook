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
-- Table structure for table `hqq_price`
--

DROP TABLE IF EXISTS `hqq_price`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hqq_price` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `product_id` int(11) NOT NULL,
  `channel_id` int(11) NOT NULL,
  `affiliate_url` varchar(45) DEFAULT NULL,
  `price_date` datetime NOT NULL,
  `price` decimal(10,2) DEFAULT NULL,
  `status` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `hqq_product_price_idx` (`product_id`),
  KEY `product_lnk_channel_idx` (`channel_id`),
  CONSTRAINT `product_lnk_channel` FOREIGN KEY (`channel_id`) REFERENCES `hqq_sale_channel` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `product_lnk_price` FOREIGN KEY (`product_id`) REFERENCES `hqq_product` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hqq_price`
--

LOCK TABLES `hqq_price` WRITE;
/*!40000 ALTER TABLE `hqq_price` DISABLE KEYS */;
INSERT INTO `hqq_price` VALUES (1,1,2,'https://shopee.prf.hn/l/7agOgqq','2020-05-06 00:00:00',1990.00,1),(2,2,2,'https://prf.hn/l/7o8wGJr','2020-05-06 00:00:00',1790.00,1),(3,3,2,'https://prf.hn/l/7ABaRgr','2020-05-06 00:00:00',1650.00,1),(4,4,2,'https://prf.hn/l/Km9gdYK','2020-05-08 00:00:00',1850.00,1),(5,5,2,'https://prf.hn/l/6Z4Bv57','2020-05-08 00:00:00',1850.00,1),(6,6,2,'https://prf.hn/l/6leAvAK','2020-05-08 00:00:00',1590.00,1),(7,7,2,'https://prf.hn/l/7a9Bvl7','2020-05-08 00:00:00',1590.00,1),(8,9,2,'https://prf.hn/l/78dmqA7','2020-05-08 00:00:00',1250.00,1),(9,10,2,'https://prf.hn/l/KLPBeRK','2020-05-08 00:00:00',990.00,1),(10,12,2,'https://prf.hn/l/7QXqmzK','2020-05-08 00:00:00',990.00,1),(11,13,2,'https://prf.hn/l/KXN5ARr','2020-05-08 00:00:00',1290.00,1),(12,14,2,'https://prf.hn/l/7gb5GqK','2020-05-08 00:00:00',1290.00,1),(13,15,2,'https://prf.hn/l/rGzoOJ7','2020-05-08 00:00:00',890.00,1),(14,1,1,'','2020-05-08 00:00:00',1990.00,1),(15,2,1,'','2020-05-08 00:00:00',1790.00,1),(16,3,1,'','2020-05-08 00:00:00',1650.00,1),(17,4,1,'','2020-05-08 00:00:00',1850.00,1),(18,5,1,'','2020-05-08 00:00:00',1850.00,1),(19,6,1,'','2020-05-08 00:00:00',1590.00,1),(20,7,1,'','2020-05-08 00:00:00',1590.00,1),(21,9,1,'','2020-05-08 00:00:00',1250.00,1),(22,10,1,'','2020-05-08 00:00:00',990.00,1),(23,12,1,'','2020-05-08 00:00:00',990.00,1),(24,13,1,'','2020-05-08 00:00:00',1290.00,1),(25,14,1,'','2020-05-08 00:00:00',1290.00,1),(26,15,1,'','2020-05-08 00:00:00',890.00,1);
/*!40000 ALTER TABLE `hqq_price` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-09  3:19:51
