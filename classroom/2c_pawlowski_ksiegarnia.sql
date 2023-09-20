-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 08 Sty 2023, 13:11
-- Wersja serwera: 10.4.25-MariaDB
-- Wersja PHP: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `2c_pawlowski_ksiegarnia`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `autorzy`
--

CREATE TABLE `autorzy` (
  `id_autora` int(10) UNSIGNED NOT NULL,
  `imie` varchar(40) CHARACTER SET utf8 COLLATE utf8_polish_ci NOT NULL,
  `nazwisko` varchar(100) CHARACTER SET utf8 COLLATE utf8_polish_ci NOT NULL,
  `data_ur` date DEFAULT NULL,
  `notka` varchar(255) CHARACTER SET utf8 COLLATE utf8_polish_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

--
-- Zrzut danych tabeli `autorzy`
--

INSERT INTO `autorzy` (`id_autora`, `imie`, `nazwisko`, `data_ur`, `notka`) VALUES
(1, 'Joanne', 'Murray', '1965-07-31', '1234'),
(2, ' William', 'Shakespeare', '1564-04-23', ''),
(3, 'Adam', 'Mickiewicz', '1789-12-24', ''),
(4, 'Juliusz', 'Słowacki', '1809-09-04', ''),
(5, 'Bolesław', 'Prus', '1847-08-20', ''),
(6, 'Weronika ', 'Łodyga', '2000-05-05', ''),
(7, 'Charles ', 'Dickens', '1812-06-09', ''),
(8, 'Antoine', 'de Saint-Exupéry', '1900-06-29', ''),
(9, 'Sandra', 'Lupin', '1999-10-10', ''),
(10, 'Edyta', 'Prusinowska', '2000-05-05', ''),
(11, 'something', 'random test 9', '2022-12-26', 'random');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `faktury1`
--

CREATE TABLE `faktury1` (
  `id_faktury` int(10) UNSIGNED NOT NULL,
  `id_ksiazki` text COLLATE utf8mb4_polish_ci NOT NULL,
  `cena` int(11) NOT NULL,
  `login_klienta` varchar(50) COLLATE utf8mb4_polish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

--
-- Zrzut danych tabeli `faktury1`
--

INSERT INTO `faktury1` (`id_faktury`, `id_ksiazki`, `cena`, `login_klienta`) VALUES
(1, 'Harry Potter i Kamień Filozoficzny * 1,Harry Potter i Więzień Azkabanu * 1,', 100, 'KvBA20'),
(2, 'Harry Potter i Kamień Filozoficzny * 1,Lalka * 1,Harry Potter i Kamień Filozoficzny * 1,Harry Potter i Więzień Azkabanu * 1,Lalka * 1,', 244, 'KvBA20'),
(3, 'Harry Potter i Więzień Azkabanu * 1,Romeło i Żulia * 4,', 246, 'kvba20'),
(4, 'Harry Potter i Więzień Azkabanu * 1,Harry Potter i Książe Półkrwi cz. 1 * 1,Harry Potter i Książe Półkrwi cz. 1 * 1,Lalka * 1,', 197, 'kvba20'),
(5, 'Harry Potter i Kamień Filozoficzny * 1,Harry Potter i Więzień Azkabanu * 1,Harry Potter i Książe Półkrwi cz. 1 * 1,Harry Potter i Książe Półkrwi cz. 1 * 1,Pan Tadeusz * 1,', 234, 'kvba20'),
(6, 'Harry Potter i Kamień Filozoficzny * 5,Angst - with happy ending * 5,Opowieść Wigilijna * 5,Mały Książe * 5,Pan Tadeusz * 5,Romeło i Żulia * 5,', 1185, 'jakiswymysl'),
(7, 'Lalka * 1,Romeło i Żulia * 1,Romeło i Żulia * 1,Romeło i Żulia * 1,', 194, 'KvBA20'),
(8, 'a taki chamski admin zmienił Ci zamówienie i co teraz zrobisz?', 50, 'Pp090909'),
(9, 'a taki chamski admin i zmienił Ci zamówienie i co teraz zrobisz?', 49, 'eloelo');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `klienci`
--

CREATE TABLE `klienci` (
  `id_klienta` int(10) NOT NULL,
  `login` varchar(50) COLLATE utf8mb4_polish_ci NOT NULL,
  `login_md` varchar(50) COLLATE utf8mb4_polish_ci NOT NULL,
  `haslo` varchar(50) COLLATE utf8mb4_polish_ci NOT NULL,
  `imie` varchar(50) COLLATE utf8mb4_polish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

--
-- Zrzut danych tabeli `klienci`
--

INSERT INTO `klienci` (`id_klienta`, `login`, `login_md`, `haslo`, `imie`) VALUES
(1, 'ADMIN', '73acd9a5972130b75066c82595a1fae3', '886eac5f7f02f07dd4262bc69d71eee9 ', 'ADMIN'),
(2, 'KvBA20', 'fb98d2756c3c9e4fd6c7993433540282', '81dc9bdb52d04dc20036dbd8313ed055', 'Kuba'),
(3, 'USER1', '9f693771ca12c43759045cdf4295e9f5', '81dc9bdb52d04dc20036dbd8313ed055', 'USER');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `ksiazki`
--

CREATE TABLE `ksiazki` (
  `id_ksiazki` int(10) UNSIGNED NOT NULL,
  `tytul` varchar(255) CHARACTER SET utf8 COLLATE utf8_polish_ci NOT NULL,
  `id_autora` int(10) UNSIGNED NOT NULL,
  `isbn` varchar(17) CHARACTER SET utf8 COLLATE utf8_polish_ci NOT NULL,
  `cena` float NOT NULL,
  `ilosc` int(10) UNSIGNED NOT NULL,
  `stawka_vat` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

--
-- Zrzut danych tabeli `ksiazki`
--

INSERT INTO `ksiazki` (`id_ksiazki`, `tytul`, `id_autora`, `isbn`, `cena`, `ilosc`, `stawka_vat`) VALUES
(1, 'Harry Potter i Zakon Feniksa', 1, '000-000-001', 50, 0, 0.05),
(2, 'Harry Potter i Kamień Filozoficzny', 1, '000-000-002', 50, 0, 0.03),
(3, 'Harry Potter i Więzień Azkabanu', 1, '000-000-003', 50, 0, 0.03),
(4, 'Harry Potter i Książe Półkrwi cz. 1', 1, '000-000-004', 50, 0, 0.03),
(5, 'Lalka', 5, '000-000-005', 47, 18, 0.03),
(6, 'Romeło i Żulia', 2, '1010001sadda', 49, 18, 0.07),
(8, 'Pan Tadeusz', 3, '000-000-008', 34, 0, 0.03),
(9, 'Mały Książe', 8, '000-000-009', 26, 28, 0.05),
(10, 'Opowieść Wigilijna', 7, '000-000-010', 34, 64, 0.05),
(11, 'Angst - with happy ending', 6, '000-000-011', 44, 39, 0.05);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Zrzut danych tabeli `users`
--

INSERT INTO `users` (`user_id`, `username`, `password`, `name`) VALUES
(1, 'ADMIN', '886eac5f7f02f07dd4262bc69d71eee9', 'ADMIN'),
(2, 'KvBA20', '81dc9bdb52d04dc20036dbd8313ed055', 'Jakub'),
(4, 'pp0909', 'f8bbef1561ca5388a21729dd792bd8a9', 'Piotrek'),
(5, '1234', '81dc9bdb52d04dc20036dbd8313ed055', ''),
(7, 'jakiswymysl', '3013ff71eca93cdc265e58c1d30ea22c', 'Barbara'),
(8, 'Pp090909', '81dc9bdb52d04dc20036dbd8313ed055', 'Piotr'),
(9, 'Janek20007', '3a58bae26f334431a44895126daa6fb9', 'jan'),
(10, 'Janek20007', '3a58bae26f334431a44895126daa6fb9', 'jan'),
(11, 'eloelo', 'f0bc85dacfc5e2e4ecf96acf8d73f315', 'elomelo');

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `autorzy`
--
ALTER TABLE `autorzy`
  ADD PRIMARY KEY (`id_autora`);

--
-- Indeksy dla tabeli `faktury1`
--
ALTER TABLE `faktury1`
  ADD PRIMARY KEY (`id_faktury`);

--
-- Indeksy dla tabeli `klienci`
--
ALTER TABLE `klienci`
  ADD PRIMARY KEY (`id_klienta`);

--
-- Indeksy dla tabeli `ksiazki`
--
ALTER TABLE `ksiazki`
  ADD PRIMARY KEY (`id_ksiazki`);

--
-- Indeksy dla tabeli `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `autorzy`
--
ALTER TABLE `autorzy`
  MODIFY `id_autora` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT dla tabeli `faktury1`
--
ALTER TABLE `faktury1`
  MODIFY `id_faktury` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT dla tabeli `klienci`
--
ALTER TABLE `klienci`
  MODIFY `id_klienta` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT dla tabeli `ksiazki`
--
ALTER TABLE `ksiazki`
  MODIFY `id_ksiazki` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT dla tabeli `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
