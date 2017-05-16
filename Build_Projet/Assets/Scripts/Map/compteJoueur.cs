using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class compteJoueur : NetworkBehaviour {

	public static List<string> joueurEquipe1 = new List<string>();
	public static List<string> joueurEquipe2 = new List<string>();

	public void ajout(string name, int equipe){
		if (!isServer)
			return;
		if (equipe == 1)
			joueurEquipe1.Add (name);
		else
			joueurEquipe2.Add(name);
		affiche();
	}
		
	public void supprimer(string name, int equipe){
		if (!isServer)
			return;
		if (equipe == 1)
			joueurEquipe1.Remove (name);
		else
			joueurEquipe2.Remove (name);
		
	}
		

	public void affiche(){
	
		Debug.Log ("equipe 1 : ");
		foreach(string s in joueurEquipe1)
		{
			Debug.Log (s);
		}
	}

}
