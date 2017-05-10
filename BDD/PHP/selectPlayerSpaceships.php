<?php

ini_set('error_reporting', E_ALL | E_STRICT); ini_set('display_errors', 1); ini_set('html_errors', 1);


  $server_name = "localhost";
  $server_login = "root";
  $server_password = "";
  $dbName = "pi_new";

  //connection
  $connection = new mysqli($server_name, $server_login, $server_password, $dbName);
  if (!$connection) {
    echo "error";
    die("Connection failed. ".mysqli_connect_error());
  }

  

  if(isset($_POST['cat']) && $_POST['cat'] == "selectSpaceships") {
	  if (isset($_POST['joueur_id'])) {
		  $joueur_id = intval($_POST['joueur_id']);
		 
          $command = "SELECT (vaisseau_pv + SUM(amelioration_pv)), (vaisseau_degat + SUM(amelioration_degat)), (vaisseau_vitesse + SUM(amelioration_vitesse)), vaisseau_nom, vaisseau_type, vaisseau_id, joueur_nom FROM joueur_vaisseau_amelioration INNER JOIN vaisseau on vaisseau_id = jva_vaisseau_id INNER JOIN joueur on joueur_id = jva_joueur_id INNER JOIN amelioration on amelioration_id = jva_amelioration_id WHERE joueur_id = ".$joueur_id." GROUP BY vaisseau_pv, vaisseau_degat, vaisseau_vitesse, vaisseau_nom, vaisseau_type,vaisseau_id, joueur_nom;";
					
          $result = mysqli_query($connection, $command);
          $rows = array();

          while($r = mysqli_fetch_assoc($result)) {
              $rows[] = $r;
          }

          echo json_encode($rows);
	}
	else echo "error";
  }



  if(isset($_POST['cat']) && $_POST['cat'] == "listeVaisseaux") {

          $command = "SELECT * FROM vaisseau;";
          $result = mysqli_query($connection, $command);
          $rows = array();

          while($r = mysqli_fetch_assoc($result)) {
              $rows[] = $r;
          }

          echo json_encode($rows);
  }

  if(isset($_POST['cat']) && $_POST['cat'] == "listeArmes") {

          $command = "SELECT * FROM arme;";
          $result = mysqli_query($connection, $command);
          $rows = array();

          while($r = mysqli_fetch_assoc($result)) {
              $rows[] = $r;
          }

          echo json_encode($rows);
  }


  ?>
