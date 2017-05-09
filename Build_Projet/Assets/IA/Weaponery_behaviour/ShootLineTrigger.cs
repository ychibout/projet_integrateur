using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLineTrigger : MonoBehaviour {

	// Public attributes
	public GameObject Player;
	public string _Team1Tag;
	public string _Team2Tag;

	// Private attributes
	private PlayerReseau PlayerScript;

	// Use this for initialization
	void Start () 
	{
		PlayerScript = Player.GetComponent<PlayerReseau> ();
	}


	/**
	 * Method : OnTriggerEnter
	 * Param : Collider, intruder - Collider reference from the GameObject entering the trigger
	 * Desc : If the GameObject entering the trigger is tagged with the enemey team tag, set the _Prey GO
	 * 		  reference on the PlayerReseau script with the intruder GameObject.
	 * Return : void
	 **/
	void OnTriggerEnter(Collider intruder)
	{
		if (((intruder.tag == _Team1Tag) && (intruder.tag != Player.transform.tag)) || ((intruder.tag == _Team2Tag) && (intruder.tag != Player.transform.tag))) 
		{
			Debug.Log (" J'ai un enemi en ligne de mire !");
			PlayerScript.setPreyGO (intruder.gameObject);
		}
	}


	/**
	 * Method : OnTriggerExit
	 * Param : Collider, intruder - Collider reference from the GameObject entering the trigger
	 * Desc : If the GameObject entering the trigger is tagged with the enemey team tag, set the _Prey GO
	 * 		  reference on the PlayerReseau script with null.
	 * Return : void
	 **/
	void OnTriggerExit(Collider intruder)
	{
		if (((intruder.tag == _Team1Tag) && (intruder.tag != Player.transform.tag)) || ((intruder.tag == _Team2Tag) && (intruder.tag != Player.transform.tag))) 
		{
			Debug.Log (" Je n'ai plus d'enemi en ligne de mire !");
			PlayerScript.setPreyGO (null);
		}
	}

}
