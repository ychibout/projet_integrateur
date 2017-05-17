using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class menuReseau : NetworkBehaviour {

	public NetworkManager nm;
	public GameObject t;

	private List<GameObject> prefabs;
	private Information player;

	bool connecte = false;


	void Start() {
		nm = GetComponent<NetworkManager> ();
		prefabs = nm.spawnPrefabs;
		player = GameObject.FindGameObjectWithTag ("Information").GetComponent<Information> ();
		ChangementVaisseau();
		if (player.team == 0) {
			nm.StartServer ();
			Debug.Log ("lancer");
		} else if (player.team == 1) {
			Debug.Log ("Client");
			nm.StartClient ();
		} else if (player.team == 2) {
			Debug.Log ("Client");
			nm.StartClient ();
		}
	}

	// Update is called once per frame
	void Update () {
		/*if (!connecte) {
			if (Input.GetKeyDown (KeyCode.S)) {	//Lance un serveur
				nm.StartServer();
				connecte = true;
				Debug.Log("Lance");
			}

			if (Input.GetKeyDown (KeyCode.R)) {	//Lance un client rouge
				GameObject.FindGameObjectWithTag("equipe").GetComponent<team_choice>().team_v = 2;

				nm.StartClient();
				connecte = true;
				Debug.Log ("LanceR");
			}

			if (Input.GetKeyDown (KeyCode.B)) {	//Lance un client bleu
				GameObject.FindGameObjectWithTag("equipe").GetComponent<team_choice>().team_v = 1;

				nm.StartClient();
				connecte = true;
				Debug.Log ("LanceB");
			}
		}*/
	}

	void ChangementVaisseau()
	{
		if (player.model.Equals ("Speed")) {
			nm.playerPrefab = prefabs [0];
			Debug.Log ("Speed");
			return;
		} else if (player.model.Equals ("Tank")) {
			nm.playerPrefab = prefabs [1];
			Debug.Log ("Tank");
			return;
		} else if (player.model.Equals ("Damage")) {
			nm.playerPrefab = prefabs [2];
			Debug.Log ("damage");
			return;
		}
		Debug.Log ("ici?");
	}
}
