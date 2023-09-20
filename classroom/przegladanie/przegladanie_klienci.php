<?php
$tabela = "klienci";
$zapytanie = "SELECT * FROM $tabela;";
$wynik = mysqli_query($sql, $zapytanie);
echo "<section>";
echo "<table class=\"przeglad\">";
echo "<tr><td class = \"medium\">ID_KLIENTA</td><td class = \"medium\">IMIE</td><td class = \"medium\">LOGIN</td></tr>";
while ($row = mysqli_fetch_array($wynik)) {
	echo "<tr><td class = \"small\">";
	echo $row['id_klienta'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['imie'];
	echo "</td>";
	echo "<td class = \"small\">";
	echo $row['login'];
	echo "</td></tr>";
}
echo "</table>";
echo "</section>";
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