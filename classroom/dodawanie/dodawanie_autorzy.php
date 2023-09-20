<?php
echo "<table  class=\"wpis\">";
include './polaczenie.php';
echo "<tr><td style = \"width: 250px; font-size: 30px; text-align: center;\">Imię</tr></td>";
echo "<tr><td style = \"border: none;\" ><input  required = \"required\"  style = \"background-image: none; background-color: rgba(94,94,94,.7); width: 250px; height: 50px; font-size: 25px;\" type=\"text\" name=\"imie\" placeholder=\"Podaj imię\"/></tr></td>";
echo "<tr><td style = \"width: 25	0px; font-size: 30px; text-align: center;\">Nazwisko</tr></td>";
echo "<tr><td style = \"border: none;\"><input  required = \"required\"  style = \"background-image: none; background-color: rgba(94,94,94,.7); width: 250px; height: 50px; font-size: 25px;\" type=\"text\" name=\"nazwisko\" placeholder=\"Podaj nazwisko\"/></tr></td>";
echo "<tr><td style = \"width: 250px; font-size: 30px; text-align: center;\">Data Urodzenia</tr></td>";
echo "<tr><td style = \"border: none;\"><input style = \"background-image: none; background-color: rgba(94,94,94,.7); width: 250px; height: 50px; font-size: 25px;\" type=\"date\" name=\"data_ur\" /></tr></td>";
echo "<tr><td style = \"width: 250px; font-size: 30px; text-align: center;\">Notatka</tr></td>";
echo "<tr><td style = \"border: none;\"><textarea style=\"width: 250px; resize: none; font-size: 25px;\" name=\"notka\" cols=\"20\" rows=\"10\" placeholder=\"tutaj wpisz cos\"></textarea></tr></td>";
echo "<input type='hidden' name='wpisywanie' value='autorzy'>";
echo "<tr><td style = \"border: none;\"><input type=\"submit\" value=\"Dalej\"></tr></td>";
echo "</table>";
?>
<footer style="position: absolute;
  bottom: -140px; font-size: 25px;
  width: 98.7%; text-align: center;" id="footer"><a href="home.php?opcja=AUTORZY">Powrót</a></footer>