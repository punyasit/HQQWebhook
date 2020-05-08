CREATE DATABASE  IF NOT EXISTS `helloqqdb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `helloqqdb`;
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
-- Table structure for table `hqq_category`
--

DROP TABLE IF EXISTS `hqq_category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hqq_category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(60) NOT NULL,
  `description` varchar(255) NOT NULL,
  `information_url` varchar(255) DEFAULT NULL,
  `status` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hqq_category`
--

LOCK TABLES `hqq_category` WRITE;
/*!40000 ALTER TABLE `hqq_category` DISABLE KEYS */;
INSERT INTO `hqq_category` VALUES (1,'นาฬิกาสมาร์ทวอท','นาฬิกามีฟังชั่น ใช้ในการออกกำลังกาย แจ้งเตือนสถานะ และฟังชั่นอื่นๆ',NULL,1),(2,'นาฬิกาสมาร์ทวอท GPS','นาฬิกามีฟังชั่น ใช้ในการออกกำลังกาย มี GPS ติดตามตำแหน่ง มีแจ้งเตือนสถานะ และฟังชั่นอื่นๆ',NULL,1),(3,'นาฬิกา','นาฬิกาทั่วไปมีฟังชั่นมาตรฐาน',NULL,1),(4,'หูฟัง True Wireless','หูฟังไร้สาย True wireless ไร้สายใช้เชื่อมต่อผ่าน Bluetooth',NULL,1),(5,'หูฟังมีสาย','หูฟังแบบมีสายใช้เสียบกับเครื่องเล่นหรือมือถือ',NULL,1);
/*!40000 ALTER TABLE `hqq_category` ENABLE KEYS */;
UNLOCK TABLES;

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

--
-- Table structure for table `hqq_meta_location`
--

DROP TABLE IF EXISTS `hqq_meta_location`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hqq_meta_location` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ISO_code` varchar(6) NOT NULL,
  `name` varchar(100) DEFAULT NULL,
  `keyword` varchar(255) DEFAULT NULL,
  `language` varchar(3) DEFAULT 'TH',
  `status` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=79 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hqq_meta_location`
--

LOCK TABLES `hqq_meta_location` WRITE;
/*!40000 ALTER TABLE `hqq_meta_location` DISABLE KEYS */;
INSERT INTO `hqq_meta_location` VALUES (1,'TH-10',' กรุงเทพมหานคร','Bangkok, KrungThap','TH',1),(2,'TH-11',' สมุทรปราการ','Samut Prakan','TH',1),(3,'TH-12',' นนทบุรี','Nonthaburi','TH',1),(4,'TH-13',' ปทุมธานี','Pathum Thani','TH',1),(5,'TH-14',' พระนครศรีอยุธยา','Phra Nakhon Si Ayutthaya','TH',1),(6,'TH-15',' อ่างทอง','Ang Thong','TH',1),(7,'TH-16',' ลพบุรี','Lop Buri','TH',1),(8,'TH-17',' สิงห์บุรี','Sing Buri','TH',1),(9,'TH-18',' ชัยนาท','Chai Nat','TH',1),(10,'TH-19',' สระบุรี','Saraburi','TH',1),(11,'TH-20',' ชลบุรี','Chon Buri','TH',1),(12,'TH-21',' ระยอง','Rayong','TH',1),(13,'TH-22',' จันทบุรี','Chanthaburi','TH',1),(14,'TH-23',' ตราด','Trat','TH',1),(15,'TH-24',' ฉะเชิงเทรา','Chachoengsao','TH',1),(16,'TH-25',' ปราจีนบุรี','Prachin Buri','TH',1),(17,'TH-26',' นครนายก','Nakhon Nayok','TH',1),(18,'TH-27',' สระแก้ว','Sa Kaeo','TH',1),(19,'TH-30',' นครราชสีมา','Nakhon Ratchasima, Korat','TH',1),(20,'TH-31',' บุรีรัมย์','Buri Ram','TH',1),(21,'TH-32',' สุรินทร์','Surin','TH',1),(22,'TH-33',' ศรีสะเกษ','Si Sa Ket','TH',1),(23,'TH-34',' อุบลราชธานี','Ubon Ratchathani','TH',1),(24,'TH-35',' ยโสธร','Yasothon','TH',1),(25,'TH-36',' ชัยภูมิ','Chaiyaphum','TH',1),(26,'TH-37',' อำนาจเจริญ','Amnat Charoen','TH',1),(27,'TH-38',' บึงกาฬ','Bueng Kan','TH',1),(28,'TH-39',' หนองบัวลำภู','Nong Bua Lam Phu','TH',1),(29,'TH-40',' ขอนแก่น','Khon Kaen','TH',1),(30,'TH-41',' อุดรธานี','Udon Thani','TH',1),(31,'TH-42',' เลย','Loei','TH',1),(32,'TH-43',' หนองคาย','Nong Khai','TH',1),(33,'TH-44',' มหาสารคาม','Maha Sarakham','TH',1),(34,'TH-45',' ร้อยเอ็ด','Roi Et','TH',1),(35,'TH-46',' กาฬสินธุ์','Kalasin','TH',1),(36,'TH-47',' สกลนคร','Sakon Nakhon','TH',1),(37,'TH-48',' นครพนม','Nakhon Phanom','TH',1),(38,'TH-49','มุกดาหาร','Mukdahan','TH',1),(39,'TH-50',' เชียงใหม่','Chiang Mai','TH',1),(40,'TH-51',' ลำพูน','Lamphun','TH',1),(41,'TH-52',' ลำปาง','Lampang','TH',1),(42,'TH-53',' อุตรดิตถ์','Uttaradit','TH',1),(43,'TH-54',' แพร่','Phrae','TH',1),(44,'TH-55',' น่าน','Nan','TH',1),(45,'TH-56',' พะเยา','Phayao','TH',1),(46,'TH-57',' เชียงราย','Chiang Rai','TH',1),(47,'TH-58',' แม่ฮ่องสอน','Mae Hong Son','TH',1),(48,'TH-60',' นครสวรรค์','Nakhon Sawan','TH',1),(49,'TH-61',' อุทัยธานี','Uthai Thani','TH',1),(50,'TH-62',' กำแพงเพชร','Kamphaeng Phet','TH',1),(51,'TH-63',' ตาก','Tak','TH',1),(52,'TH-64',' สุโขทัย','Sukhothai','TH',1),(53,'TH-65',' พิษณุโลก','Phitsanulok','TH',1),(54,'TH-66',' พิจิตร','Phichit','TH',1),(55,'TH-67',' เพชรบูรณ์','Phetchabun','TH',1),(56,'TH-70',' ราชบุรี','Ratchaburi','TH',1),(57,'TH-71',' กาญจนบุรี','Kanchanaburi','TH',1),(58,'TH-72',' สุพรรณบุรี','Suphan Buri','TH',1),(59,'TH-73',' นครปฐม','Nakhon Pathom','TH',1),(60,'TH-74',' สมุทรสาคร','Samut Sakhon','TH',1),(61,'TH-75',' สมุทรสงคราม','Samut Songkhram','TH',1),(62,'TH-76',' เพชรบุรี','Phetchaburi','TH',1),(63,'TH-77',' ประจวบคีรีขันธ์','Prachuap Khiri Khan','TH',1),(64,'TH-80',' นครศรีธรรมราช','Nakhon Si Thammarat','TH',1),(65,'TH-81',' กระบี่','Krabi','TH',1),(66,'TH-82',' พังงา','Phangnga','TH',1),(67,'TH-83',' ภูเก็ต','Phuket','TH',1),(68,'TH-84',' สุราษฎร์ธานี','Surat Thani','TH',1),(69,'TH-85',' ระนอง','Ranong','TH',1),(70,'TH-86',' ชุมพร','Chumphon','TH',1),(71,'TH-90',' สงขลา','Songkhla','TH',1),(72,'TH-91',' สตูล','Satun','TH',1),(73,'TH-92',' ตรัง','Trang','TH',1),(74,'TH-93',' พัทลุง','Phatthalung','TH',1),(75,'TH-94',' ปัตตานี','Pattani','TH',1),(76,'TH-95',' ยะลา','Yala','TH',1),(77,'TH-96',' นราธิวาส','Narathiwat','TH',1),(78,'TH-S',' พัทยา','Phatthaya','TH',1);
/*!40000 ALTER TABLE `hqq_meta_location` ENABLE KEYS */;
UNLOCK TABLES;

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

--
-- Table structure for table `hqq_product`
--

DROP TABLE IF EXISTS `hqq_product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hqq_product` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `category_id` int(11) NOT NULL,
  `name` varchar(120) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `information_url` varchar(255) DEFAULT NULL,
  `preview_image_url` varchar(500) DEFAULT NULL,
  `created_on` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL DEFAULT '1',
  `modified_on` datetime DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  `status` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  KEY `category_link_product_idx` (`category_id`),
  CONSTRAINT `category_link_product` FOREIGN KEY (`category_id`) REFERENCES `hqq_category` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hqq_product`
--

LOCK TABLES `hqq_product` WRITE;
/*!40000 ALTER TABLE `hqq_product` DISABLE KEYS */;
INSERT INTO `hqq_product` VALUES (1,4,'Sabbat E12 Ultra',' หูฟัง In-Ear True Wireless ระบบ aptX และบลูธูท 5.0   ','https://www.facebook.com/HelloQQShop/posts/2757919954316299','https://dz.lnwfile.com/0i07gk.png','2020-05-06 00:00:00',1,NULL,NULL,1),(2,4,'Sabbat X12 Ultra','หูฟัง Earbuds  True Wireless 5.0 รองรับ Aptx และ Bluetooth 5.0','https://www.facebook.com/HelloQQShop/posts/2654898071285155','https://dz.lnwfile.com/xm2o2m.png','2020-05-06 00:00:00',1,NULL,NULL,1),(3,4,'Sabbat X12 Pro','หูฟัง Earbuds  True Wireless 5.0ฟังเพลง ดูหนัง เล่นเกม ราคาเบาๆ','https://www.facebook.com/HelloQQShop/posts/2227153944059572','https://dz.lnwfile.com/qg3y1w.png','2020-05-06 00:00:00',1,NULL,NULL,1),(4,2,'Mega M5','มาพร้อมหน้าจอทรงสปอร์ตเต็มเหนี่ยว มี GPS สามารถรับสาย โทรออกได้','https://www.facebook.com/HelloQQShop/posts/2458462237595407/','https://dz.lnwfile.com/f0hba6.png','2020-05-08 00:00:00',1,NULL,NULL,1),(5,2,'Mega M4','สมาร์ทว๊อท GPS มาพร้อมหน้าเรียบหรู ใส่วิ่ง ใส่ทำงานได้สบายๆ ','https://www.facebook.com/HelloQQShop/posts/2345330352241930','https://dz.lnwfile.com/0g7bbx.png','2020-05-08 00:00:00',1,NULL,NULL,1),(6,2,'Mega M1','มาพร้อม GPS น้ำหนักเบา ฟังชั่นครบครัน','https://www.facebook.com/HelloQQShop/posts/2171309716310662/','https://dz.lnwfile.com/vgbbbf.png','2020-05-08 00:00:00',1,NULL,NULL,1),(7,2,'Iwown P1C','สมาร์ทว๊อท GPS ฟังชั่นวิ่งครบครัน VO2Max, Endurance, น้ำหนักเบา','https://www.facebook.com/HelloQQShop/posts/1944182885690014/','https://dz.lnwfile.com/l8xyrv.png','2020-05-08 00:00:00',1,NULL,NULL,1),(8,2,'SKY3','','','https://dz.lnwfile.com/htrrw3.png','2020-05-08 00:00:00',1,NULL,NULL,0),(9,1,'Falcon X9',' รุ่นที่ขายดีที่สุด ทรง Sport Carbon Fiber พร้อมฟังชั่นออกกำลังกาย','https://www.facebook.com/HelloQQShop/posts/1903698406405129/','https://dz.lnwfile.com/q394mq.png','2020-05-08 00:00:00',1,NULL,NULL,1),(10,1,'Falcon G8','สมาร์ทว๊อท ทรงสปอร์ท มาพร้อมฟังชั่น ECG และฮาร์ทเรท','https://www.facebook.com/HelloQQShop/posts/2204554262986207/','https://dz.lnwfile.com/cdewp3.png','2020-05-08 00:00:00',1,NULL,NULL,1),(11,1,'SKY4','','','','2020-05-08 00:00:00',1,NULL,NULL,0),(12,1,'SKY2','เรียบหรูหวานๆ ผู้หญิงก็ใส่ได้ ผู้ชายก็ใส่ดี คมชัด สไตร์วัยแนวๆ','https://www.facebook.com/HelloQQShop/posts/2449715191803445/','https://dz.lnwfile.com/1cdfvd.png','2020-05-08 00:00:00',1,NULL,NULL,1),(13,1,'SN80','สมาร์ทว๊อททรงเรียบหรู สไตร์โมเดิร์น เข้ากับทุก Life Style','https://www.facebook.com/HelloQQShop/posts/2432902450151386/','https://dz.lnwfile.com/cdewp3.png','2020-05-08 00:00:00',1,NULL,NULL,1),(14,1,'DT78','DT78 รุ่นยอดฮิตในตลาดยุโรป วิ่ง ทำงาน เที่ยว ได้ทุกสถานการณ์','https://www.facebook.com/HelloQQShop/posts/2433178750123756/','https://dz.lnwfile.com/bnwjb0.png','2020-05-08 00:00:00',1,NULL,NULL,1),(15,1,'S9 Leo','ทรงเหลี่ยมที่ขายดีตลอดกาล พร้อมฟังชั่นออกกำลัง และการแจ้งเตือน','https://www.facebook.com/HelloQQShop/posts/1968636439911325/','https://dz.lnwfile.com/4iofbc.png','2020-05-08 00:00:00',1,NULL,NULL,1);
/*!40000 ALTER TABLE `hqq_product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hqq_sale_channel`
--

DROP TABLE IF EXISTS `hqq_sale_channel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hqq_sale_channel` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL COMMENT 'Facebook, Shopee, Lazada, Line, Direct',
  `description` varchar(300) DEFAULT NULL,
  `URL` varchar(100) DEFAULT NULL,
  `status` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hqq_sale_channel`
--

LOCK TABLES `hqq_sale_channel` WRITE;
/*!40000 ALTER TABLE `hqq_sale_channel` DISABLE KEYS */;
INSERT INTO `hqq_sale_channel` VALUES (1,'Facebook','คุยกันได้ทุกเรื่อง ที่ Facebook','https://www.facebook.com',1),(2,'Shopee','Shopee ราคาดี มีโปร','https://shopee.co.th/helloqq.shop',1),(3,'Lazada','Lazada ราคาดี มีโปร','https://www.lazada.co.th/shop/helloqqshop/',1),(4,'Line@','พบกับโปรโมชั่น ประจำต้นเดือนที่  Line@','https://lin.ee/aTnLF2B',1);
/*!40000 ALTER TABLE `hqq_sale_channel` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'helloqqdb'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-09  3:37:26
