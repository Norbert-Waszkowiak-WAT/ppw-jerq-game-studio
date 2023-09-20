<?php
include './polaczenie.php';
$tabela = "autorzy";
$zapytanie = "SELECT * FROM $tabela;";
$wynik = mysqli_query($sql, $zapytanie);
echo "<section>";
echo "<table>";
echo "<tr><td style = \"width: 250px; font-size: 30px; text-align: center;\">Tytuł</tr></td>";
echo "<tr><td><input required = \"required\" style = \"background-image: none; background-color: rgba(94,94,94,.7); width: 250px; height: 50px; font-size: 25px;\" type=\"text\" name=\"tytul\" placeholder=\"Podaj tytuł\"/></tr></td>";
echo "<tr><td style = \"width: 250px; font-size: 30px; text-align: center;\">Autor</tr></td>";
echo "<tr><td><select style = \"background-image: none; background-color: rgba(94,94,94,.7); width: 250px; height: 50px; font-size: 25px;\" name=\"id_autora\" />";
while ($row = mysqli_fetch_array($wynik)) {
	echo "<option value=\"" . $row['id_autora'] . "\">";
	echo $row['imie'] . " " . $row['nazwisko'];
	echo "</option>";
}
echo "</select></tr></td>";
echo "<tr><td style = \"width: 250px; font-size: 30px; text-align: center;\">ISBN</tr></td>";
echo "<tr><td><input required = \"required\"  style = \"background-image: none; background-color: rgba(94,94,94,.7); width: 250px; height: 50px; font-size: 25px;\" type=\"text\" name=\"isbn\" placeholder=\"Podaj ISBN\"/></tr></td>";
echo "<tr><td style = \"width: 250px; font-size: 30px; text-align: center;\">Cena</tr></td>";
echo "<tr><td><input  required = \"required\" style = \"background-image: none; background-color: rgba(94,94,94,.7); width: 250px; height: 50px; font-size: 25px;\" type=\"text\" name=\"cena\" placeholder=\"Podaj cenę\"/></tr></td>";
echo "<tr><td style = \"width: 250px; font-size: 30px; text-align: center;\">Ilość</tr></td>";
echo "<tr><td><input  required = \"required\" style = \"background-image: none; background-color: rgba(94,94,94,.7); width: 250px; height: 50px; font-size: 25px;\" type=\"number\" name=\"ilosc\" placeholder=\"Podaj ilość\"/></tr></td>";
echo "<tr><td style = \"width: 250px; font-size: 30px; text-align: center;\">Stawka VAT</tr></td>";
echo "<tr><td><select style = \"background-image: none; background-color: rgba(94,94,94,.7); width: 250px; height: 50px; font-size: 25px;\" name=\"stawka_vat\">";
echo "<option value=\"0\">0%</option>";
echo "<option value=\"0.03\">3%</option>";
echo "<option value=\"0.05\">5%</option>";
echo "<option value=\"0.23\">23%</option>";
echo "</select></tr></td>";
echo "<tr><td><input type='hidden' name='wpisywanie' value='ksiazki'></tr></td>";
echo "<tr><td><input type=\"submit\" value=\"Dalej\"></tr></td>";
echo "</table>";
?>
<footer style="position: absolute;
  bottom: 10px; font-size: 25px;
  width: 98.7%; text-align: center;" id="footer"><a href="home.php?opcja=KSIĄŻKI">Powrót</a></footer>