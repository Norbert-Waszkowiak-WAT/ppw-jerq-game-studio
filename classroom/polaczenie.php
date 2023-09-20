<?php
$url = 'localhost';
$user = 'root';
$password = '';
$baza_danych = '2c_pawlowski_ksiegarnia';
$sql = mysqli_connect($url, $user, $password, $baza_danych);
if (!$sql) {
	die("Connection failed: " . mysqli_connect_error());
}
?>