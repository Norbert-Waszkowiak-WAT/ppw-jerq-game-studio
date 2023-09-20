<?php
include('./polaczenie.php');
$tabela = "autorzy";
$imie = $_GET['imie'];
$nazwisko = $_GET['nazwisko'];
$data_ur = $_GET['data_ur'];
$notka = $_GET['notka'];
$id_a = $_GET['edycja_a'];
$zapytanie = "UPDATE $tabela SET imie='$imie', nazwisko='$nazwisko', data_ur='$data_ur', notka='$notka', id_autora = '$id_a' WHERE id_autora='$id_a';";
$wynik = mysqli_query($sql, $zapytanie);
mysqli_close($sql);
?>