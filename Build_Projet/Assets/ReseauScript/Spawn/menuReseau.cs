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
	}

	// Update is called once per frame
	void Update () {
		if (!connecte) {
			if (Input.GetKeyDown (KeyCode.S)) {	//Lance un serveur
				nm.StartServer();
				connecte = true;
				Debug.Log("Lance");
			}

			if (Input.GetKeyDown (KeyCode.R)) {	//Lance un client rouge
				GameObject.FindGameObjectWithTag("equipe").GetComponent<team_choice>().team_v = 2;

				// tag
				GameObject objtmp = nm.playerPrefab;
				//objtmp.tag = "Equipe2";
				Transform objtmpChild = objtmp.transform.GetChild (0);
				//objtmpChild.tag = "Equipe2";
				/*
				Transform nomJoueurtmp = objtmpChild.transform.GetChild (4);
				Transform nomJoueur = nomJoueurtmp.transform.GetChild (1);
				nomJoueur.gameObject.GetComponent<TextMesh> ().color = Color.red;
*/

				nm.StartClient();
				connecte = true;
				Debug.Log ("LanceR");
			}

			if (Input.GetKeyDown (KeyCode.B)) {	//Lance un client bleu
				GameObject.FindGameObjectWithTag("equipe").GetComponent<team_choice>().team_v = 1;

				// tag
				GameObject objtmp = nm.playerPrefab;
				//objtmp.tag = "Equipe1";
				Transform objtmpChild = objtmp.transform.GetChild (0);
				//objtmpChild.tag = "Equipe1";
				/*
				Transform nomJoueurtmp = objtmpChild.transform.GetChild (4);
				Transform nomJoueur = nomJoueurtmp.transform.GetChild (1);
				nomJoueur.gameObject.GetComponent<TextMesh> ().color = Color.blue;
*/

				nm.StartClient();
				connecte = true;
				Debug.Log ("LanceB");
			}
		}
	}

	void OnGUI()
	{
		if (!connecte)
		{
			GUI.Label(new Rect(2, 10, 150, 100), "Press S for server");        
			GUI.Label(new Rect(2, 30, 150, 100), "Press B for client bleu");
			GUI.Label(new Rect(2, 50, 150, 100), "Press R for client rouge");
		}
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
