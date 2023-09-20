<?php
include 'polaczenie.php';
$tabela = "autorzy";
$imie = $_GET['imie'];
$nazwisko = $_GET['nazwisko'];
$data_ur = $_GET['data_ur'];
$notka = $_GET['notka'];
$zapytanie = "INSERT INTO $tabela SET id_autora='', imie='$imie', nazwisko='$nazwisko', data_ur='$data_ur', notka='$notka';";
$wynik = mysqli_query($sql, $zapytanie);
?>