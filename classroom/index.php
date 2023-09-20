<html>

<head>
  <link rel="stylesheet" type="text/css" href="style.css">
  <link rel="icon" type="image/x-icon" href="./favicon.png">
  <title>2C Pawłowski Księgarnia</title>
</head>

<body>
  <div class="form-wrapper" style="height: 450px;">

    <form action="" method="post">
      <h3 style="text-align:center">Zaloguj się do konta</h3>

      <div class="form-item">
        <input type="text" name="login" required="required" placeholder="LOGIN" autofocus required></input>
      </div>

      <div class="form-item">
        <input type="password" name="haslo" required="required" placeholder="HASŁO" required></input>
      </div>

      <div class="button-panel">
        <input type="submit" class="button" title="Zaloguj" name="sub" value="Login"></input>
      </div>
    </form>
    <?php
    include "dbcon.php";

    if (isset($_POST["sub"])) {
      session_start();
      $login = $_POST["login"];
      $_SESSION["login"] = $login;
      $haslo = $_POST["haslo"];
      $haslo = md5($haslo);
      $login = md5($login);
      $zapytanie = mysqli_query(
        $con,
        "select * from klienci where login_md='$login'and haslo='$haslo'"
      );
      $numRows = mysqli_num_rows($zapytanie);
      $row = mysqli_fetch_array($zapytanie);
      if ($numRows == 1) {
        if ($row["id_klienta"] == "1") {
          header("location:home.php");
          exit();
        } else {
          header("location:home_user.php");
          exit();
        }
      } else {
        echo "Nieudane logowanie";
      }
    }
    if (isset($_GET["sub"])) {

      session_start();
      $login = $_GET["login"];
      $_SESSION["login"] = $login;
      $haslo = $_GET["haslo"];
      $haslo = md5($haslo);
      $imie = $_GET["imie"];
      $login_md = md5($login);
      $zapytanie = "INSERT INTO klienci SET id_klienta='', imie='$imie', login ='$login', login_md = '$login_md', haslo='$haslo';";
      $wynik = mysqli_query($con, $zapytanie);
      header("location:home_user.php");
      exit();
    }


    ?>
    <form method="get" style="text-align: center;">
      Rejestracja nowego klienta</br>
      <input style="height: 30px; width: 250px; background: none;" type="text" name="imie"
        placeholder="Podaj imię"></br>
      <input style="height: 30px; width: 250px; background: none;" type="text" name="login"
        placeholder="Podaj login"></br>
      <input style="height: 30px; width: 250px; background: none;" type="password" name="haslo"
        placeholder="Podaj hasło"></br>
      <input type="submit" name="sub">
    </form>
  </div>

</body>

</html>