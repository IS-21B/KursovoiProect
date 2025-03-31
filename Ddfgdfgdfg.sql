-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: 10.207.106.12    Database: db29
-- ------------------------------------------------------
-- Server version	8.0.31

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
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categories` (
  `CategoryID` int NOT NULL AUTO_INCREMENT,
  `CategoryName` varchar(100) NOT NULL,
  `Description` text,
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=103 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (51,'Смартфоны','Современные мобильные телефоны с функциями смартфона, включая камеры, приложения и интернет.'),(52,'Ноутбуки','Портативные компьютеры для работы и развлечений с различными характеристиками и размерами.'),(53,'Телевизоры','Смарт-телевизоры и LED-телевизоры с высоким разрешением и различными функциями.'),(54,'Планшеты','Устройства с сенсорными экранами, предназначенные для работы и развлечений.'),(55,'Аудиотехника','Наушники, колонки, усилители и другие устройства для воспроизведения звука.'),(56,'Игровые консоли','Устройства для видеоигр, включая приставки и аксессуары.'),(57,'Фотоаппараты','Цифровые фотоаппараты и видеокамеры для любителей и профессионалов.'),(58,'Бытовая техника','Электроприборы для дома, включая холодильники, стиральные машины и микроволновые печи.'),(59,'Компьютерные комплектующие','Процессоры, видеокарты, материнские платы и другие компоненты для сборки ПК.'),(60,'Умный дом','Устройства для автоматизации дома, включая умные лампочки, термостаты и системы безопасности.'),(61,'Смарт-часы','Умные часы с функциями отслеживания фитнеса, уведомлений и управления музыкой.'),(62,'Наушники','Беспроводные и проводные наушники для прослушивания музыки и общения.'),(63,'Портативные колонки','Компактные колонки с Bluetooth для воспроизведения музыки на ходу.'),(64,'Электросамокаты','Современные электрические самокаты для удобного передвижения по городу.'),(65,'Дрон','Беспилотные летательные аппараты для съемки и развлечений.'),(66,'Видеокамеры','Камеры для записи видео, включая экшн-камеры и видеорегистраторы.'),(67,'Камеры наблюдения','Устройства для видеонаблюдения и безопасности дома и офиса.'),(68,'Флешки','USB-накопители для хранения и передачи данных.'),(69,'Жесткие диски','Внешние и внутренние жесткие диски для хранения больших объемов данных.'),(70,'Сетевое оборудование','Маршрутизаторы, модемы и точки доступа для организации сети.'),(71,'Кабели и адаптеры','Разнообразные кабели и адаптеры для подключения устройств.'),(72,'Игровые аксессуары','Геймпады, клавиатуры и мыши для геймеров.'),(73,'Проекторы','Устройства для проекции изображений и видео на экран.'),(74,'Электрические зубные щетки','Умные зубные щетки для эффективной гигиены полости рта.'),(75,'Умные термостаты','Устройства для управления температурой в доме через мобильное приложение.'),(76,'Системы умного дома','Комплекты для автоматизации и управления домом.'),(77,'Планшетные компьютеры','Устройства, сочетающие функции планшета и ноутбука.'),(78,'Камеры для видеозвонков','Web-камеры для общения и видеоконференций.'),(79,'Смарт-колонки','Колонки с голосовыми помощниками для управления устройствами и получения информации.'),(80,'Электронные книги','Устройства для чтения электронных книг с высоким разрешением экрана.'),(81,'Микроволновые печи','Приборы для быстрого разогрева и приготовления пищи.'),(82,'Кофемашины','Автоматические и полуавтоматические устройства для приготовления кофе.'),(83,'Пылесосы','Устройства для уборки, включая роботы-пылесосы.'),(84,'Стиральные машины','Бытовая техника для стирки одежды с различными функциями и режимами.'),(85,'Холодильники','Устройства для хранения продуктов с различными технологиями охлаждения.'),(86,'Сушилки для фруктов','Приборы для сушки фруктов и овощей.'),(87,'Мультиварки','Универсальные устройства для приготовления пищи.'),(88,'Электрические плиты','Приборы для приготовления пищи с электрическим нагревом.'),(89,'Блендеры','Устройства для смешивания и измельчения продуктов.'),(90,'Соковыжималки','Приборы для получения свежевыжатых соков.'),(91,'Чайники','Электрические и обычные чайники для кипячения воды.'),(92,'Планшеты для рисования','Устройства для цифрового рисования и графического дизайна.'),(93,'Системы охлаждения','Охлаждающие системы для компьютеров и серверов.'),(94,'Клавиатуры','Разнообразные клавиатуры для компьютеров и ноутбуков.'),(95,'Мыши','Оптические и лазерные мыши для работы и игр.'),(96,'Сетевые фильтры','Устройства для защиты от перенапряжения и распределения электроэнергии.'),(97,'Устройства для потокового видео','Приставки для просмотра потокового видео на телевизоре.'),(98,'Внешние акустические системы','Акустические системы для создания качественного звука.'),(99,'Картриджи для принтеров','Запасные картриджи для различных моделей принтеров.'),(100,'Принтеры','Устройства для печати документов и фотографий.'),(101,'Сканеры','Устройства для сканирования документов и фотографий.'),(102,'Беспроводные зарядные устройства','Устройства для беспроводной зарядки смартфонов');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customers`
--

DROP TABLE IF EXISTS `customers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customers` (
  `CustomerID` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(100) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `Address` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`CustomerID`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customers`
--

LOCK TABLES `customers` WRITE;
/*!40000 ALTER TABLE `customers` DISABLE KEYS */;
INSERT INTO `customers` VALUES (1,'Иван','Иванов','ivan.ivanov@yandex.ru','+7 (911) 123-45-67','Нижний Новгород, ул. Ленина, д. 1'),(2,'Анна','Петрова','anna.petrovna@gmail.com','+7 (911) 234-56-78','Нижний Новгород, пр. Невский, д. 2'),(3,'Сергей','Сидоров','sergey.sidorov@yandex.ru','+7 (911) 345-67-89','Нижний Новгород, ул. Мира, д. 3'),(4,'Ольга','Кузнецова','olga.kuznetsova@gmail.com','+7 (911) 456-78-90','Нижний Новгород, ул. Красный проспект, д. 4'),(5,'Дмитрий','Федоров','dmitry.fedorov@yandex.ru','+7 (911) 567-89-01','Нижний Новгород, ул. Баумана, д. 5'),(6,'Елена','Семенова','elena.semenova@gmail.com','+7 (911) 678-90-12','Нижний Новгород, ул. Горького, д. 6'),(7,'Александр','Григорьев','alexander.grigoryev@yandex.ru','+7 (911) 789-01-23','Нижний Новгород, ул. Труда, д. 7'),(8,'Мария','Соловьева','maria.solovyeva@gmail.com','+7 (911) 890-12-34','Нижний Новгород, ул. Ленина, д. 8'),(9,'Павел','Морозов','pavel.morozov@yandex.ru','+7 (911) 901-23-45','Нижний Новгород,  ул. Садовая, д. 9'),(10,'Ксения','Лебедева','ksenia.lebedyeva@gmail.com','+7 (911) 012-34-56','Нижний Новгород,  ул. Пушкина, д. 10');
/*!40000 ALTER TABLE `customers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employees`
--

DROP TABLE IF EXISTS `employees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employees` (
  `EmployeeID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `login` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `RollID` int NOT NULL,
  PRIMARY KEY (`EmployeeID`),
  KEY `1_idx` (`RollID`),
  CONSTRAINT `1` FOREIGN KEY (`RollID`) REFERENCES `roll` (`RollID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employees`
--

LOCK TABLES `employees` WRITE;
/*!40000 ALTER TABLE `employees` DISABLE KEYS */;
INSERT INTO `employees` VALUES (1,'vas','admin','8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918',1),(4,'qwe','seller','a4279eae47aaa7417da62434795a011ccb0ec870f7f56646d181b5500a892a9a',2),(5,'q','q','8e35c2cd3bf6641bdb0e2050b76932cbb2e6034a0ddacc1d9bea82a6ba57f7cf',1),(6,'t','t','8f545869b5360eea66774fa5a04cbefd7be9395aae7fa9dd41dab34136649c5f',2);
/*!40000 ALTER TABLE `employees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderitems`
--

DROP TABLE IF EXISTS `orderitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderitems` (
  `OrderItemID` int NOT NULL AUTO_INCREMENT,
  `OrderID` int DEFAULT NULL,
  `ProductID` int DEFAULT NULL,
  `Quantity` int NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  PRIMARY KEY (`OrderItemID`),
  KEY `OrderID` (`OrderID`),
  KEY `ProductID` (`ProductID`),
  CONSTRAINT `orderitems_ibfk_1` FOREIGN KEY (`OrderID`) REFERENCES `orders` (`OrderID`),
  CONSTRAINT `orderitems_ibfk_2` FOREIGN KEY (`ProductID`) REFERENCES `products` (`ProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderitems`
--

LOCK TABLES `orderitems` WRITE;
/*!40000 ALTER TABLE `orderitems` DISABLE KEYS */;
/*!40000 ALTER TABLE `orderitems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `OrderID` int NOT NULL AUTO_INCREMENT,
  `CustomerID` int NOT NULL,
  `EmployeesID` int NOT NULL,
  `OrderDate` datetime DEFAULT CURRENT_TIMESTAMP,
  `TotalAmount` decimal(10,2) DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`OrderID`),
  KEY `CustomerID` (`CustomerID`),
  KEY `orders_ibfk_2_idx` (`EmployeesID`),
  CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`CustomerID`),
  CONSTRAINT `orders_ibfk_2` FOREIGN KEY (`EmployeesID`) REFERENCES `employees` (`EmployeeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `products` (
  `ProductID` int NOT NULL AUTO_INCREMENT,
  `ProductName` varchar(100) NOT NULL,
  `Description` text,
  `Price` decimal(10,2) NOT NULL,
  `StockQuantity` int NOT NULL,
  `CategoryID` int DEFAULT NULL,
  `SuppliersID` int DEFAULT NULL,
  `Photo` longblob,
  `SKU` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ProductID`),
  UNIQUE KEY `SKU` (`SKU`),
  KEY `CategoryID` (`CategoryID`),
  KEY `SuppliersID` (`SuppliersID`),
  CONSTRAINT `products_ibfk_1` FOREIGN KEY (`CategoryID`) REFERENCES `categories` (`CategoryID`),
  CONSTRAINT `products_ibfk_2` FOREIGN KEY (`SuppliersID`) REFERENCES `suppliers` (`SuppliersID`)
) ENGINE=InnoDB AUTO_INCREMENT=147 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES (115,'Смартфон','Современный смартфон с высоким разрешением экрана и мощным процессором.',699.99,50,51,11,NULL,'SKU1001'),(116,'Ноутбук','Мощный ноутбук для работы и игр с длительным временем работы от батареи.',1299.99,30,52,12,NULL,'SKU1002'),(117,'Планшет','Легкий и портативный планшет с поддержкой стилуса.',399.99,75,54,15,NULL,'SKU1003'),(118,'Электронная книга','Удобная электронная книга с подсветкой и большим экраном.',129.99,150,80,13,NULL,'SKU1004'),(119,'Наушники','Беспроводные наушники с отличным звуком и шумоподавлением.',199.99,100,62,12,NULL,'SKU1005'),(120,'Умные часы','Умные часы с функциями мониторинга здоровья и уведомлениями.',249.99,60,61,13,NULL,'SKU1006'),(121,'Фитнес-трекер','Фитнес-трекер для отслеживания активности и сна.',99.99,200,61,16,NULL,'SKU1007'),(122,'Камера','Цифровая камера с высоким разрешением и множеством функций.',499.99,20,57,15,NULL,'SKU1008'),(123,'Игровая консоль','Современная игровая консоль с поддержкой онлайн-игр.',499.99,30,56,14,NULL,'SKU1009'),(124,'Дрон','Беспилотный летательный аппарат с камерой для съемки.',299.99,40,65,11,NULL,'SKU1010'),(125,'Смартфон X1','Смартфон с отличной камерой и долгим временем работы.',799.99,25,51,11,NULL,'SKU1011'),(126,'Ноутбук Y2','Ультратонкий ноутбук с мощным процессором.',1499.99,15,52,12,NULL,'SKU1012'),(127,'Планшет Z3','Планшет с большим экраном и высокой производительностью.',499.99,40,54,15,NULL,'SKU1013'),(128,'Камера A4','Профессиональная камера для фотографов.',899.99,10,57,14,NULL,'SKU1014'),(129,'Наушники B5','Наушники с активным шумоподавлением.',299.99,30,62,13,NULL,'SKU1015'),(130,'Умные часы C6','Часы с GPS и функциями отслеживания активности.',349.99,20,61,16,NULL,'SKU1016'),(131,'Фитнес-трекер D7','Устройство для отслеживания фитнес-активности.',129.99,50,61,12,NULL,'SKU1017'),(132,'Портативная колонка E8','Bluetooth колонка с хорошим звуком.',199.99,35,63,15,NULL,'SKU1018'),(133,'Игровая консоль F9','Игровая консоль нового поколения.',499.99,12,56,14,NULL,'SKU1019'),(134,'Дрон G10','Дрон с HD-камерой для съемки с воздуха.',399.99,18,65,11,NULL,'SKU1020'),(135,'Внешний жесткий диск H11','Жесткий диск на 2 ТБ для резервного копирования.',99.99,60,69,12,NULL,'SKU1021'),(136,'USB-накопитель I12','Флешка на 64 ГБ.',19.99,100,68,15,NULL,'SKU1022'),(137,'Проектор J13','Проектор с поддержкой Full HD.',499.99,20,73,14,NULL,'SKU1023'),(138,'Электрическая зубная щетка K14','Зубная щетка с режимами чистки.',79.99,80,74,15,NULL,'SKU1024'),(139,'Кофемашина L15','Автоматическая кофемашина для приготовления эспрессо.',299.99,25,82,12,NULL,'SKU1025'),(140,'Мультиварка M16','Умная мультиварка для быстрого приготовления.',149.99,40,87,11,NULL,'SKU1026'),(141,'Стиральная машина N17','Стиральная машина с функцией отжима.',499.99,15,84,14,NULL,'SKU1027'),(142,'Холодильник O18','Современный холодильник с системой No Frost.',799.99,10,85,13,NULL,'SKU1028'),(143,'Электрическая плита P19','Плита с индукционным нагревом.',399.99,20,88,12,NULL,'SKU1029'),(144,'Соковыжималка Q20','Соковыжималка для приготовления свежих соков.',129.99,50,90,11,NULL,'SKU1030'),(145,'Чайник R21','Электрический чайник с функцией поддержания температуры.',49.99,75,91,15,NULL,'SKU1031'),(146,'1','ыввывы',123.00,12,51,11,NULL,'123фва');
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roll`
--

DROP TABLE IF EXISTS `roll`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roll` (
  `RollID` int NOT NULL AUTO_INCREMENT,
  `NameRoll` varchar(100) NOT NULL,
  PRIMARY KEY (`RollID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roll`
--

LOCK TABLES `roll` WRITE;
/*!40000 ALTER TABLE `roll` DISABLE KEYS */;
INSERT INTO `roll` VALUES (1,'Admin'),(2,'Seller');
/*!40000 ALTER TABLE `roll` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `suppliers`
--

DROP TABLE IF EXISTS `suppliers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `suppliers` (
  `SuppliersID` int NOT NULL AUTO_INCREMENT,
  `SuppliersName` varchar(100) NOT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Phon` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`SuppliersID`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `suppliers`
--

LOCK TABLES `suppliers` WRITE;
/*!40000 ALTER TABLE `suppliers` DISABLE KEYS */;
INSERT INTO `suppliers` VALUES (11,'Электроника Плюс','info@electronicaplus.ru','+7 (495) 123-45-67'),(12,'ТехноМир','contact@technomir.ru','+7 (812) 234-56-78'),(13,'ГаджетСтор','support@gadgetstore.ru','+7 (499) 345-67-89'),(14,'Мир Электроники','sales@mir-electroniki.ru','+7 (383) 456-78-90'),(15,'СмартТех','info@smarttech.ru','+7 (495) 567-89-01'),(16,'ТехноГигант','contact@technogigant.ru','+7 (846) 678-90-12'),(17,'ЭлектроСнаб','info@electrosnab.ru','+7 (495) 789-01-23'),(18,'DigitalMarket','support@digitalmarket.ru','+7 (812) 890-12-34'),(19,'ЭкоТехника','sales@ekotechnika.ru','+7 (499) 901-23-45'),(20,'ПрофиЭлектро','info@proelectro.ru','+7 (495) 012-34-56');
/*!40000 ALTER TABLE `suppliers` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-03-06 15:19:37
