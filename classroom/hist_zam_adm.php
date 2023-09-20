<?php
$tabela = "faktury1";
$login = $_SESSION["login"];
$zapytanie = "SELECT * FROM $tabela;";
$wynik = mysqli_query($sql, $zapytanie);
echo "<section>";
echo "<table class=\"przeglad\">";
echo "<tr><td class = \"medium\">ID FAKTURY</td><td class = \"medium\">ID KLIENTA</td><td class = \"medium\">KSIAZKI</td><td class = \"medium\">CENA</td></tr>";
while ($row = mysqli_fetch_array($wynik)) {
    echo "<tr><td class = \"small\">";
    echo $row["id_faktury"];
    echo "</td>";
    echo "<td class = \"small\">";
    echo $row["login_klienta"];
    echo "</td>";
    echo "<td class = \"small\">";
    echo $row["id_ksiazki"];
    echo "</td>";
    echo "<td class = \"small\">";
    echo $row["cena"] . "zł";
    echo "</tr></td>";
}
echo "</table>";
echo "</section>";
?>
<footer style="position: absolute;
  bottom: 10px; font-size: 25px;
  width: 100%; text-align: center;" id="footer"><a href="home.php">Powrót</a></footer>