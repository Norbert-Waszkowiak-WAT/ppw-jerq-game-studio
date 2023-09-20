<?php
	switch($_GET['opcja']){
		case "AUTORZY";
			include 'modyfikowanie/modyfikowanie_autorzy.php';
			break;
		case "KLIENCI";
			include 'modyfikowanie/modyfikowanie_klienci.php';
			break;
		case "KSIĄŻKI";
			include 'modyfikowanie/modyfikowanie_ksiazki.php';
			break;
	}
?>