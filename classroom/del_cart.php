<?php
session_start();
foreach ($_SESSION["shopping_cart"] as $key => $value) {
    if ($_POST["code"] == $key) {
        unset($_SESSION["shopping_cart"][$key]);
        $status = "<div style='color:red;'>
    Produkt usunięty!</div>";
    }
}
?>