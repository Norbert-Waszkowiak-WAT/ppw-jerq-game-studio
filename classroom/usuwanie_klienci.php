<?php
include "./dbcon.php";
$id = $_GET["temp"];
$zapytanie = "DELETE FROM `klienci` WHERE `id_klienta` = $id";
$wynik = mysqli_query($con, $zapytanie);
mysqli_close($con);
?>