/* 
 * Elisa Kalbé
 * limitsMap.cs
 * 
 * Script pour indiquer au joueur de faire demi tour lorsqu'il
 * sort de la map. Un flèche lui indique la direction à prendre.
 * Le joueur perd s'il ne revient pas au bout de 5 secondes.
 * 
 * Dans scène Game
 * A mettre sur l'objet : Limits
 * 
 * Paramètres
 * 	- MessOut : Mess_OutOfMap
 * 		message à afficher pour indiquer au joueur de retourner dans la map
 * 	- PlayerOut : 0
 * 		0 si le joueur est dans la map, 1 si il est en dehors
 * 	- Arrow : Arrow
 * 		flèche qui indique la direction à prendre pour retourner dans la map
 *	- Target : target
 *		cible visée pas la flèche (centre de la map)
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class limitsMap : MonoBehaviour {

	public GameObject messOut; // message pour dire au joueur de faire demi tour
	public int playerOut = 0; // verifier si le joueur est dehors
	public Transform target; // cible vers laquelle la fleche regarde
	GameObject arrow; // fleche

	/*********************************************************************
	************************* START & UPDATE *****************************
	*********************************************************************/

	void Start () {
		messOut.SetActive (false);
	}

	void Update(){
		if(arrow != null)
			arrow.transform.LookAt (target); // on dirige fleche vers centre de la map
	}


	/*********************************************************************
	************************** FONCTIONS *********************************
	*********************************************************************/

	// Lorsque le joueur sort de la map
	private void OnTriggerExit(Collider collision){

		// on exlut les IA
		if (collision.name != "AI_Equipe1_model" && collision.name != "AI_Equipe2_model") {
			// On verifie qu'il s'agit du joueur
			if (collision.tag == "Equipe1" || collision.tag == "Equipe2") {

				// recupere fleche
				GameObject coll = GameObject.Find (collision.name);
				arrow = coll.transform.Find ("Arrow").gameObject;
				messOut.SetActive (true); // affiche message
				arrow.SetActive (true); // affiche flèche
				// routine pour afficher message quitte partie au bout de 5 sec
				StartCoroutine (outOfMap ()); 
				playerOut = -1;
			}
		}
	}

	// Lorsque le joueur revient dans la map
	private void OnTriggerEnter(Collider collision){

		// exlusion IA
		if (collision.name != "AI_Equipe1_model" && collision.name != "AI_Equipe2_model") {
			// verifie si joueur
			if (collision.tag == "Equipe1" || collision.tag == "Equipe2") {
				// recupere fleche
				GameObject coll = GameObject.Find (collision.name);
				arrow = coll.transform.Find ("Arrow").gameObject;
				messOut.SetActive (false);
				arrow.SetActive (false);
				playerOut = 0;
			}
		}
	}

	// Recharge la scene au bout de 7 secondes
	private IEnumerator outOfMap(){
		// attend 5 secondes
		yield return new WaitForSeconds(7);

		// si le joueur est toujours dehors
		if(playerOut == -1){
			// recharge la scene
			Application.LoadLevel("ChoixEquipe");
		}
	}

}
