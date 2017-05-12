/*
 * Elisa Kalbé
 * menuTeam.cs
 * 
 * Script pour eviter la collision avec les objets de la scène
 * 
 * Dans scène Game
 * A mettre sur tous les objets avec qui une collision et possible.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionObject : MonoBehaviour {

	// Lorsqu'un joueur vient dans la zone
	private void OnTriggerEnter(Collider collision){

		// on "evite" la collision
		if (collision.tag == "Equipe1" || collision.tag == "Equipe2") {
			GameObject player = collision.transform.parent.gameObject;
			player.transform.Rotate (0, 90, 0);

		} 
	}
}
