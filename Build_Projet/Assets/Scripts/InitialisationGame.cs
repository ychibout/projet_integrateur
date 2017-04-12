/* 
 * Nathan Urbain
 * Elisa Kalbé	
 * spawn.cs
 * 
 * Script pour initialiser le jeu.
 * 	- recupere info joueur
 * 	- crée le vaisseau
 *  - défini tag du joueur en fonction de l'équipe choisie.
 * 	- spawn du joueur dans la zone de son equipe
 * 
 * Parametres :
 * 	Different points de spawn par equipe.
 * 		- SpawnBlue1 : SpawnBlue1
 * 		- SpawnBlue2 : SpawnBlue2
 * 		- SpawnBlue3 : SpawnBlue3
 * 		- SpawnRed1 : SpawnRed1	
 * 		- SpawnRed2 : SpawnRed2
 * 		- SpawnRed3 : SpawnRed3
 * 
 * 
 * */
 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InitialisationGame : MonoBehaviour {

	private Information player;
	private int team;
	private GameObject MainCamera;

	// point de spawn equipe bleue
	public GameObject spawnBlue1; 
	public GameObject spawnBlue2;
	public GameObject spawnBlue3;

	// point de spawn equipe rouge
	public GameObject spawnRed1;
	public GameObject spawnRed2;
	public GameObject spawnRed3;

	// Use this for initialization
	void Start () {

		MainCamera = GameObject.FindGameObjectWithTag ("MainCamera");

		//Recup info player 
		player = GameObject.FindGameObjectWithTag("Information").GetComponent<Information>();

		//Creation Vaisseau depuis dossier ressources 
		GameObject joueur = Instantiate (Resources.Load(player.model)) as GameObject;
		joueur.name = "Player";
		joueur.transform.position = new Vector3(8.47f , 0f, -10f);
		joueur.GetComponent<PlayerControl>().bullet = GameObject.FindGameObjectWithTag ("Bullet");

		setTag(joueur); // defini tag du joueur
		spawn (team, joueur); // spawn du joueur
	}

		
	void spawn(int team, GameObject joueur){

		// verifie equipe
		if (team == 1) {
			joueur.transform.position = spawnBlue1.transform.position;
			// on rotate pour qu'il soit vers la sortie
			joueur.transform.Rotate(0,90,0);
		} else {
			joueur.transform.position = spawnRed1.transform.position;
			// on rotate pour qu'il soit vers la sortie
			joueur.transform.Rotate(0,-90,0);
		}
	}


	void setTag(GameObject joueur){

		// equipe choisie
		team = player.team; 


		// on definit le tag correspondant et la couleur du nom
		if (team == 1) {
			joueur.tag = "Equipe1";
			// Couleur de l'outline en rouge
			// joueur.transform.GetChild (1).gameObject.GetComponent<TextMesh> ().color = Color.red;
		} else {
			joueur.tag = "Equipe2";
			// Couleur de l'outline en bleu
			// joueur.transform.GetChild (1).gameObject.GetComponent<TextMesh> ().color = Color.blue;
		}
	}
}
