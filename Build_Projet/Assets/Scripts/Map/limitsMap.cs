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

		GameObject vaisseau = collision.gameObject;
		if (collision.tag != "Bullet") {
			GameObject joueur = vaisseau.transform.parent.gameObject;

			// on exlut les IA et les croiseurs
			if (collision.name != "AI_Equipe1_model" && collision.name != "AI_Equipe2_model" && joueur.name != "croiseurBlue" && joueur.name != "croiseurRed") {
				// On verifie qu'il s'agit du joueur
				if (collision.tag == "Equipe1" || collision.tag == "Equipe2") {
					
					// verifie si joueur local et affiche message
					if (joueur.GetComponent<PlayerInput> ().estLocalPlayer ())
						messOut.SetActive (true); // affiche message
				
					// indique joueur out
					joueur.GetComponent<PlayerInput> ().playerOut = -1;

					// recupere fleche
					arrow = vaisseau.transform.Find ("Arrow").gameObject;
					arrow.SetActive (true); // affiche flèche

					// routine pour afficher message quitte partie au bout de 5 sec
					StartCoroutine (outOfMap (joueur)); 

				}
			}
		}
	}

	// Lorsque le joueur revient dans la map
	private void OnTriggerEnter(Collider collision){

		GameObject vaisseau = collision.gameObject;
		if (collision.tag != "Bullet") {
			GameObject joueur = vaisseau.transform.parent.gameObject;
			// exlusion IA
			if (collision.name != "AI_Equipe1_model" && collision.name != "AI_Equipe2_model" && joueur.name != "croiseurBlue" && joueur.name != "croiseurRed") {
				// verifie si joueur
				if (collision.tag == "Equipe1" || collision.tag == "Equipe2") {
					joueur.GetComponent<PlayerInput> ().playerOut = 0;

					// verifie s'il s'agit du joueur local 
					if (joueur.GetComponent<PlayerInput> ().estLocalPlayer ())
						messOut.SetActive (false);
					
					// recupere fleche
					arrow = vaisseau.transform.Find ("Arrow").gameObject;
					arrow.SetActive (false);
				}
			}
		}
	}

	// Recharge la scene au bout de 7 secondes
	private IEnumerator outOfMap(GameObject joueur){
		// attend 5 secondes
		for (int i = 5; i >= 0; i--) {
			messOut.transform.GetChild (1).GetComponent<Text> ().text = "00:0"+i;
			yield return new WaitForSeconds (1);
			if (joueur.GetComponent<PlayerInput>().playerOut == 0)
				i = -1;
		}

		// si le joueur est toujours dehors
		if(joueur.GetComponent<PlayerInput>().playerOut == -1){
			// recharge la scene
			Application.LoadLevel("ChoixEquipe");
		}
	}

}
