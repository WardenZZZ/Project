-- phpMyAdmin SQL Dump
-- version 4.9.5
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: May 18, 2021 at 08:24 AM
-- Server version: 5.7.24
-- PHP Version: 7.4.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `project`
--

-- --------------------------------------------------------

--
-- Table structure for table `cond`
--

CREATE TABLE `cond` (
  `id_cond` int(10) NOT NULL,
  `condname` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `cond`
--

INSERT INTO `cond` (`id_cond`, `condname`) VALUES
(1, 'Активное'),
(2, 'Неактивное'),
(3, 'На складе'),
(4, 'Ремонт'),
(5, 'Списано');

-- --------------------------------------------------------

--
-- Table structure for table `dept`
--

CREATE TABLE `dept` (
  `id_dept` int(10) NOT NULL,
  `deptname` varchar(20) NOT NULL,
  `office` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `dept`
--

INSERT INTO `dept` (`id_dept`, `deptname`, `office`) VALUES
(1, 'Brazil', 'Hell'),
(2, 'Podolsk', 'Ad'),
(3, 'Ukraine', 'Kiev'),
(4, 'Redgrave', 'DMC');

-- --------------------------------------------------------

--
-- Table structure for table `hardtypes`
--

CREATE TABLE `hardtypes` (
  `id_hardtype` int(10) NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `hardtypes`
--

INSERT INTO `hardtypes` (`id_hardtype`, `name`) VALUES
(1, 'CPU'),
(2, 'GPU'),
(3, 'RAM'),
(4, 'HardDrive'),
(5, 'Motherboard'),
(6, 'PowerSupply'),
(7, 'Fan');

-- --------------------------------------------------------

--
-- Table structure for table `hardware`
--

CREATE TABLE `hardware` (
  `id_hardware` int(10) NOT NULL,
  `id_hardtype` int(10) NOT NULL,
  `id_cond` int(10) NOT NULL,
  `qty` int(5) NOT NULL,
  `model` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `hardware`
--

INSERT INTO `hardware` (`id_hardware`, `id_hardtype`, `id_cond`, `qty`, `model`) VALUES
(1, 1, 1, 50, 'Ryzen 2700X'),
(2, 2, 2, 100, 'RTX 3080ti'),
(3, 3, 2, 38, 'Corsair 3200Mhz'),
(4, 4, 1, 50, 'Western Digital podolsk edition'),
(5, 5, 1, 20, 'Asus e3progaming v5'),
(6, 6, 2, 50, '750W AeroCool KCAS-750GM'),
(7, 7, 2, 16, 'Everage'),
(8, 2, 2, 22, '22'),
(9, 1, 2, 43, '55'),
(10, 2, 1, 10, 'aaaaa');

-- --------------------------------------------------------

--
-- Table structure for table `pc acc`
--

CREATE TABLE `pc acc` (
  `id_pc` int(10) NOT NULL,
  `id_cond` int(10) NOT NULL,
  `id_dept` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `pc acc`
--

INSERT INTO `pc acc` (`id_pc`, `id_cond`, `id_dept`) VALUES
(1, 1, 1),
(5, 1, 3),
(4, 4, 1);

-- --------------------------------------------------------

--
-- Table structure for table `peri`
--

CREATE TABLE `peri` (
  `id_peri` int(10) NOT NULL,
  `id_peritype` int(10) NOT NULL,
  `id_cond` int(10) NOT NULL,
  `qty` int(5) NOT NULL,
  `model` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `peri`
--

INSERT INTO `peri` (`id_peri`, `id_peritype`, `id_cond`, `qty`, `model`) VALUES
(2, 2, 3, 30, 'Sven'),
(3, 3, 2, 29, 'Sony w400'),
(4, 4, 1, 5, 'HyperX QuadCast s'),
(5, 5, 4, 10, 'DxRacer'),
(6, 6, 1, 111, 'everage'),
(7, 7, 4, 30, 'Xbox gamepad'),
(8, 8, 3, 10, 'defender'),
(9, 1, 1, 50, 'coc'),
(10, 2, 4, 25, '50');

-- --------------------------------------------------------

--
-- Table structure for table `peritypes`
--

CREATE TABLE `peritypes` (
  `id_peritype` int(10) NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `peritypes`
--

INSERT INTO `peritypes` (`id_peritype`, `name`) VALUES
(1, 'mouse'),
(2, 'keyboard'),
(3, 'headphones'),
(4, 'microphone'),
(5, 'Chair'),
(6, 'RGB tape'),
(7, 'gamepad'),
(8, 'web cam');

-- --------------------------------------------------------

--
-- Table structure for table `post`
--

CREATE TABLE `post` (
  `id_post` int(10) NOT NULL,
  `postname` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `post`
--

INSERT INTO `post` (`id_post`, `postname`) VALUES
(1, 'Admin');

-- --------------------------------------------------------

--
-- Table structure for table `softtypes`
--

CREATE TABLE `softtypes` (
  `id_typesoft` int(10) NOT NULL,
  `Name` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `softtypes`
--

INSERT INTO `softtypes` (`id_typesoft`, `Name`) VALUES
(1, 'Специализированное'),
(2, 'Общее');

-- --------------------------------------------------------

--
-- Table structure for table `software`
--

CREATE TABLE `software` (
  `id_soft` int(10) NOT NULL,
  `softname` varchar(50) NOT NULL,
  `softtype` int(10) NOT NULL,
  `id_pc` int(10) NOT NULL,
  `license_start` date NOT NULL,
  `license_end` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `software`
--

INSERT INTO `software` (`id_soft`, `softname`, `softtype`, `id_pc`, `license_start`, `license_end`) VALUES
(1, 'Word', 1, 4, '2021-05-17', '2021-05-19'),
(2, 'Word', 1, 1, '2021-05-18', '2021-05-28'),
(3, 'Visual studio', 2, 1, '2020-07-14', '2020-11-10'),
(4, 'PowerPoint', 1, 4, '2021-05-13', '2021-05-25'),
(5, 'Handbrake', 2, 4, '2021-05-17', '2021-10-14'),
(6, 'Excel', 1, 5, '2021-05-11', '2021-05-31'),
(8, 'ff15', 1, 4, '2021-05-06', '2021-05-07');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id_user` int(10) NOT NULL,
  `login` varchar(20) NOT NULL,
  `pass` varchar(20) NOT NULL,
  `id_post` int(10) NOT NULL,
  `fname` varchar(30) NOT NULL,
  `lname` varchar(30) NOT NULL,
  `thname` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id_user`, `login`, `pass`, `id_post`, `fname`, `lname`, `thname`) VALUES
(1, 'admin1', 'admin2', 1, 'Sirgay', 'Klen', 'Semenovich');

-- --------------------------------------------------------

--
-- Table structure for table `workers`
--

CREATE TABLE `workers` (
  `id_worker` int(10) NOT NULL,
  `fname` varchar(30) NOT NULL,
  `lname` varchar(30) NOT NULL,
  `thname` varchar(30) NOT NULL,
  `id_post` int(10) NOT NULL,
  `id_dept` int(10) NOT NULL,
  `id_pc` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cond`
--
ALTER TABLE `cond`
  ADD PRIMARY KEY (`id_cond`);

--
-- Indexes for table `dept`
--
ALTER TABLE `dept`
  ADD PRIMARY KEY (`id_dept`);

--
-- Indexes for table `hardtypes`
--
ALTER TABLE `hardtypes`
  ADD PRIMARY KEY (`id_hardtype`);

--
-- Indexes for table `hardware`
--
ALTER TABLE `hardware`
  ADD PRIMARY KEY (`id_hardware`),
  ADD KEY `id_hardtype` (`id_hardtype`,`id_cond`),
  ADD KEY `id_cond` (`id_cond`);

--
-- Indexes for table `pc acc`
--
ALTER TABLE `pc acc`
  ADD PRIMARY KEY (`id_pc`),
  ADD KEY `id_cond` (`id_cond`,`id_dept`),
  ADD KEY `id_dept` (`id_dept`);

--
-- Indexes for table `peri`
--
ALTER TABLE `peri`
  ADD PRIMARY KEY (`id_peri`),
  ADD KEY `id_peritype` (`id_peritype`,`id_cond`),
  ADD KEY `id_cond` (`id_cond`);

--
-- Indexes for table `peritypes`
--
ALTER TABLE `peritypes`
  ADD PRIMARY KEY (`id_peritype`);

--
-- Indexes for table `post`
--
ALTER TABLE `post`
  ADD PRIMARY KEY (`id_post`);

--
-- Indexes for table `softtypes`
--
ALTER TABLE `softtypes`
  ADD PRIMARY KEY (`id_typesoft`);

--
-- Indexes for table `software`
--
ALTER TABLE `software`
  ADD PRIMARY KEY (`id_soft`),
  ADD KEY `FK_Software_Softtypes` (`softtype`),
  ADD KEY `id_pc` (`id_pc`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id_user`),
  ADD KEY `id_post` (`id_post`);

--
-- Indexes for table `workers`
--
ALTER TABLE `workers`
  ADD PRIMARY KEY (`id_worker`),
  ADD KEY `id_post` (`id_post`,`id_dept`,`id_pc`),
  ADD KEY `id_dept` (`id_dept`),
  ADD KEY `id_pc` (`id_pc`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `cond`
--
ALTER TABLE `cond`
  MODIFY `id_cond` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `dept`
--
ALTER TABLE `dept`
  MODIFY `id_dept` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `hardtypes`
--
ALTER TABLE `hardtypes`
  MODIFY `id_hardtype` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `hardware`
--
ALTER TABLE `hardware`
  MODIFY `id_hardware` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `pc acc`
--
ALTER TABLE `pc acc`
  MODIFY `id_pc` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `peri`
--
ALTER TABLE `peri`
  MODIFY `id_peri` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `peritypes`
--
ALTER TABLE `peritypes`
  MODIFY `id_peritype` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `post`
--
ALTER TABLE `post`
  MODIFY `id_post` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `softtypes`
--
ALTER TABLE `softtypes`
  MODIFY `id_typesoft` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `software`
--
ALTER TABLE `software`
  MODIFY `id_soft` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id_user` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `workers`
--
ALTER TABLE `workers`
  MODIFY `id_worker` int(10) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `hardware`
--
ALTER TABLE `hardware`
  ADD CONSTRAINT `hardware_ibfk_1` FOREIGN KEY (`id_cond`) REFERENCES `cond` (`id_cond`),
  ADD CONSTRAINT `hardware_ibfk_2` FOREIGN KEY (`id_hardtype`) REFERENCES `hardtypes` (`id_hardtype`);

--
-- Constraints for table `pc acc`
--
ALTER TABLE `pc acc`
  ADD CONSTRAINT `pc acc_ibfk_1` FOREIGN KEY (`id_cond`) REFERENCES `cond` (`id_cond`),
  ADD CONSTRAINT `pc acc_ibfk_3` FOREIGN KEY (`id_dept`) REFERENCES `dept` (`id_dept`);

--
-- Constraints for table `peri`
--
ALTER TABLE `peri`
  ADD CONSTRAINT `peri_ibfk_1` FOREIGN KEY (`id_peritype`) REFERENCES `peritypes` (`id_peritype`),
  ADD CONSTRAINT `peri_ibfk_2` FOREIGN KEY (`id_cond`) REFERENCES `cond` (`id_cond`);

--
-- Constraints for table `software`
--
ALTER TABLE `software`
  ADD CONSTRAINT `FK_Software_PC` FOREIGN KEY (`id_pc`) REFERENCES `pc acc` (`id_pc`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Software_Softtypes` FOREIGN KEY (`softtype`) REFERENCES `softtypes` (`id_typesoft`);

--
-- Constraints for table `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`id_post`) REFERENCES `post` (`id_post`);

--
-- Constraints for table `workers`
--
ALTER TABLE `workers`
  ADD CONSTRAINT `workers_ibfk_1` FOREIGN KEY (`id_post`) REFERENCES `post` (`id_post`),
  ADD CONSTRAINT `workers_ibfk_2` FOREIGN KEY (`id_dept`) REFERENCES `dept` (`id_dept`),
  ADD CONSTRAINT `workers_ibfk_3` FOREIGN KEY (`id_pc`) REFERENCES `pc acc` (`id_pc`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
