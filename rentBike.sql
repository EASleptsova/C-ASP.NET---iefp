-- --------------------------------------------------------
-- Anfitrião:                    127.0.0.1
-- Versão do servidor:           10.3.14-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Versão:              10.1.0.5464
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for rentbike
DROP DATABASE IF EXISTS `rentbike`;
CREATE DATABASE IF NOT EXISTS `rentbike` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `rentbike`;

-- Dumping structure for table rentbike.bike
DROP TABLE IF EXISTS `bike`;
CREATE TABLE IF NOT EXISTS `bike` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `stationId` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_bike_station` (`stationId`),
  CONSTRAINT `FK_bike_station` FOREIGN KEY (`stationId`) REFERENCES `station` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=393 DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table rentbike.rent
DROP TABLE IF EXISTS `rent`;
CREATE TABLE IF NOT EXISTS `rent` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bikeId` int(11) NOT NULL,
  `destinationId` int(11) DEFAULT NULL,
  `startStationId` int(11) NOT NULL,
  `userId` int(11) NOT NULL,
  `startDate` date NOT NULL,
  `endDate` date DEFAULT NULL,
  `status` varchar(10) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_rent_bike` (`bikeId`),
  KEY `FK_rent_station` (`destinationId`),
  KEY `FK_rent_station_2` (`startStationId`),
  KEY `FK_rent_user` (`userId`),
  CONSTRAINT `FK_rent_user` FOREIGN KEY (`userId`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table rentbike.station
DROP TABLE IF EXISTS `station`;
CREATE TABLE IF NOT EXISTS `station` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `address` varchar(50) NOT NULL,
  `status` varchar(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
-- Dumping structure for table rentbike.user
DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `nif` int(11) NOT NULL,
  `contact` varchar(50) NOT NULL,
  `login` varchar(15) NOT NULL,
  `password` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
