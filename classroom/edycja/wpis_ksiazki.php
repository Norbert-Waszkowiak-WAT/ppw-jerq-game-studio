<?php
include('./polaczenie.php');
$tabela = "ksiazki";
$tytul = $_GET['tytul'];
$isbn = $_GET['isbn'];
$id_autora = $_GET['autor_numer'];
$cena = $_GET['cena'];
$ilosc = $_GET['ilosc'];
$stawka_vat = $_GET['stawka_vat'];
$id_ksiazki = $_GET['edycja_ks'];
$zapytanie1 = "UPDATE $tabela SET tytul='$tytul', isbn='$isbn', id_autora='$id_autora', cena='$cena', ilosc = '$ilosc', stawka_vat = '$stawka_vat' WHERE id_ksiazki='$id_ksiazki';";
$wynik = mysqli_query($sql, $zapytanie1);
mysqli_close($sql);
?>