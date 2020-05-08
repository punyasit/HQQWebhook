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
-- Table structure for table `hqq_member`
--

DROP TABLE IF EXISTS `hqq_member`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hqq_member` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `facebook_id` varchar(100) DEFAULT NULL,
  `fullname` varchar(120) NOT NULL,
  `facebook_name` varchar(125) NOT NULL,
  `picture_url` varchar(255) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `location_code` varchar(5) DEFAULT NULL,
  `hometown_code` varchar(5) DEFAULT NULL,
  `postcode` varchar(5) NOT NULL,
  `role` tinyint(4) NOT NULL DEFAULT '1',
  `created_by` int(11) DEFAULT NULL,
  `created_on` datetime NOT NULL,
  `modified_on` datetime DEFAULT NULL,
  `status` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hqq_member`
--

LOCK TABLES `hqq_member` WRITE;
/*!40000 ALTER TABLE `hqq_member` DISABLE KEYS */;
INSERT INTO `hqq_member` VALUES (1,'00000000001','System Admin','none',NULL,'no-address',NULL,NULL,'',99,1,'2018-01-01 00:00:00',NULL,1),(2,'10154504180661356','ปุณยสิทธิ์ จันทรรัตน์','Punyasit Juntararat','https://platform-lookaside.fbsbx.com/platform/profilepic/?asid=10154504180661356&height=50&width=50&ext=1546087171&hash=AeTOgk0jucF9tcAS','2/19 เอเบิลคอนโด ลาดพร้าว 27 แขวง จันเกษม จตุจักร กรุงเทพมหานคร 10900','TH-10','TH-30','',99,1,'2018-05-23 23:44:14','2018-11-29 19:39:34',1),(3,'128810511035301','John Smith Wiskey','John Smith Wiskey','https://platform-lookaside.fbsbx.com/platform/profilepic/?asid=128810511035301&height=50&width=50&ext=1531915970&hash=AeS3PUvehoVWfahf','ที่อยู่ในประเทศไทย สักที่ใน โลก อยากรู้จังว่า จะอยู่ที่ไหน ไม่ไกลหรอก 20123',NULL,NULL,'30000',1,1,'2018-07-14 16:56:35','2018-07-15 19:12:52',0);
/*!40000 ALTER TABLE `hqq_member` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-09  3:19:50
