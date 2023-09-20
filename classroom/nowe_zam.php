<title>Nowe zamówienie</title>
<?php
include "dbcon.php";
$status = "";
if (isset($_POST["code"]) && $_POST["code"] != "") {
    $code = $_POST["code"];
    $result = mysqli_query(
        $con,
        "SELECT * FROM `ksiazki` WHERE `id_ksiazki`='$code'"
    );
    $row = mysqli_fetch_assoc($result);
    $name = $row["tytul"];
    $code = $row["id_ksiazki"];
    $price = $row["cena"];

    $cartArray = [
        $code => [
            "name" => $name,
            "code" => $code,
            "price" => $price,
            "quantity" => 1,
        ],
    ];


    if (empty($_SESSION["shopping_cart"])) {
        $_SESSION["shopping_cart"] = $cartArray;
        $status = "<div class='box'>Produkt dodany do koszyka!</div>";
    } else {
        $array_keys = array_keys($_SESSION["shopping_cart"]);
        if (in_array($code, $array_keys)) {
            $status = "<div class='box' style='color:red;'>
		Produkt został dodany wcześniej do koszyka!</div>";
        } else {
            $_SESSION["shopping_cart"] = array_merge(
                $_SESSION["shopping_cart"],
                $cartArray
            );
            $status = "<div class='box'>Produkt dodany do koszyka!</div>";
        }
    }
}
?>
<html>

<head>
    <link rel='stylesheet' href='css/style.css' type='text/css' media='all' />
</head>

<body>
    <?php
    $result = mysqli_query($con, "SELECT * FROM `ksiazki`");
    echo "<table style = \"margin-top: 15px ;border: solid white 2px; background: rgba(94,94,94,.7)\">";
    while ($row = mysqli_fetch_assoc($result)) {
        if ($row["id_ksiazki"] == "1") {
            echo "
			  <form method='post' action=''>
			  <input type='hidden' name='code' value=" .
                $row["id_ksiazki"] .
                " />
			  <tr><td>" .
                $row["tytul"] .
                "</td></tr>
		   	  <tr><td>&nbsp" .
                $row["cena"] .
                "zł</td></tr>
			  <tr><td><button style = \"background: rgba(162,45,45,.7)\" type='submit' disabled=\"disabled\" class='buy'>Produkt niedostępny</button></td></tr>
			  </form>";
        } else {
            echo "
			  <form method='post' action=''>
			  <input type='hidden' name='code' value=" .
                $row["id_ksiazki"] .
                " />
			  <tr><td>" .
                $row["tytul"] .
                "</td></tr>
		   	  <tr><td>&nbsp" .
                $row["cena"] .
                "zł</td></tr>
			  <tr><td><button style=\"background: rgba(92,92,92, .7)\" type='submit' class='buy'>Dodaj do koszyka</button></td></tr>
			  </form>";
        }
    }
    mysqli_close($con);
    ?>

    <div style="clear:both;"></div>
    <?php if (!empty($_SESSION["shopping_cart"])) {
        
        
        $cart_count = count(array_keys($_SESSION["shopping_cart"])); ?>
        <div class="cart_div">
            <a style="padding-top: 15px;font-size: 30px; float: right;" href="cart.php"><img
                    style="float: right; height: 75px; width: 85px;" src="cart.png" />Ilość artykułów w koszyku: &nbsp
                <span>
                    <?php echo $cart_count; ?>
                </span></a>
        </div>
        <?php
    } ?>
    <table style="text-align: center; margin-left:auto; margin-right:auto;">
        <tr>
            <td style="background: rgba(69,69,69,.69)">
                <?php echo $status; ?>
            </td>
        </tr>
    </table>
    <footer style="position: absolute;
  bottom: 10px; font-size: 25px;
  width: 100%; text-align: center;" id="footer"><a href="home_user.php">Powrót</a></footer>
</body>
</html>