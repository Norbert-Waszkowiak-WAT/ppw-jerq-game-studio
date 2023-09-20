<?php
include 'dbcon.php';
$tabela = "klienci";
$imie = $_GET['imie'];
$login = $_GET['login'];
$haslo = $_GET['haslo'];
$login_md = md5($login);
$haslo = md5($haslo);
$zapytanie = "INSERT INTO $tabela SET id_klienta='', login_md = '$login_md' ,imie='$imie', login='$login', haslo='$haslo';";
$wynik = mysqli_query($con, $zapytanie);
?>