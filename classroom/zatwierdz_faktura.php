<?php
if (isset($total_price) and $total_price > 0) {
    include "polaczenie.php";
    $tabela = "faktury1";
    $id_ksiazek = "";
    $login = $_SESSION["login"];
    foreach ($_SESSION["shopping_cart"] as $product) {
        if ($product["quantity"] != 0) {
            $id_ksiazek =
                $id_ksiazek .
                $product["name"] .
                " * " .
                $product["quantity"] .
                ",";
        }
    }
    $zapytanie = "INSERT INTO $tabela SET id_faktury='', login_klienta='$login', id_ksiazki='$id_ksiazek',cena='$total_price';";
    $wynik = mysqli_query($sql, $zapytanie);
    unset($_SESSION["shopping_cart"]);
} else {
    echo "<a href = \"./home_user.php\">Powrót do strony głównej</a>";
}
?>