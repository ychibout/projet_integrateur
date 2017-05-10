<?php
  $server_name = "localhost";
  $server_login = "root";
  $server_password = "";
  $dbName = "pi_new";

  //connection
  $connection = new mysqli($server_name, $server_login, $server_password, $dbName);
  if (!$connection) {
    echo "Connection failed";
    die("Connection failed. ".mysqli_connect_error());
  }


  //test si login existe pas déjà dans la base
  if(isset($_POST['cat']) && $_POST['cat'] == "testLogin") {

    if (isset($_POST['joueur_nom'])) {
      $joueur_nom = $_POST['joueur_nom'];

      $command = "SELECT * FROM joueur WHERE joueur_nom = '".$joueur_nom."';";
      $result = mysqli_query($connection, $command);
      $num_rows = mysqli_num_rows($result);

      if ($num_rows == 0)
        echo "success";
      else
        echo "error";
    }
    else echo "error";
  }


  //insertion nouveau joueur
  if(isset($_POST['cat']) && $_POST['cat'] == "insert") {
    if(isset($_POST['joueur_nom']) && isset($_POST['joueur_mdp'])) {
        $joueur_nom = $_POST['joueur_nom'];
        $joueur_mdp = $_POST['joueur_mdp'];
        $joueur_mdp = md5($joueur_mdp);

        $command = "INSERT INTO joueur (joueur_nom, joueur_mdp) VALUES ('".$joueur_nom."', '".$joueur_mdp."');";
        $result = mysqli_query($connection, $command);

        if (!result) {
          echo "error";
        }
      }
      else echo "error";
  }


  //Connection joueur
  if(isset($_POST['cat']) && $_POST['cat'] == "connection") {
      if(isset($_POST['joueur_nom']) && isset($_POST['joueur_mdp'])) {
          $joueur_nom =  $_POST['joueur_nom'];
          $joueur_mdp =  $_POST['joueur_mdp'];
          $joueur_mdp = md5($joueur_mdp);

          $command = "SELECT * FROM joueur WHERE joueur_nom = '" . $joueur_nom."' AND joueur_mdp = '".$joueur_mdp."' ";

          $result = mysqli_query($connection, $command);
          echo mysqli_num_rows($result);

      }
      else echo "error";
  }


  //Selection joueur
  if(isset($_POST['cat']) && $_POST['cat'] == "select") {
      if(isset($_POST['joueur_nom'])) {
          $joueur_nom =  $_POST['joueur_nom'];

          $command = "SELECT * FROM joueur WHERE joueur_nom = '".$joueur_nom."'";

          $result = mysqli_query($connection, $command);

          if (mysqli_num_rows($result) > 0 ) {
              while ($row = mysqli_fetch_assoc($result)) {
                  echo $row['joueur_id']. "|".$row['joueur_nom']. "|".$row['joueur_mdp']."|".$row['joueur_niveau']. "|".$row['joueur_xp']. ";";
              }
          }
          else echo "error";

      }
      else echo "error";
  }


  ?>
