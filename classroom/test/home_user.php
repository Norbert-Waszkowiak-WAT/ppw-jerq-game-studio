<?php 
include('dbcon.php'); 
session_start();
?>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">

<html>
<head>
<link rel="stylesheet" type="text/css" href="style.css">
</head>
<body>
	<section class = "tytul"><a href="index.php"><img src="./tytul.png"></a><a class="wyloguj" href = "./logout.php">WYLOGUJ SIĘ</a></br></section>
	</table>
	
		<?php
			echo "<form action=\"\" method=\"get\">";
			include 'polaczenie.php';
			
			if(isset($_GET['wpisywanie']))
			{
				switch($_GET['wpisywanie'])
				{
					case 'autorzy':
					include './wpisywanie/wpisywanie_autorzy.php';
					break;
					case 'ksiazki':
					include './wpisywanie/wpisywanie_ksiazki.php';
					break;
				}
				
			}
			elseif(isset($_GET['zatwierdz']))
			{
				include './zatwierdz_faktura.php';
			}
			
			if(!isset($_GET['opcja']) and !isset($_GET['akcja']) and !isset($_GET['numer_autora']) and !isset($_GET['numer_ksiazki']) and !isset($_GET['numer_klienta']))
			{
				echo "<table class = \"wybor\">";
				echo "<th><tr><td class=\"big\">WYBIERZ OPCJĘ</td></tr></th>";
				echo "<tr><td class=\"medium1\"><input class=\"wpis\" type=\"submit\" name=\"opcja\" value=\"autorzy\"/></tr></td>";
				echo "<tr><td class=\"medium1\"><input class=\"wpis\"  type=\"submit\" name=\"opcja\" value=\"zamowienia\"/></tr></td>";
				echo "<tr><td class=\"medium1\"><input class=\"wpis\"  type=\"submit\" name=\"opcja\" value=\"historia\"/></tr></td>";
				echo "<tr><td class=\"medium1\"><input class=\"wpis\"  type=\"submit\" name=\"opcja\" value=\"ksiazki\"/></tr></td>";
				echo "</section>";
				echo "</table>";
			}
			elseif(isset($_GET['opcja']))
			{
				switch($_GET['opcja']) 
				{
				case "autorzy":
					include 'przegladanie/przegladanie_autorzy.php';
					break;
				case "zamowienia":
					include './nowe_zam.php';
					break;
				case "historia":
					include './hist_zam.php';
					break;
				case "ksiazki":
					include 'przegladanie/przegladanie_ksiazki.php';
					break;
				}
				echo "</table>";
			}
			echo "</table>";
			echo "</form>";
	
		?>
</body>
</html>