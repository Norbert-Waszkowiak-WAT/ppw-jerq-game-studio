<?php
include './dbcon.php';
$tabela = "klienci";
$temp = $_GET['numer_klienta'];
$zapytanie = "SELECT * FROM $tabela WHERE id_klienta = $temp;";
$wynik = mysqli_query($con, $zapytanie);
$row = mysqli_fetch_array($wynik);
$imie = $row['imie'];
$login = $row['login'];
$haslo = $row['haslo'];
echo "<table>";
echo "<tr><td class = \"medium\">Imie</tr></td>";
echo "<tr><td class = \"small\"><input style = \"width: 250px; height: 50px; font-size: 25px; background-image: none; background-color: rgba(94,94,94,.7)\" type=\"text\" name=\"name\" value = \"$imie\"/></tr></td>";
echo "<tr><td class = \"medium\">Login</tr></td>";
echo "<tr><td class = \"small\"><input style = \"width: 250px; height: 50px; font-size: 25px; background-image: none; background-color: rgba(94,94,94,.7)\" type=\"text\" name=\"username\" value = \"$login\"/></tr></td>";
echo "<tr><td class = \"medium\">Hasło</tr></td>";
echo "<tr><td class = \"small\"><input style = \"width: 250px; height: 50px; font-size: 25px; background-image: none; background-color: rgba(94,94,94,.7)\" type=\"text\" name=\"password\" value=\"Podaj hasło\"/></tr></td>";
echo "<input type = \"hidden\" name = \"temp\" value = \"$temp\">";
$numer_klienta = $row['id_klienta'];

echo "<td class = \"small\">";
	$id = $row['id_klienta'];
	echo "<form method = \"get\"><input type = \"hidden\" name = \"edycja_kl\" value=\"$numer_klienta\"></form>";
	echo "<input style = \"text-align : center; width: 100px; none;\" type = \"submit\" value=\"Dalej\">";
	echo "</td></tr>";
echo "<tr><td class = \"medium\"><input \"text-align : center; width: 100px; none;\" type=\"submit\" name = \"usuwanie_klienci\" value=\"Usuń\"></tr></td>";
echo "</table>";
mysqli_close($con);
?>
<footer style="position: absolute;
  bottom: 10px; font-size: 25px;
  width: 98.7%; text-align: center;" id="footer"><a href="home.php?opcja=KLIENCI">Powrót</a></footer>