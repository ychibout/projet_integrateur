/* 
 * A changer:
 *  - Fonction outOfMap()
 * 
 * A utiliser:
 *  - Ajouter en tant que deuxième enfant de messOut un texte (qui sera modifié)
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class limitsMap : MonoBehaviour {

	public GameObject messOut; // message pour dire au joueur de faire demi tour
	public int playerOut = 0; // verifier si le joueur est dehors
	public GameObject arrow; // fleche indique direction pour retourner dans la map
	public Transform target; // cible vers laquelle la fleche regarde

	public IEnumerator co;

	/*********************************************************************
	************************* START & UPDATE *****************************
	*********************************************************************/

	void Start () {
		messOut.SetActive (false);
		arrow.SetActive (false);

	}

	void Update(){
		arrow.transform.LookAt (target); // on dirige fleche vers centre de la map
	}


	/*********************************************************************
	************************** FONCTIONS *********************************
	*********************************************************************/

	// Lorsque le joueur sort de la map
	private void OnTriggerExit(Collider collision){

		// On verifie qu'il s'agit du joueur
		if (collision.tag == "Equipe1" || collision.tag == "Equipe2") {
			
			messOut.SetActive (true); // affiche message
			arrow.SetActive(true); // affiche flèche
			// routine pour afficher message quitte partie au bout de 5 sec
			co = outOfMap();
			StartCoroutine(co); 
			playerOut = -1;
		}
	}

	// Lorsque le joueur revient dans la map
	private void OnTriggerEnter(Collider collision){
		if (collision.tag == "Equipe1" || collision.tag == "Equipe2") {
			messOut.SetActive (false);
			arrow.SetActive (false);
			playerOut = 0;
		}
	}

	// Recharge la scene au bout de 5 secondes
	private IEnumerator outOfMap(){
		// attend 5 secondes
		//yield return new WaitForSeconds(5);

		for (int i = 5; i >= 0; i--) {
			messOut.transform.GetChild (1).GetComponent<Text> ().text = "00:0"+i;
			yield return new WaitForSeconds (1);
			if (playerOut == 0)
				i = -1;
			Debug.Log (i);
		}

		// si le joueur est toujours dehors
		if(playerOut == -1){
			// recharge la scene
			Application.LoadLevel("ChoixEquipe");
		}
	}
}
