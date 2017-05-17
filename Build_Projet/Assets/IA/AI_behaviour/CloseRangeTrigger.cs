using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeTrigger : MonoBehaviour {

	public GameObject _Self; // GameObject of the AI
	public string _EnemyTeamTag; // Enemy team tag
	private GameObject Enemy; // GameObject of Enemy


	/**
	* Method : OnTriggerEnter
	* Param :  Collider, intruder - the GO that enter the trigger
	* Desc : Send a message to the AI. Tell it that someone is on there close fight range.
	* Return : Void
	**/
	void OnTriggerEnter (Collider intruder) 
	{
		if ((intruder.tag == _EnemyTeamTag) && (intruder.GetComponent<Croiseur>() != null)) 
		{
			Enemy = intruder.gameObject;
			_Self.GetComponent<AIBehaviour>().GettingCloser(intruder.gameObject);
		}
	}
	

	/**
	* Method : ChangeStateRoaming
	* Param :  void
	* Desc : Set the CurrentState to Roaming
	* Return : Void
	**/
	void OnTriggerExit (Collider intruder) 
	{
		if ((intruder.tag == _EnemyTeamTag) && (intruder.GetComponent<Croiseur>() != null)) 
		{
			_Self.GetComponent<AIBehaviour>().IamYourPrey(Enemy);
		}
	}


}
