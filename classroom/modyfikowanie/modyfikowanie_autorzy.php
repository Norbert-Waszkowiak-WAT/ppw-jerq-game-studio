<?php
include './polaczenie.php';
$tabela = "autorzy";
$zapytanie = "SELECT * FROM $tabela;";
$wynik = mysqli_query($sql, $zapytanie);
echo "<table>";
echo "<form action=\"\" method=\"get\">";
echo "<tr style = \"text-align: center;\"><td>ID_AUTORA</td><td>IMIE</td><td>NAZWISKO</td><td>DATA URODZENIA</td><td>NOTKA</td><td>Edycja</td></tr>";
while ($row = mysqli_fetch_array($wynik)) {
	echo "<tr><td class = \"small\">";
	echo $row['id_autora'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['imie'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['nazwisko'];
	echo "<td class = \"small\">";
	echo $row['data_ur'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['notka'];
	echo "</td>";
	$numer_autora = $row['id_autora'];
	echo "<td class = \"small\">";
	echo "<input type=\"submit\" name = \"numer_autora\" value=\"$numer_autora\">";
	echo "</td></tr>";
}
echo "</table>";
echo "</form>";
mysqli_close($sql);
?>
<footer style="position: absolute;
  bottom: -140px; font-size: 25px;
  width: 98.7%; text-align: center;" id="footer"><a href="home.php?opcja=AUTORZY">Powrót</a></footer>