<link rel="stylesheet" type="text/css" href="style.css">
<?php
echo "<table>";
echo "<tr><td style = \"width: 250px; font-size: 30px; text-align: center;\">Imię</tr></td>";
echo "<tr><td style = \"border: none;\"><input  required = \"required\" style = \"background-image: none; background-color: rgba(94,94,94,.7); width: 250px; height: 50px; font-size: 25px;\" type=\"text\" name=\"imie\" placeholder=\"Podaj imię\"/></tr></td>";
echo "<tr><td style = \"width: 250px; font-size: 30px; text-align: center;\">Login</tr></td>";
echo "<tr><td style = \"border: none;\"><input  required = \"required\" style = \"background-image: none; background-color: rgba(94,94,94,.7); width: 250px; height: 50px; font-size: 25px;\" type=\"text\" name=\"login\" placeholder=\"Podaj login\"/></tr></td>";
echo "<tr><td style = \"width: 250px; font-size: 30px; text-align: center;\">Hasło</tr></td>";
echo "<tr><td style = \"border: none;\"><input  required = \"required\" style = \"background-image: none; background-color: rgba(94,94,94,.7); width: 250px; height: 50px; font-size: 25px;\" type=\"password\" name=\"haslo\" placeholder=\"Podaj hasło\"/></tr></td>";
echo "<tr><td><input type='hidden' name='wpisywanie' value='klienci'></tr></td>";
echo "<tr><td style = \"width: 250px; font-size: 30px; text-align: center;\"><input type=\"submit\" value=\"Dalej\"></tr></td>";
echo "</table>";
?>
<footer style="position: absolute;
  bottom: 10px; font-size: 25px;
  width: 98.7%; text-align: center;" id="footer"><a href="home.php?opcja=KLIENCI">Powrót</a></footer>