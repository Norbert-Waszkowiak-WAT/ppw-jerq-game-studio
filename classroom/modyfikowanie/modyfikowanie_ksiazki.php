<?php
include './polaczenie.php';
$tabela = "ksiazki";
$zapytanie = "SELECT * FROM $tabela;";
$wynik = mysqli_query($sql, $zapytanie);
echo "<form action=\"\" method=\"get\">";
echo "<table class = \"przeglad\">";
echo "<tr><td class = \"medium\">ID KSIĄŻKI</td><td class = \"medium\">TYTUŁ</td><td class = \"medium\">ID AUTORA</td><td class = \"medium\">ISBN</td><td class = \"medium\">CENA</td><td class = \"medium\">ILOŚĆ</td><td class = \"medium\">VAT</td><td class = \"medium\">EDYCJA</td></tr>";
while ($row = mysqli_fetch_array($wynik)) {
	echo "<tr><td class = \"small\">";
	echo $row['id_ksiazki'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['tytul'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['id_autora'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['isbn'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['cena'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['ilosc'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['stawka_vat'] * 100;
	echo "%";
	echo "</td>";
	echo "<td class = \"small\">";
	$numer_ksiazki = $row['id_ksiazki'];
	echo "<input type=\"submit\" name = \"numer_ksiazki\" value=\"$numer_ksiazki\">";
	echo "</td></tr>";
}
echo "</table>";
echo "</form>";
mysqli_close($sql);
?>
<footer style="position: absolute;
  bottom: 10px; font-size: 25px;
  width: 98.7%; text-align: center;" id="footer"><a href="home.php?opcja=KSIĄŻKI">Powrót</a></footer>