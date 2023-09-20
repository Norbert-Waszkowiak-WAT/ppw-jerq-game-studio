<?php
include 'polaczenie.php';
$tabela = "ksiazki";
$tytul = $_GET['tytul'];
$isbn = $_GET['isbn'];
$cena = $_GET['cena'];
$ilosc = $_GET['ilosc'];
$stawka_vat = $_GET['stawka_vat'];
$id_autora = $_GET['id_autora'];
$zapytanie = "INSERT INTO $tabela SET id_autora = '$id_autora', id_ksiazki='', tytul='$tytul', isbn='$isbn', cena='$cena', ilosc='$ilosc', stawka_vat = '$stawka_vat';";
$wynik = mysqli_query($sql, $zapytanie);
?>