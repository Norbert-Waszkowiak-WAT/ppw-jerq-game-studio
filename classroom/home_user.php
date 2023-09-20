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
    echo "<form action=\"\" method=\"get\">";
    include "polaczenie.php";

    if (isset($_GET["wpisywanie"])) {
        switch ($_GET["wpisywanie"]) {
            case "autorzy":
                include "./wpisywanie/wpisywanie_autorzy.php";
                break;
            case "ksiazki":
                include "./wpisywanie/wpisywanie_ksiazki.php";
                break;
        }
    } elseif (isset($_GET["zatwierdz"])) {
        include "./zatwierdz_faktura.php";
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
        echo "<tr><td class=\"medium1\"><input style=\"width: 400px; height: 70px; font-size: 35px;text-align: center;background-color: rgba(173, 173, 173, 0.6);\" type=\"submit\" name=\"opcja\" value=\"ZAMÓWIENIA\"/></tr></td>";
        echo "<tr><td class=\"medium1\"><input style=\"width: 400px; height: 70px; font-size: 35px;text-align: center;background-color: rgba(173, 173, 173, 0.6);\"  type=\"submit\" name=\"opcja\" value=\"HISTORIA\"/></tr></td>";
        echo "<tr><td class=\"medium1\"><input style=\"width: 400px; height: 70px; font-size: 35px;text-align: center;background-color: rgba(173, 173, 173, 0.6);\" type=\"submit\" name=\"opcja\" value=\"KSIĄŻKI\"/></tr></td>";
        echo "</section>";
        echo "</table>";
    } elseif (isset($_GET["opcja"])) {
        switch ($_GET["opcja"]) {
            case "AUTORZY":
                include "przegladanie/przegladanie_autorzy.php";
                break;
            case "ZAMÓWIENIA":
                include "./nowe_zam.php";
                break;
            case "HISTORIA":
                include "./hist_zam.php";
                break;
            case "KSIĄŻKI":
                include "przegladanie/przegladanie_ksiazki.php";
                break;
        }
        echo "</table>";
    }
    echo "</table>";
    echo "</form>";
    ?>
</body>

</html>