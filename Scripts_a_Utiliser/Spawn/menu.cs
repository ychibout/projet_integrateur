using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class menu : NetworkBehaviour {

	public NetworkManager nm;
	public GameObject t;

	public GameObject Speed;
	public GameObject Tank;

	private List<GameObject> prefabs;


	bool connecte = false;

	void Start() {
		nm = GetComponent<NetworkManager> ();
		prefabs = nm.spawnPrefabs;
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

				/*if (player.NomVaisseau.Equals ("Speed")) {
					nm.playerPrefab = prefabs.
				}
				else
					nm.playerPrefab = prefabs.Find(Tank);*/
				
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
}
