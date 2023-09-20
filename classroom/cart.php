<?php
session_start();
$status = "";
if (isset($_POST["action"]) && $_POST["action"] == "remove") {
    if (!empty($_SESSION["shopping_cart"])) {
        foreach ($_SESSION["shopping_cart"] as $key => $value) {
            if ($_POST["code"] == $key) {
                unset($_SESSION["shopping_cart"][$key]);
                $status = "<div style='color:red;'>
		Produkt usunięty!</div>";
            }
            if (empty($_SESSION["shopping_cart"])) {
                unset($_SESSION["shopping_cart"]);
            }
        }
    }
}

if (isset($_POST["action"]) && $_POST["action"] == "change") {
    foreach ($_SESSION["shopping_cart"] as &$value) {
        if ($value["code"] === $_POST["code"]) {
            $value["quantity"] = $_POST["quantity"];
            break;
        }
    }
}
?>
<html>

<head>
    <link rel='stylesheet' href='style.css' type='text/css' media='' />
</head>

<body>
    <?php if (!empty($_SESSION["shopping_cart"])) {
        $cart_count = count(array_keys($_SESSION["shopping_cart"])); ?>
        <a style="float:right; font-size: 30px; margin-top: 10px;" href="cart.php">
            <img style="float:right; width: 85px; height: 75px;" src="cart.png" /> Ilość artykułów w koszyku:
            <span>
                <?php echo $cart_count; ?>
            </span></a>
        <?php
    } ?>

    <?php if (isset($_SESSION["shopping_cart"])) {
        $total_price = 0; ?>
        <table
            style="font-size: 20px; border: solid 2px white; background: rgba(94,94,94,.7); width: 80%;margin-left: auto; margin-right: auto;">
            <tr>
                <td style="border: solid 2px white;">Tytuł</td>
                <td></td>
                <td style="border: solid 2px white;">Ilość</td>
                <td style="border: solid 2px white;">Cena jednostkowa</td>
                <td style="border: solid 2px white;">Suma</td>
            </tr>
            <?php foreach ($_SESSION["shopping_cart"] as $product) { ?>
                <tr>
                    <td style="border: solid 2px white;"><?php echo $product[
                        "name"
                        ]; ?><br /></td>
                    <form method='post' action=''>
                        <input type='hidden' name='code' value="<?php echo $product["code"]; ?>" />
                        <input type='hidden' name='action' value="remove" />
                        <td><button style="border: none; background: none; color: white; font-size: 18px;" type='submit'>Usuń
                                przedmiot</button></td>
                    </form>
                    <td style="border: solid 2px white;">
                        <form method='post' action=''>
                            <input type='hidden' name='code' value="<?php echo $product["code"]; ?>" />
                            <input type='hidden' name='action' value="change" />
                            <select style="width: 50px;" name='quantity' onchange="this.form.submit()">
                                <option <?php if ($product["quantity"] == 1) {
                                    echo "selected";
                                } ?> value="1">1</option>
                                <option <?php if ($product["quantity"] == 2) {
                                    echo "selected";
                                } ?> value="2">2</option>
                                <option <?php if ($product["quantity"] == 3) {
                                    echo "selected";
                                } ?> value="3">3</option>
                                <option <?php if ($product["quantity"] == 4) {
                                    echo "selected";
                                } ?> value="4">4</option>
                                <option <?php if ($product["quantity"] == 5) {
                                    echo "selected";
                                } ?> value="5">5</option>
                            </select>
                        </form>
                    </td>
                    <td style="border: solid 2px white;">
                        <?php echo $product["price"] .
                            " zł"; ?>
                    </td>
                    <td style="border: solid 2px white;"><?php echo $product["price"] *
                        $product["quantity"] .
                        " zł"; ?></td>
                </tr>
                <?php $total_price += $product["price"] * $product["quantity"];
            } ?>
            <tr>
                <td colspan="5" align="right">
                    <strong>Suma: <?php echo $total_price . " zł"; ?></strong>
                </td>
            </tr>
        </table>
        <?php
    } else {
        echo "<h3>Koszyk jest pusty!</h3>";
    } ?>



    <div style="margin:10px 0px;">
        <?php echo $status; ?>
    </div>
    <form method="post">
        <input style="
  width: 140px; 
  text-align: center;   
  display: block;
  margin-right: auto;
  margin-left: auto;" type="submit" name="tak" value="Potwierdź">
    </form>

    <?php if (isset($_POST["tak"])) {
        include "./zatwierdz_faktura.php";
    } ?>
    <br /><br />
    <footer style="position: absolute;
  bottom: 10px; font-size: 25px;
  width: 100%; text-align: center;" id="footer"><a href="home_user.php">Powrót</a></footer>
</body>

</html>