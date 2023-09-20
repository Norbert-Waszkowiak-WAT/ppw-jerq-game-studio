<?php
$tabela = "ksiazki";
$tabela1 = "autorzy";
$zapytanie = "SELECT * FROM $tabela;";
$wynik = mysqli_query($sql, $zapytanie);
echo "<table class = \"przeglad\">";
echo "<tr><td class = \"medium\">ID_KSIĄŻKI</td><td class = \"medium\">AUTOR</td><td class = \"medium\">TYTUŁ</td><td class = \"medium\">ISBN</td><td class = \"medium\">ILOŚĆ</td><td class = \"medium\">STAWKA VAT</td><td class = \"medium\">CENA</td></tr>";
while ($row = mysqli_fetch_array($wynik)) {
	$zapytanie1 = "SELECT * FROM $tabela1;";
	$wynik1 = mysqli_query($sql, $zapytanie1);
	echo "<tr><td class = \"small\">";
	echo $row['id_ksiazki'];
	echo "</td>";
	echo "<td class = \"small\">";
	while ($row1 = mysqli_fetch_array($wynik1)) {
		if ($row1['id_autora'] == $row['id_autora']) {
			echo $row1['imie'] . " " . $row1['nazwisko'] . " ";
		}
	}
	echo "<td class = \"small\">";
	echo $row['tytul'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['isbn'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['ilosc'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['stawka_vat'] * 100;
	echo "%";
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['cena'];
	echo "zł";
	echo "</td>";
	echo "</tr></td>";
}
echo "</table>";
if($_SESSION["login"] == "ADMIN")
{
	echo "<footer style=\"position: absolute;
	bottom: 10px; font-size: 25px;
	width: 98%; text-align: center;\" id=\"footer\"><a href=\"home.php\">Powrót</a></footer>";
}
else{
	echo "<footer style=\"position: absolute;
	bottom: 10px; font-size: 25px;
	width: 98%; text-align: center;\" id=\"footer\"><a href=\"home_user.php\">Powrót</a></footer>";
}
?>