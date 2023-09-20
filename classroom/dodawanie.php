<?php
switch ($_GET["opcja"]) {
    case "AUTORZY":
        include "dodawanie/dodawanie_autorzy.php";
        break;
    case "KLIENCI":
        include "dodawanie/dodawanie_klienci.php";
        break;
    case "KSIĄŻKI":
        include "dodawanie/dodawanie_ksiazki.php";
        break;
}
?>