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
-- Table structure for table `hqq_product_manual`
--

DROP TABLE IF EXISTS `hqq_product_manual`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hqq_product_manual` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `product_id` int(11) NOT NULL,
  `subject` varchar(255) NOT NULL,
  `content` text NOT NULL,
  `created_on` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_on` datetime DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  `status` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  KEY `product_manual_lnk_product_idx` (`product_id`),
  CONSTRAINT `product_manual_lnk_product` FOREIGN KEY (`product_id`) REFERENCES `hqq_product` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hqq_product_manual`
--

LOCK TABLES `hqq_product_manual` WRITE;
/*!40000 ALTER TABLE `hqq_product_manual` DISABLE KEYS */;
INSERT INTO `hqq_product_manual` VALUES (1,1,'คู่มือ Falcon X2','<p><br>Hello <b>Summernote<br>ทดสอบภาษาไทย<br><span style=\"background-color: rgb(255, 255, 0);\">สุดยอด</span></b></p><p style=\"margin-right: 0px; margin-bottom: 15px; margin-left: 0px; padding: 0px; text-align: justify; font-family: &quot;Open Sans&quot;, Arial, sans-serif; font-size: 14px;\">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras faucibus nisi vitae orci lobortis tempor. Duis auctor lorem eu consectetur porta. Cras sodales ut nisl non faucibus. Donec bibendum et ante id tristique. Fusce vitae euismod metus. Nunc mi lacus, elementum et hendrerit eget, finibus sit amet ipsum. Curabitur eget lorem sapien. Nunc vitae consequat eros. In porttitor lorem dictum nisi egestas, quis convallis lorem interdum. Nulla commodo quis nulla sed varius.</p><p style=\"margin-right: 0px; margin-bottom: 15px; margin-left: 0px; padding: 0px; text-align: justify; font-family: &quot;Open Sans&quot;, Arial, sans-serif; font-size: 14px;\">In quis arcu fringilla, euismod diam a, scelerisque quam. Nam ultrices vitae neque a ultricies. Suspendisse vestibulum rhoncus eros, quis luctus sapien interdum tincidunt. Pellentesque non ex scelerisque, mollis ante eu, vehicula elit. Nulla cursus, est sed feugiat tincidunt, justo quam blandit urna, quis tristique urna diam pharetra arcu. Donec posuere finibus porta. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Duis nulla eros, aliquet et auctor id, fringilla ac tellus. Nunc non sodales felis. Vivamus vel venenatis ipsum. Donec auctor varius felis interdum eleifend. Mauris a arcu et tellus bibendum tristique tristique nec elit. Morbi eget imperdiet elit. Cras ut ipsum vitae mi maximus mollis posuere sed nunc. Vestibulum euismod feugiat augue ac lacinia. Interdum et malesuada fames ac ante ipsum primis in faucibus.</p><p style=\"margin-right: 0px; margin-bottom: 15px; margin-left: 0px; padding: 0px; text-align: justify; font-family: &quot;Open Sans&quot;, Arial, sans-serif; font-size: 14px;\">Nunc ultrices laoreet diam sit amet elementum. Morbi ultricies sapien at egestas sollicitudin. In hac habitasse platea dictumst. Maecenas scelerisque vulputate turpis, quis elementum dui ultrices ut. Integer fringilla consequat pellentesque. Etiam venenatis sodales orci bibendum porta. Mauris maximus sagittis leo, vel sagittis odio fringilla eu. Vestibulum a lectus tincidunt metus iaculis condimentum et id massa. Duis finibus neque ligula. Fusce posuere dictum leo eu interdum. Proin vitae enim id odio molestie vehicula sed vitae metus. In fermentum tincidunt ex. Suspendisse potenti. Phasellus mattis ante felis, eget tincidunt mi iaculis sit amet. In non arcu quis lectus pharetra aliquam imperdiet non purus. Sed dignissim tortor tellus, eu commodo purus interdum ac.</p><table style=\"border:0\"><tbody><tr><td><img src=\"/image/1/thumbnail\" style=\"font-size: 1rem;\"></td><td>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras faucibus nisi vitae orci lobortis tempor. Duis auctor lorem eu consectetur porta. Cras sodales ut nisl non faucibus. Donec bibendum et ante id tristique. Fusce vitae euismod metus. Nunc mi lacus, elementum et hendrerit eget, finibus sit amet ipsum. Curabitur eget lorem sapien. Nunc vitae consequat eros. In porttitor lorem dictum nisi egestas, quis convallis lorem interdum. Nulla commodo quis nulla sed varius.<br></td></tr></tbody></table><p><br></p>','2018-06-07 23:51:28',1,'2018-06-19 21:24:02',1,1);
/*!40000 ALTER TABLE `hqq_product_manual` ENABLE KEYS */;
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
