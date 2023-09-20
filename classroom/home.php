<?php
include "dbcon.php";
session_start();
?>

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">

<head>
    <link rel="stylesheet" type="text/css" href="style.css">
    <link rel="icon" type="image/x-icon" href="./favicon.png">
    <title> Zalogowany jako <?php echo $_SESSION["login"]; ?></title>
</head>
<html>

<head>
    <link rel="stylesheet" type="text/css" href="style.css">
</head>

<body>
    <section class="tytul"><a href="index.php"><img src="./tytul.png"></a><a class="wyloguj" href="./logout.php">WYLOGUJ
            SIĘ</a></br></section>
    </table>

    <?php
    echo "<section class=\"vertical\">";
    echo "<form action=\"\" method=\"get\">";
    include "polaczenie.php";

    if (isset($_GET["wpisywanie"])) {
        switch ($_GET["wpisywanie"]) {
            case "autorzy":
                include "./wpisywanie/wpisywanie_autorzy.php";
                break;
            case "klienci":
                include "./wpisywanie/wpisywanie_klienci.php";
                break;
            case "ksiazki":
                include "./wpisywanie/wpisywanie_ksiazki.php";
                break;
        }
    } elseif (isset($_GET["numer_autora"])) {
        include "./edycja/edycja_autorzy.php";
    } elseif (isset($_GET["numer_ksiazki"])) {
        include "./edycja/edycja_ksiazki.php";
    } elseif (isset($_GET["numer_klienta"])) {
        include "./edycja/edycja_klienci.php";
    } elseif (isset($_GET["edycja_a"])) {
        include "./edycja/wpis_autorzy.php";
    } elseif (isset($_GET["edycja_ks"])) {
        include "./edycja/wpis_ksiazki.php";
    } elseif (isset($_GET["edycja_kl"])) {
        include "./edycja/wpis_klienci.php";
    } elseif (isset($_GET["usuwanie_autorzy"])) {
        include "./usuwanie/usuwanie_autorzy.php";
    } elseif (isset($_GET["usuwanie_klienci"])) {
        include "./usuwanie_klienci.php";
    } elseif (isset($_GET["usuwanie_ksiazki"])) {
        include "./usuwanie/usuwanie_ksiazki.php";
    }
    if (
        !isset($_GET["opcja"]) and
        !isset($_GET["akcja"]) and
        !isset($_GET["numer_autora"]) and
        !isset($_GET["numer_ksiazki"]) and
        !isset($_GET["numer_klienta"])
    ) {
        echo "<table class = \"wybor\">";
        echo "<th><tr><td class=\"big\">WYBIERZ OPCJĘ</td></tr></th>";
        echo "<tr><td class=\"medium1\"><input style=\"width: 400px; height: 70px; font-size: 35px;text-align: center;background-color: rgba(173, 173, 173, 0.6);\" type=\"submit\" name=\"opcja\" value=\"AUTORZY\"/></tr></td>";
        echo "<tr><td class=\"medium1\"><input style=\"width: 400px; height: 70px; font-size: 35px;text-align: center;background-color: rgba(173, 173, 173, 0.6);\"  type=\"submit\" name=\"opcja\" value=\"KLIENCI\"/></tr></td>";
        echo "<tr><td class=\"medium1\"><input style=\"width: 400px; height: 70px; font-size: 35px;text-align: center;background-color: rgba(173, 173, 173, 0.6);\"  type=\"submit\" name=\"opcja\" value=\"ZAMÓWIENIA\"/></tr></td>";
        echo "<tr><td class=\"medium1\"><input style=\"width: 400px; height: 70px; font-size: 35px;text-align: center;background-color: rgba(173, 173, 173, 0.6);\"  type=\"submit\" name=\"opcja\" value=\"KSIĄŻKI\"/></tr></td>";
        echo "</section>";
        echo "</table>";
    } elseif (isset($_GET["opcja"]) and !isset($_GET["akcja"])) {
        if ($_GET["opcja"] == "ZAMÓWIENIA") {
            include "./hist_zam_adm.php";
        } else {
            echo "<table class = \"wybor\">";
            $opcja = $_GET["opcja"];
            echo "<input type=\"hidden\" name = \"opcja\" value = \"$opcja\"";
            echo "<th><tr><td class = \"big\">WYBIERZ KATEGORIĘ</td></tr></th>";
            echo "<tr><td class=\"medium1\"><input style=\"width: 400px; height: 70px; font-size: 35px;text-align: center;background-color: rgba(173, 173, 173, 0.6);\" type=\"submit\" name=\"akcja\" value=\"DODAWANIE\" /></br></td></tr>";
            echo "<tr><td class=\"medium1\"><input style=\"width: 400px; height: 70px; font-size: 35px;text-align: center;background-color: rgba(173, 173, 173, 0.6);\" type=\"submit\" name=\"akcja\" value=\"MODYFIKOWANIE\" /></br></td></tr>";
            echo "<tr><td class=\"medium1\"><input style=\"width: 400px; height: 70px; font-size: 35px;text-align: center;background-color: rgba(173, 173, 173, 0.6);\" type=\"submit\" name=\"akcja\" value=\"PRZEGLĄDANIE\" /></br></td></tr>";
            echo "<footer style=\"position: absolute;
	bottom: 10px; font-size: 25px;
	width: 98%; text-align: center;\" id=\"footer\"><a href=\"home.php\">Powrót</a></footer>";
        }
    } elseif (isset($_GET["opcja"]) and isset($_GET["akcja"])) {
        switch ($_GET["akcja"]) {
            case "DODAWANIE":
                include "./dodawanie.php";
                break;
            case "PRZEGLĄDANIE":
                include "./przegladanie.php";
                break;
            case "MODYFIKOWANIE":
                include "./modyfikowanie.php";
                break;
        }
        echo "</table>";
    }
    echo "</table>";
    echo "</form>";
    ?>

</body>

</html>