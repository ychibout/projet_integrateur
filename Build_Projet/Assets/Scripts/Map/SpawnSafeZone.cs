/* 
 * Elisa Kalbé
 * SpawnSafeZone.cs
 * 
 * scrit qui etabli la zone safe du spawn
 *  - balles des joueurs adverses ne traversent pas la zone
 *  - joueurs adverses ne peuvent traverser la zone
 *  - joueurs de l'équipe ne peuvent tirer à l'interieur de la zone
 *  - une fois sortis, les joueurs de l'équipe peuvent revenir dans la zone
 * 
 * Paramètre :
 * 		- equipe : 1 sur l'objet SafeZoneBlue, 2 sur l'objet SafeZoneRed
 * 			indique l'équipe à laquelle correspond la safezone
 * 
 * Dans scene Game
 * A mettre sur les objets : SafeZoneBlue, SafeZoneRed
 * 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSafeZone : MonoBehaviour {

	public int equipe; // equipe associée a la safezone


	// Lorsque le joueur sort de la zone
	private void OnTriggerExit(Collider collision){
		/*
		if (collision.tag == "Equipe1" && equipe == 2) {
			GameObject player = collision.transform.parent.gameObject;
			player.transform.Rotate (0, 180, 0);
			player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z + 5);
		}
	
		else if(collision.tag == "Equipe2" && equipe ==1){
			GameObject player = collision.transform.parent.gameObject;
			player.transform.Rotate (0, 180, 0);
			player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z + 5);


		}*/
		// On verife qu'il s'agit d'une balle
		if (collision.tag == "Bullet") {
			// on detruit la balle lorsqu'elle sort de la zone safe
			Destroy(collision.gameObject);
		}
	}

	// Lorsqu'un joueur vient dans la zone
	// Lorsqu'un joueur tire en direction de la zone
	private void OnTriggerEnter(Collider collision){

		// joueur ne peut rentrer que si c'est la safezone de son équipe
		if (collision.tag == "Equipe1" && equipe == 2) {
			GameObject player = collision.transform.parent.gameObject;
			player.transform.Rotate (0, 90, 0);

		}

		else if(collision.tag == "Equipe2" && equipe ==1){
			GameObject player = collision.transform.parent.gameObject;
			player.transform.Rotate (0, 90, 0);


		}
		// On verife qu'il s'agit d'une balle
		if (collision.tag == "Bullet") {
			// on detruit la balle lorsqu'elle entre dans la zone safe
			Destroy(collision.gameObject);
		}
	}

}


/* 
 * Elisa Kalbé
 * SpawnSafeZone.cs
 * 
 * scrit qui etabli la zone safe du spawn
 *  - balles des joueurs adverses ne traversent pas la zone
 *  - joueurs adverses ne peuvent traverser la zone
 *  - joueurs de l'équipe ne peuvent tirer à l'interieur de la zone
 *  - une fois sortis, les joueurs de l'équipe peuvent revenir dans la zone
 * 
 * Paramètre :
 * 		- equipe : 1 sur l'objet SafeZoneBlue, 2 sur l'objet SafeZoneRed
 * 			indique l'équipe à laquelle correspond la safezone
 * 
 * Dans scene Game
 * A mettre sur les objets : SafeZoneBlue, SafeZoneRed
 * 
*/
/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnSafeZone :  NetworkBehaviour {

	public int equipe; // equipe associée a la safezone

	// appellé au lancement du jeu, permet de sécuriser zone de spawn
	public void initialise() {

		// recupere modele du vaisseau
		GameObject information = GameObject.Find("Vaisseau");
		string vaisseau = information.GetComponent<Information> ().model;

		// recupere joueur selon type de vaisseau
		GameObject[] player1 = GameObject.FindGameObjectsWithTag("Equipe1");
		GameObject[] player2 = GameObject.FindGameObjectsWithTag ("Equipe2");


		// ignore collisions pour equipe 1
		if(equipe == 1)
			ignoreCollision(player1);
		else if(equipe == 2)
			ignoreCollision (player2);
	}

	// ignore collisions avec membre de l'équipe
	private void ignoreCollision(GameObject[] play){
		foreach (GameObject player in play) {

			string equipePlayer = player.tag; // equipe du joueur
			Transform child = player.transform.GetChild (0);

			// seul les joueurs de l'équipe peuvent passer à travers la safezone
			Collider[] colliders = this.GetComponents<Collider> ();
			if (colliders != null) {
				foreach (Collider collider in colliders) {
					// collider de la safe zone ignore les joueurs de l'équipe
					Physics.IgnoreCollision (child.GetComponent<Collider> (), collider);
				}
			}

		}
	}

	// Lorsque le joueur sort de la zone
	private void OnTriggerExit(Collider collision){

		// Attendre la modif de code de Guillaume
		// On verife qu'il s'agit d'une balle
		if (collision.tag == "Bullet") {
			// on detruit la balle lorsqu'elle sort de la zone safe
			Destroy(collision.gameObject);
			Debug.Log ("Balle sort");
		}
	}

	// Lorsqu'un joueur vient dans la zone
	// Lorsqu'un joueur tire en direction de la zone
	private void OnTriggerEnter(Collider collision){

		// Attendre la modif de code de Guillaume
		// On verife qu'il s'agit d'une balle
		if (collision.tag == "Bullet") {
			// on detruit la balle lorsqu'elle entre dans la zone safe
			Destroy(collision.gameObject);
			Debug.Log ("Balle entre");
		}
	}
}
*/