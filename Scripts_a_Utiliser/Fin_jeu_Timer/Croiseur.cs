/*
 * Script gérant la fin d'une partie
 * Pour l'utiliser, rajouter un canvas end_game contenant un panel qui contient un texte (voir code)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Croiseur : NetworkBehaviour {
	// Global variables
	public float LIFE = 100.0f;

	// EndGame Panel
	public GameObject end_panel;

	// Player
	public GameObject player;

	// Life variable
	private float life;

	private float CountDownTest = 2.0f;

	// Use this for initialization
	void Start () {
		life = LIFE;
		end_panel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		// Fonction de test *à enlever par la suite*
		// Destruction du croiseur utilisant ce script au bout d'un certain temps
		if (CountDownTest <= 0.0f)
			gameObject.GetComponent<Croiseur> ().TakeDamage (20.0f);
		else
			CountDownTest -= 0.1f * Time.deltaTime;
	}

	// Fonction TakeDamage
	public void TakeDamage(float damage) {
		if (life <= 0.0f)
			return;
		life -= damage;
		if (life <= 0.0f) {
			RpcOnDestroyed();
		} else {

		}
	}


	// Fonction affichant le message de victoire ou de défaite en fonction du joueur
	// Pour modifier le texte du canvas, on prend son premier enfant (panel) puis l'enfant de celui-ci (text)
	[ClientRpc]
	public void RpcOnDestroyed() {
		// Recherche du vaisseau du joueur
		player = GameObject.Find ("Player");
		// Arret jeu
		Time.timeScale = 0.0f;
		// Affichage
		end_panel.SetActive(true);
		if(player.tag != gameObject.tag) {
			end_panel.SetActive (true);
			end_panel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "YOU WIN!";
		} else {
			end_panel.SetActive (true);
			end_panel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "YOU LOSE!";
		}
	}
}
