using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeTrigger : MonoBehaviour {

	public GameObject _Self; // GameObject of the AI
	public string _EnemyTeamTag; // Enemy team tag


	/**
	* Method : OnTriggerEnter
	* Param :  Collider, intruder - the GO that enter the trigger
	* Desc : Send a message to the AI. Tell it that someone is on there Long fight range.
	* Return : Void
	**/
	void OnTriggerEnter (Collider intruder) 
	{  
		if (intruder.tag == _EnemyTeamTag) 
		{
			_Self.GetComponent<AIBehaviour>().IamYourPrey(intruder.gameObject);
			Debug.Log ("je chasse" + intruder.name);
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
		if (intruder.tag == _EnemyTeamTag) 
		{
			_Self.GetComponent<AIBehaviour>().IamNoLongerThere();
		}
	}
}
