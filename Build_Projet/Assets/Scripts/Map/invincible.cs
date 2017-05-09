/* 
 * Elisa Kalbé
 * invincible.cs
 * 
 * permet de rendre un joueur invincible
 *  - detruit les balles des quelles rentrent dans la zone
 * 
 * 
 * Dans scene Game
 * A mettre sur les prefab reseaux des joueur sur l'objet : invincible
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invincible : MonoBehaviour {

	// Lorsqu'un joueur tire en direction de la zone
	private void OnTriggerEnter(Collider collision){

		// Attendre la modif de code de Guillaume
		// On verife qu'il s'agit d'une balle
		if (collision.tag == "Bullet") {
			// on detruit la balle lorsqu'elle entre dans la zone safe
			Destroy(collision.gameObject);
			Debug.Log ("Balle entre");
		}
	}
}
