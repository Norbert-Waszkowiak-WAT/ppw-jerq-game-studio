<?php
include './polaczenie.php';
$tabela = "autorzy";
$temp = $_GET['numer_autora'];
$zapytanie = "SELECT * FROM $tabela WHERE id_autora = $temp;";
$wynik = mysqli_query($sql, $zapytanie);
$row = mysqli_fetch_array($wynik);
$imie = $row['imie'];
$nazwisko = $row['nazwisko'];
$data_ur = $row['data_ur'];
$notka = $row['notka'];
echo "<table class = \"wpis\">";
echo "<tr><td>Imie</tr></td>";
echo "<tr><td><input style = \"width: 250px; height: 50px; font-size: 25px; background-image: none; background-color: rgba(94,94,94,.7)\" type=\"text\" name=\"imie\" value = \"$imie\"/></tr></td>";
echo "<tr><td>Nazwisko</tr></td>";
echo "<tr><td><input style = \"width: 250px; height: 50px; font-size: 25px; background-image: none; background-color: rgba(94,94,94,.7)\" type=\"text\" name=\"nazwisko\" value = \"$nazwisko\"/></tr></td>";
echo "<tr><td>Data</tr></td>";
echo "<tr><td><input style = \"width: 250px; height: 50px; font-size: 25px; background-image: none; background-color: rgba(94,94,94,.7)\" type=\"date\" name=\"data_ur\" value = \"$data_ur\"/></tr></td>";
echo "<tr><td>Notatka</tr></td>";
echo "<tr><td><textarea style = \"resize: none; width: 250px; font-size: 25px; background-image: none; background-color: rgba(94,94,94,.7)\" name=\"notka\" cols=\"20\" rows=\"10\" value = \"$notka\"></textarea></tr></td>";

$numer_autora = $row['id_autora'];
echo "<input type=\"hidden\" name =\"edycja_a\" value=\"$numer_autora\">";
echo "<tr style = \"text-align: center;\"><td><input type=\"submit\" value=\"Zapisz\"></tr></td>";
echo "</table>";

mysqli_close($sql);
?>
<footer style="position: absolute;
  bottom: -140px; font-size: 25px;
  width: 98.7%; text-align: center;" id="footer"><a href="home.php?opcja=AUTORZY">Powrót</a></footer>