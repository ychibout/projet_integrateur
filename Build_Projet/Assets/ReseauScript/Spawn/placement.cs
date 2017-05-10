using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class placement : NetworkBehaviour {

	public GameObject[] spawnPointsRed;
	public GameObject[] spawnPointsBlue;

	void Start() {

		// pour lancer safeZone
		GameObject safeZoneBlue = GameObject.Find("SafeZoneBlue");
		GameObject safeZoneRed = GameObject.Find("SafeZoneRed");
		// lance
		safeZoneRed.GetComponent<SpawnSafeZone>().initialise();
		safeZoneBlue.GetComponent<SpawnSafeZone>().initialise();


		spawnPointsBlue = GameObject.FindGameObjectsWithTag("SpawnBlue");
		spawnPointsRed = GameObject.FindGameObjectsWithTag("SpawnRed");

		if (!isServer)
			Cmdreplacement (GameObject.FindGameObjectWithTag ("equipe").GetComponent<team_choice> ().team_v);
	} 

	void Update()
	{ 
		
	}

	[Command]
	public void Cmdreplacement (int team) {



		if (team == 1) {
			transform.Rotate(0,90,0);
			GetComponent<Transform> ().position = spawnPointsBlue[Random.Range (0, spawnPointsBlue.Length)].transform.position;
			transform.tag = "Equipe1";
			Transform child = transform.GetChild (0);
			child.tag = "Equipe1";
			Transform nomJoueurtmp = child.transform.GetChild (4);
			Transform nomJoueur = nomJoueurtmp.transform.GetChild (1);
			nomJoueur.gameObject.GetComponent<TextMesh> ().color = Color.blue;
		}

		if (team == 2) {
			transform.Rotate(0,-90,0);
			GetComponent<Transform> ().position = spawnPointsRed[Random.Range (0, spawnPointsRed.Length)].transform.position;
			transform.tag = "Equipe2";
			Transform child = transform.GetChild (0);
			child.tag = "Equipe2";
			Transform nomJoueurtmp = child.transform.GetChild (4);
			Transform nomJoueur = nomJoueurtmp.transform.GetChild (1);
			nomJoueur.gameObject.GetComponent<TextMesh> ().color = Color.red;
		}
		Rpcclientremplacement (team);
	}

	[ClientRpc]
	public void Rpcclientremplacement (int team)
	{

		if (team == 1) {
			transform.Rotate(0,90,0);
			GetComponent<Transform> ().position = spawnPointsBlue[Random.Range (0, spawnPointsBlue.Length)].transform.position;
			transform.tag = "Equipe1";
			transform.GetChild(0).tag = "Equipe1";
		}

		if (team == 2) {
			transform.Rotate(0,-90,0);
			GetComponent<Transform> ().position = spawnPointsRed[Random.Range (0, spawnPointsRed.Length)].transform.position;
			transform.tag = "Equipe2";
			transform.GetChild(0).tag = "Equipe2";
		}
	}
}
