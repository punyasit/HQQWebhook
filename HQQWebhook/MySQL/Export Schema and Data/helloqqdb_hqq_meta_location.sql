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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-09  3:19:50
