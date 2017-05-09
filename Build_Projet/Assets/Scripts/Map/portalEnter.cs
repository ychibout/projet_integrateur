/* 
 * Elisa Kalbé
 * portalEnter.cs
 * 
 * Script pour les actions suivants le passage d'un portail
 * Lorsqu'on passe dans un portail, il disparaît et réapparait 
 * aléatoirement dans la map.
 * A ajouter : boost
 * 
 * Dans la scene Game
 * A mettre sur l'objet : Circle (enfant de l'objet portal)
 *  
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalEnter : MonoBehaviour {


	// Lorsque le joueur passe un portail
	private void OnCollisionEnter(Collision joueur){

		// on verifie qu'il s'agit d'un joueur
		if (joueur.collider.tag == "Equipe1" || joueur.collider.tag == "Equipe2") {

			// On récupère les caractéristiques du portail concerné
			GameObject parent = this.transform.parent.gameObject;

			// ---- On regarde quel bonus le joueur a eu
			// INVINCIBILITE : le joueur ne peux etre touché pendant quelques secondes
			if (parent.tag == "Invincible") {
				Debug.Log ("Invincible");
				StartCoroutine (invincible(joueur));
			}

			// INVISIBILITE : le joueur ne peux etre vu pendant quelques secondes
			if (parent.tag == "Invisible") {
				StartCoroutine (invisible(joueur));
			}

			// DEGAT : le joueur fait plus de dégat pendant quelques secondes
			if (parent.tag == "Degat") {
				Debug.Log ("Degat");
				StartCoroutine (degat(joueur));
			}

			// ESPION : le joueur apparaît aux couleurs de l'équipe adversaire pendant quelques secondes
			if (parent.tag == "Espion") {
				Debug.Log ("Espion");
				StartCoroutine (espion(joueur));
			}

			// ACCELERATION : le vaisseau du joueur accelere pendant quelques secondes
			if (parent.tag == "Accelere") {
				StartCoroutine (accelere(joueur));
			}


			// ---- On change le portail de position
			// On modifie sa position
			parent.transform.position = Random.insideUnitSphere * 170; // idem que dans portalCreation ( a changer selon scale de la map)
			// On modifie sa rotation
			parent.transform.rotation = Random.rotationUniform;
		}

	}

	// Routine pour acceleration
	private IEnumerator accelere(Collision joueur){

		// recupere joueur
		GameObject obj = joueur.transform.parent.gameObject;

		// demarre acceleration
		obj.GetComponent<PlayerInput> ().Speed=60; // modifie valeur vitesse

		// attend 5 secondes
		yield return new WaitForSeconds(10);

		// fini acceleration
		obj.GetComponent<PlayerInput>().Speed=30; //valeur acceleration par défault

	}

	// Routine pour invisibilité
	private IEnumerator invisible(Collision joueur){

		// demarre invisible
		GameObject obj = joueur.gameObject.transform.GetChild (0).gameObject;
		Debug.Log (obj.name);
		Color alphaColor = obj.GetComponent<MeshRenderer>().material.color;
		obj.GetComponent<MeshRenderer> ().material.color = new Color(alphaColor.r, alphaColor.g, alphaColor.b, 0);

		// attend 5 secondes
		yield return new WaitForSeconds(10);

		// fin invisible
		obj.GetComponent<MeshRenderer> ().material.color = new Color(alphaColor.r, alphaColor.g, alphaColor.b, 255);

	}

	// Routine pour augmenter degat
	private IEnumerator degat(Collision joueur){

		// recupere joueur
		GameObject obj = joueur.transform.parent.gameObject;

		// demarre acceleration
		Debug.Log("debut degat (manque script guillaume)");
		// modifie valeur degat

		// attend 5 secondes
		yield return new WaitForSeconds(10);

		// fini acceleration
		// remet valeur degat par défault
		Debug.Log("Fin degat");

	}

	// Routine pour mode espion
	private IEnumerator espion(Collision joueur){
		
		// recupere joueur
		GameObject obj = joueur.transform.parent.gameObject;

		// demarre mode espion
		Debug.Log("debut espion (manque prefab nathan)");

		// modifie couleur text

		// attend 5 secondes
		yield return new WaitForSeconds(10);

		// fin
		Debug.Log("Fin espion");

	}

	// Routine pour invincibilité
	private IEnumerator invincible(Collision joueur){

		// recupere joueur
		GameObject obj = joueur.gameObject.transform.GetChild (3).gameObject;

		// demarre invincible
		obj.SetActive(true);

		// attend 5 secondes
		yield return new WaitForSeconds(10);

		// fin
		obj.SetActive(false);

		Debug.Log("Fin invincible");

	}
}
