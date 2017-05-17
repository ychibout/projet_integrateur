using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class placement : NetworkBehaviour {

	public GameObject[] spawnPointsRed;
	public GameObject[] spawnPointsBlue;
	//public GameObject mess_chang; // message en cas de choix automatique d'équipe

	void Start() {

		spawnPointsBlue = GameObject.FindGameObjectsWithTag("SpawnBlue");
		spawnPointsRed = GameObject.FindGameObjectsWithTag("SpawnRed");

		if (!isServer)
			Cmdreplacement (GameObject.FindGameObjectWithTag ("Information").GetComponent<Information> ().team);
	} 


	[Command]
	public void Cmdreplacement (int team) {


		if (team == 1) {
		
			// trouve posistion spawn
			GetComponent<Transform> ().position = spawnPointsBlue[Random.Range (0, spawnPointsBlue.Length)].transform.position;

			// met le joueur dans le bon sens
			transform.Rotate(0,90,0);

			// tags
			transform.tag = "Equipe1";
			Transform child = transform.GetChild (0);
			child.tag = "Equipe1";

			// couleur du nom du joueur
			Transform nomJoueurtmp = child.transform.GetChild (4);
			Transform nomJoueur = nomJoueurtmp.transform.GetChild (1);
			nomJoueur.gameObject.GetComponent<TextMesh> ().color = Color.blue;

			// couleur de l'icone minimap
			child.GetChild(7).GetComponent<SpriteRenderer>().color = Color.blue;


			// verifie si le nbr de joueur est proportionné
			/*GameObject.Find ("cptJoueur").GetComponent<compteJoueur> ().demarreCpt (); // cpt les joueurs
			// si il y a plus de bleu
			if (compteJoueur.nbrJoueurEquipe1 > compteJoueur.nbrJoueurEquipe2+1){
				// placement auto en rouge
				Cmdreplacement (2);
				// affichage message
				if (isLocalPlayer)
					StartCoroutine (afficheMess ()); 
			}*/

		}

		if (team == 2) {

			// trouve position spawn
			GetComponent<Transform> ().position = spawnPointsRed[Random.Range (0, spawnPointsRed.Length)].transform.position;

			// met le joueur dans le bon sens
			transform.Rotate(0,-90,0);

			// tags
			transform.tag = "Equipe2";
			Transform child = transform.GetChild (0);
			child.tag = "Equipe2";

			// couleur du nom du joueur
			Transform nomJoueurtmp = child.transform.GetChild (4);
			Transform nomJoueur = nomJoueurtmp.transform.GetChild (1);
			nomJoueur.gameObject.GetComponent<TextMesh> ().color = Color.red;

			// couleur de l'icone minimap
			child.GetChild(7).GetComponent<SpriteRenderer>().color = Color.red;


			// verifie si le nbr de joueur est proportionné
			/*GameObject.Find ("cptJoueur").GetComponent<compteJoueur> ().demarreCpt (); // cpt joueur
			// si il y a plus de rouge
			if (compteJoueur.nbrJoueurEquipe2 > compteJoueur.nbrJoueurEquipe1+1){
				// placement auto en bleu
				Cmdreplacement (1);
				// affiche message
				if (isLocalPlayer)
					StartCoroutine (afficheMess ()); 
			}*/
		}
		Rpcclientremplacement (team);
	}

	[ClientRpc]
	public void Rpcclientremplacement (int team)
	{
		if (team == 1) {

			// trouve posistion spawn
			GetComponent<Transform> ().position = spawnPointsBlue[Random.Range (0, spawnPointsBlue.Length)].transform.position;

			// met le joueur dans le bon sens
			transform.Rotate(0,90,0);

			// tags
			transform.tag = "Equipe1";
			Transform child = transform.GetChild (0);
			child.tag = "Equipe1";

			// couleur du nom du joueur
			Transform nomJoueurtmp = child.transform.GetChild (4);
			Transform nomJoueur = nomJoueurtmp.transform.GetChild (1);
			nomJoueur.gameObject.GetComponent<TextMesh> ().color = Color.blue;

			// couleur de l'icone minimap
			child.GetChild(7).GetComponent<SpriteRenderer>().color = Color.blue;


			// verifie si le nbr de joueur est proportionné
			/*GameObject.Find ("cptJoueur").GetComponent<compteJoueur> ().demarreCpt (); // cpt les joueurs
			// si il y a plus de bleu
			if (compteJoueur.nbrJoueurEquipe1 > compteJoueur.nbrJoueurEquipe2+1){
				// placement auto en rouge
				Cmdreplacement (2);
				// affichage message
				if (isLocalPlayer)
					StartCoroutine (afficheMess ()); 
			}*/

		}


		if (team == 2) {

			// trouve position spawn
			GetComponent<Transform> ().position = spawnPointsRed[Random.Range (0, spawnPointsRed.Length)].transform.position;

			// met le joueur dans le bon sens
			transform.Rotate(0,-90,0);

			// tags
			transform.tag = "Equipe2";
			Transform child = transform.GetChild (0);
			child.tag = "Equipe2";

			// couleur du nom du joueur
			Transform nomJoueurtmp = child.transform.GetChild (4);
			Transform nomJoueur = nomJoueurtmp.transform.GetChild (1);
			nomJoueur.gameObject.GetComponent<TextMesh> ().color = Color.red;

			// couleur de l'icone minimap
			child.GetChild(7).GetComponent<SpriteRenderer>().color = Color.red;

			// verifie si le nbr de joueur est proportionné
			/*GameObject.Find ("cptJoueur").GetComponent<compteJoueur> ().demarreCpt (); // cpt joueur
			Debug.Log("b : "+compteJoueur.nbrJoueurEquipe1+" r : "+compteJoueur.nbrJoueurEquipe2);
			// si il y a plus de rouge
			if (compteJoueur.nbrJoueurEquipe2 > compteJoueur.nbrJoueurEquipe1+1){
				Debug.Log ("okpourrouge");
				// placement auto en bleu
				Cmdreplacement (1);
				// affiche message
				if (isLocalPlayer)
					StartCoroutine (afficheMess ()); 
			}*/
		}
	}

	/*private IEnumerator afficheMess(){
		mess_chang.SetActive (true);
		yield return new WaitForSeconds (1);
		mess_chang.SetActive (false);
	}*/
}
