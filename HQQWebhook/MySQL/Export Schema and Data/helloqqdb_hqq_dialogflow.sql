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
-- Table structure for table `hqq_dialogflow`
--

DROP TABLE IF EXISTS `hqq_dialogflow`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hqq_dialogflow` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `flow_type` varchar(50) DEFAULT NULL,
  `match_keywords` varchar(500) DEFAULT NULL,
  `product_id` int(11) DEFAULT NULL,
  `payload` varchar(255) DEFAULT NULL,
  `payload_img_url` varchar(255) DEFAULT NULL,
  `response_header` varchar(255) DEFAULT NULL,
  `response_answer` varchar(255) DEFAULT NULL,
  `response_items` varchar(100) DEFAULT NULL,
  `status` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `product_link_dialogflow_idx` (`product_id`),
  CONSTRAINT `product_link_dialogflow` FOREIGN KEY (`product_id`) REFERENCES `hqq_product` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hqq_dialogflow`
--

LOCK TABLES `hqq_dialogflow` WRITE;
/*!40000 ALTER TABLE `hqq_dialogflow` DISABLE KEYS */;
INSERT INTO `hqq_dialogflow` VALUES (1,'introduction','แนะนำสินค้า',NULL,'HQQ_PL_INTRODUCTION',NULL,'สนใจเป็นประเภทใหนคะ;ดูเป็นประเภทใหนไว้คะ','','3,4,5,6',1),(2,'introduction','ช่วยแนะนำให้ฉันได้ไหม',NULL,'',NULL,'สนใจเป็นประเภทใหนคะ;ดูเป็นประเภทใหนไว้คะ','','3,4,5,6',1),(3,'introduction','',NULL,'HQQ_PL_HEADPHONE',NULL,'ตอนนี้ที่ร้านมีให้เลือกตามนี้นะคะ;มีให้เลือกตามนี้เลย','หูฟัง','7,8,9',1),(4,'introduction','',NULL,'HQQ_PL_GPSWATCH',NULL,'นาฬิกา GPS ที่ร้านจะมีตามนี้นะคะ;นาฬิกา GPS มีให้เลือกเน้นๆตามนี้เลย','สมาร์ทวอท GPS','12,13,14,15,16',1),(5,'introduction','',NULL,'HQQ_PL_WATCH',NULL,'นาฬิกามีหลายแบบเลย กดรูปดูรายละเอียดได้นะ;มีแบบให้เลือกตามนี้นะคะ กดรูปดูรายละเอียดได้นะ','สมาร์ทวอทเบสิก','17,18,19,20,21,22,23',1),(6,'introduction','',NULL,'HQQ_PL_ENDFLOW',NULL,NULL,'ติดต่อแอดมิน','',1),(7,'introduction','',1,'HQQ_PL_SE12ULTRA',NULL,NULL,'Sabbat E12 Ultra','',1),(8,'introduction','',2,'HQQ_PL_SX12ULTRA',NULL,NULL,'Sabbat X12 Ultra','',1),(9,'introduction','',3,'HQQ_PL_SX12PRO',NULL,NULL,'Sabbat X12 Pro','',1),(10,'private_reply','สนใจ, สนใจครับ, สนใจคับ, สนใจค่ะ, inboxครับ',NULL,'สวัสดีค่ะ สนใจสินค้าตัวใหนสอบถามได้เลยนะคะ',NULL,NULL,'อินบ๊อคไปนะคะ;Inbox ไปนะคะ;รายละเอียด​ส่งให้ทาง Inbox นะค; รับทราบคะ​;ขออนุญาติส่งข้อมูลให้ทาง Inbox นะคะ;ยินดีค๊า​ ทัก Inbox ไปละค่ะ','11',1),(11,'private_reply','',NULL,'HQQ_PL_INTRODUCTION',NULL,NULL,'แนะนำสินค้า','3,4,6',1),(12,'introduction','',4,'HQQ_PL_M5','','','M5','',1),(13,'introduction','',5,'HQQ_PL_M4','','','M4','',1),(14,'introduction','',6,'HQQ_PL_M1','','','M1','',1),(15,'introduction','',7,'HQQ_PL_IW','','','iWown','',1),(16,'introduction','',8,'HQQ_PL_SKY3','','','SKY3','',1),(17,'introduction','',9,'HQQ_PL_X9','','','X9','',1),(18,'introduction','',10,'HQQ_PL_G8','','','G8','',1),(19,'introduction','',11,'HQQ_PL_SKY4','','','SKY4','',1),(20,'introduction','',12,'HQQ_PL_SKY2','','','SKY2','',1),(21,'introduction','',13,'HQQ_PL_SN80','','','SN80','',1),(22,'introduction','',14,'HQQ_PL_DT78','','','DT78','',1),(23,'introduction','',15,'HQQ_PL_S9','','','S9PLUS','',1);
/*!40000 ALTER TABLE `hqq_dialogflow` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-09  3:19:49
