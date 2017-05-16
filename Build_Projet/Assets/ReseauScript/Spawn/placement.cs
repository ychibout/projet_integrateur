﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class placement : NetworkBehaviour {

	public GameObject[] spawnPointsRed;
	public GameObject[] spawnPointsBlue;

	void Start() {

		spawnPointsBlue = GameObject.FindGameObjectsWithTag("SpawnBlue");
		spawnPointsRed = GameObject.FindGameObjectsWithTag("SpawnRed");

		if (!isServer)
			Cmdreplacement (GameObject.FindGameObjectWithTag ("Information").GetComponent<Information> ().team);
	} 

	void Update()
	{ 
		
	}

	[Command]
	public void Cmdreplacement (int team) {



		if (team == 1) {
			// ajoute dans le compte des joueurs par equipe
		/*	GameObject network = GameObject.Find("Network");
			network.GetComponent<menuReseau>().AjoutJoueur(team);*/
		
			// trouve posistion spawn
			GetComponent<Transform> ().position = spawnPointsBlue[Random.Range (0, spawnPointsBlue.Length)].transform.position;

			// met le joueur dans le bon sens
			transform.Rotate(0,90,0);

			// tags
			transform.tag = "Equipe1";
			Transform child = transform.GetChild (0);
			child.tag = "Equipe1";

			// couleur du nom du joueur
			Transform nomJoueurtmp = child.transform.GetChild (4);
			Transform nomJoueur = nomJoueurtmp.transform.GetChild (1);
			nomJoueur.gameObject.GetComponent<TextMesh> ().color = Color.blue;

			// couleur de l'icone minimap
			child.GetChild(7).GetComponent<SpriteRenderer>().color = Color.blue;
		}

		if (team == 2) {

			// ajoute dans le compte des joueurs par equipe
			/*GameObject network = GameObject.Find("Network");
			network.GetComponent<menuReseau>().AjoutJoueur(team);*/

			transform.Rotate(0,-90,0);
			GetComponent<Transform> ().position = spawnPointsRed[Random.Range (0, spawnPointsRed.Length)].transform.position;
			transform.tag = "Equipe2";
			Transform child = transform.GetChild (0);
			child.tag = "Equipe2";
			Transform nomJoueurtmp = child.transform.GetChild (4);
			Transform nomJoueur = nomJoueurtmp.transform.GetChild (1);
			nomJoueur.gameObject.GetComponent<TextMesh> ().color = Color.red;

			// couleur de l'icone minimap
			child.GetChild(7).GetComponent<SpriteRenderer>().color = Color.red;
		}
		Rpcclientremplacement (team);
	}

	[ClientRpc]
	public void Rpcclientremplacement (int team)
	{
		Debug.Log ("ok");


		if (team == 1) {
			// ajoute dans le compte des joueurs par equipe
			/*GameObject network = GameObject.Find("Network");
			network.GetComponent<menuReseau>().AjoutJoueur(team);*/

			transform.Rotate(0,90,0);
			GetComponent<Transform> ().position = spawnPointsBlue[Random.Range (0, spawnPointsBlue.Length)].transform.position;
		
			// tag du joueur selon equipe
			transform.tag = "Equipe1";
			Transform child = transform.GetChild (0);
			child.tag = "Equipe1";
			// couleur du nom de joueur
			Transform nomJoueurtmp = child.transform.GetChild (4);
			Transform nomJoueur = nomJoueurtmp.transform.GetChild (1);
			nomJoueur.gameObject.GetComponent<TextMesh> ().color = Color.blue;

			// couleur de l'icone minimap
			child.GetChild(7).GetComponent<SpriteRenderer>().color = Color.blue;
		}

		if (team == 2) {
			// ajoute dans le compte des joueurs par equipe
			/*GameObject network = GameObject.Find("Network");
			network.GetComponent<menuReseau>().AjoutJoueur(team);*/

			transform.Rotate(0,-90,0);
			GetComponent<Transform> ().position = spawnPointsRed[Random.Range (0, spawnPointsRed.Length)].transform.position;
			// tag du joueur selon equipe
			transform.tag = "Equipe2";
			Transform child = transform.GetChild (0);
			child.tag = "Equipe2";
			// couleur du nom de joueur
			Transform nomJoueurtmp = child.transform.GetChild (4);
			Transform nomJoueur = nomJoueurtmp.transform.GetChild (1);
			nomJoueur.gameObject.GetComponent<TextMesh> ().color = Color.red;

			// couleur de l'icone minimap
			child.GetChild(7).GetComponent<SpriteRenderer>().color = Color.red;
		}
	}
}
