using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MenuPause : NetworkBehaviour {

	private bool isPaused = false;

	public DisconnectClient nm ;
	public GameObject prefabs; 

	// Use this for initialization
	void Start () {
		nm = GameObject.FindGameObjectWithTag ("Network").GetComponent<DisconnectClient>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
				
		if (Input.GetKeyDown (KeyCode.Escape))
			isPaused = !isPaused;
		}

	void OnGUI(){
		if (isPaused) {
			if (GUI.Button (new Rect (Screen.width / 2 - 30, Screen.height / 2 - 20, 80, 40), "Continuer")) {
				
				isPaused = false;
			}

			if (GUI.Button (new Rect (Screen.width / 2 - 30, Screen.height / 2 + 40, 80, 40), "Quitter")) {
				Application.Quit ();
			}
		}
	}

}
