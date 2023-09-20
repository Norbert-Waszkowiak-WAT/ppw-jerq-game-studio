<?php
include './dbcon.php';
$tabela = "klienci";
$zapytanie = "SELECT * FROM $tabela;";
$wynik = mysqli_query($sql, $zapytanie);
echo "<table>";
echo "<form>";
echo "<tr><td class = \"medium\">ID KLIENTA</td><td class = \"medium\">IMIE</td><td class = \"medium\">LOGIN</td><td class = \"medium\">HASŁO</td></tr>";
while ($row = mysqli_fetch_array($wynik)) {
	echo "<tr><td class = \"small\">";
	echo $row['id_klienta'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['imie'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['login'];
	echo "</td>";
	$numer_klienta = $row['id_klienta'];
	echo "<td class = \"small\">";
	echo "<input type=\"submit\" name = \"numer_klienta\" value=\"$numer_klienta\">";
	echo "</td></tr>";
}
echo "</table>";
echo "</form>";
mysqli_close($sql);
?>
<footer style="position: absolute;
  bottom: 10px; font-size: 25px;
  width: 98.7%; text-align: center;" id="footer"><a href="home.php?opcja=KLIENCI">Powrót</a></footer>