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
	private GameObject player;

	// Life variable
	private float life;

	// Use this for initialization
	void Start () {
		life = LIFE;
		end_panel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	// Fonction TakeDamage
	public void TakeDamage(float damage) {
		if (!isServer) 
		{
			return;
		}
		if (life <= 0.0f)
			return;
		life -= damage;
		if (life <= 0.0f) {
			RpcOnDestroyed(transform.parent.name);
		}
	}


	// Fonction affichant le message de victoire ou de défaite en fonction du joueur
	// Pour modifier le texte du canvas, on prend son premier enfant (panel) puis l'enfant de celui-ci (text)
	[ClientRpc]
	public void RpcOnDestroyed(string croiseur) {
		// Recherche du vaisseau du joueur
		player = GameObject.FindGameObjectWithTag("Information");
		// Arret jeu
		Time.timeScale = 0.0f;
		// Affichage *à faire quand le panel sera ajoute*

		end_panel.SetActive(true);
		if(croiseur.Equals("croiseurBlue")){
			if(player.GetComponent<Information>().team == 1){
				end_panel.SetActive (true);
				end_panel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "YOU LOSE!";
			}
			else{
				end_panel.SetActive (true);
				end_panel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "YOU WIN!";
			}
		} 
		else {
			if(player.GetComponent<Information>().team == 1){
					end_panel.SetActive (true);
					end_panel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "YOU WIN!";
				}
			else{
					end_panel.SetActive (true);
					end_panel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "YOU LOSE!";
			}
		}
	}
}
