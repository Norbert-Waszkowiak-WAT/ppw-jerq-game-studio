//how to create login form in php?
<?php
if(isset($_POST['login'])){
    session_start();
    $errmsg_arr = array();
    $errflag = false;
    // configuration
    $dbhost     = "localhost";
    $dbname     = "2c_pawlowski_ksiegarnia";
    $dbuser     = "root";
    $dbpass     = "";

    // database connection
    $conn = new PDO("mysql:host=$dbhost;dbname=$dbname",$dbuser,$dbpass);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    $conn->exec("SET CHARACTER SET utf8mb4");
    // new data

    $user = $_POST['your name of email input'];
    $password = $_POST['password'];

    if($user == '') {
        $errmsg_arr[] = 'You must enter your Username';
        $errflag = true;
    }
    if($password == '') {
        $errmsg_arr[] = 'You must enter your Password';
        $errflag = true;
    }

    // query
    $result = $conn->prepare("SELECT * FROM login WHERE username= :u AND password= :p");
    $result->bindParam(':u', $user);
    $result->bindParam(':p', $password);
    $result->execute();
    $rows = $result->fetch(PDO::FETCH_NUM);
    if($rows > 0) {
        $_SESSION['username'] = $user;
        header("location: ./pages/home.php");
    }
    else{
        $errmsg_arr[] = 'Username and Password are not found';
        $errflag = true;
    }

}
?>


<body>
<form action="" method="post" name="login">
<input type="text" name="username" placeholder="Enter a Username"/>
<input type="password" name="password" placeholder="***"/>
<input type="submit" name="login_submit" value="Login"/>
</form>
</body>


