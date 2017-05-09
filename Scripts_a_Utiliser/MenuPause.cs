using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour {

	private bool isPaused = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
