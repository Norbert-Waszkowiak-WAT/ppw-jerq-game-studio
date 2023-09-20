<?php
include('./dbcon.php');
$tabela = "klienci";
$imie = $_GET['name'];
$login = $_GET['username'];
$haslo = $_GET['password'];
$haslo = md5($haslo);
$login_md = md5($login);
$id = $_GET['edycja_kl'];
$zapytanie = "UPDATE `klienci` SET `login_md` = '$login_md', `imie`='$imie',`login`='$login' ,`haslo`='$haslo 'WHERE `id_klienta` = $id";
$wynik = mysqli_query($con, $zapytanie);
mysqli_close($con);
?>