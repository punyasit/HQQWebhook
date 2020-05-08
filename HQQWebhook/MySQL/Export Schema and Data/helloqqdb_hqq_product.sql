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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-09  3:19:48
