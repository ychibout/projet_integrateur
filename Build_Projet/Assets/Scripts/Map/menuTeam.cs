/*
 * Elisa Kalbé
 * menuTeam.cs
 * 
 * Script pour le menu du choix de l'équipe
 * 
 * Dans scène ChoixEquipe
 * A mettre sur l'objet : EventSystem
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuTeam : MonoBehaviour {


	void Start(){
		Debug.Log ("lancement");
	}
	/*
	// nbr de membre dans chaque equipe
	int nbRouge;
	int nbBleu;

	public Button equipe1;
	public Button equipe2;

	void Update(){
	
		//Debug.Log ("ok dans test");

		// verifie le nb de joueurs par equipe
		nbRouge = menuReseau.s_nbRouge;
		nbBleu = menuReseau.s_nbBleu;
	 	//Debug.Log ("r " + nbRouge + "b " + nbBleu);
		if (nbRouge >= nbBleu + 1) {
			// désactive bouton equipe 2
			equipe1.interactable = true;
			equipe2.interactable = false;

		} else if (nbBleu >= nbRouge + 1) {
			// desactive bouton equipe 1
			equipe2.interactable = true;
			equipe1.interactable = false;
		} 

		// toutes les equipes sont actives
		else {
			equipe1.interactable = true;
			equipe2.interactable = true;
		}


	}*/

	// Clique sur bouton equipe 1
	public void OnEquipe1Click(){
		GameObject.FindGameObjectWithTag ("Information").GetComponent<Information>().team = 1; // numero de l'equipe
		SceneManager.LoadScene("Game"); // chargement map
	}

	// Clique sur bouton equipe 2
	public void OnEquipe2Click(){
		GameObject.FindGameObjectWithTag ("Information").GetComponent<Information>().team=  2; // numero de l'equipe
		SceneManager.LoadScene("Game"); // chargement map
	}

	public void ServeurClick(){
		GameObject.FindGameObjectWithTag ("Information").GetComponent<Information>().team=  0; // numero de l'equipe
		SceneManager.LoadScene("Game"); // chargement map
	}

	public void RetourClick(){
		SceneManager.LoadScene("Profil"); // chargement profil
	}
}
