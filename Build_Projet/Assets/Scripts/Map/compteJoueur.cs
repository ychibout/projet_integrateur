/*
 * Elisa Kalbé
 * Nathan Urbain
 * compteJoueur.cs
 * 
 * Script pour compter le nombre de joueur dans chaque equipe
 * 
 * obsolete
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compteJoueur : MonoBehaviour {

	public static int nbrJoueurEquipe1 = 0;
	public static int nbrJoueurEquipe2 = 0;

	// compte le nbr de joueur
	public void demarreCpt(){

		// pour les objets taggés Equipe1
		foreach (GameObject joueur in GameObject.FindGameObjectsWithTag ("Equipe1")) {
			// on garde les joueurs
			if (joueur.GetComponent<PlayerInput> () != null) {
				Debug.Log ("test;  ");

				Debug.Log(joueur.name);
				nbrJoueurEquipe1++;
			}
		}

		// pour les objets taggés Equipe2
		foreach (GameObject joueur in GameObject.FindGameObjectsWithTag ("Equipe2")) {
			// on garde les joueurs
			if (joueur.GetComponent<PlayerInput> () != null) {
				nbrJoueurEquipe2++;
			}
		}
	}
}


