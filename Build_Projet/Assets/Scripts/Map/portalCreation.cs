/* 
 * Elisa Kalbé
 * portalCreation.cs
 * 
 * Script qui créée un certain nombre de portail à des
 * positions aléatoires comprises dans la sphère de la map.
 * On crée un nombre égal de portal pour chaque bonus qu'ils donnent
 * càd : invincibilité, invisibilité, mode espion, accélération, dégats+.
 * 
 * Dans la scene Game
 * A mettre sur l'objet : CreationMap
 * 
 * Paramètre :
 * 		- Portal : portal
 * 			model du portail
 * 		- CounterP : 0
 * 			nombre de portails present dans la scène
 * 		- NbrP : 25
 * 			nombre de portail a cloner (défini en fonction de la taille de la map
 * 			ici c'est pour un scale de la map de 500)
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class portalCreation : MonoBehaviour {

	public GameObject portal; // objet portal
	public int counterP = 0; // compteur nbr portail
	public int nbrP = 25; // nbr voulu

	/*********************************************************************
	************************* START & UPDATE *****************************
	*********************************************************************/

	void Start () {	

		//creation des portails
		creation ();
	}


	/*********************************************************************
	************************** FONCTIONS *********************************
	*********************************************************************/

	// creation des portails
	void creation(){

		// boucle pour la creation portail
		while( counterP < nbrP){

			GameObject newPortal; 
			newPortal = Instantiate(portal); 
			// Position aléatoire dans la sphere de la map ici pour un scale de la map de 500
			newPortal.transform.position = Random.insideUnitSphere * 200; // A CHANGER SI ON MODIFIE TAILLE LIMITe DE JEU
			// Rotation aléatoire
			newPortal.transform.rotation = Random.rotationUniform;
			counterP++;

			// défini le tag en fonction du bonus
			// Bonus invincibilité
			if (counterP <= (nbrP / 5))
				newPortal.tag = "Invincible";

			// Bonus invisibilité
			else if (counterP <= ((nbrP / 5) * 2))
				newPortal.tag = "Invisible";

			// Bonus acceleration
			else if (counterP <= ((nbrP / 5) * 3))
				newPortal.tag = "Accelere";

			// Bonus degats+
			else if (counterP <= ((nbrP / 5) *4 ))
				newPortal.tag = "Degat";

			// Bonus mode espion
			else if (counterP <= ((nbrP / 5) * 5))
				newPortal.tag = "Espion";
			
		}
	}

}
