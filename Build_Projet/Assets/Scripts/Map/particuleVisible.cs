using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particuleVisible : MonoBehaviour {

	public GameObject particule;

	void Start () {
		GameObject joueur = transform.parent.gameObject.transform.parent.gameObject;
		if (joueur.GetComponent<PlayerInput> ().estLocalPlayer ())
			particule.SetActive (true);
		else
			particule.SetActive (false);
	}
	

}
