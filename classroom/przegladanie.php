<?php
	switch($_GET['opcja']){
		case "AUTORZY";
			include 'przegladanie/przegladanie_autorzy.php';
			break;
		case "KLIENCI";
			include 'przegladanie/przegladanie_klienci.php';
			break;
		case "KSIĄŻKI";
			include 'przegladanie/przegladanie_ksiazki.php';
			break;
	}
?>