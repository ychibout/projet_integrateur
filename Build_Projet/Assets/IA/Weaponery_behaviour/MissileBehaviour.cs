using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MissileBehaviour : NetworkBehaviour {


	private GameObject Target; // Target of the missile
	private float LifeTime; // Life time of the missile
	private float MissileSpeed; // Speed of the missile
	private float Rotation_speed; // Rotation speed of the missile
	private float Damage; // Amount of damage dealed by missile
	private string EnemyTag; // Enemy tag

	// Use this for initialization
	void Start () 
	{
		Damage = 5.0f;
		LifeTime = 4.0f;
		MissileSpeed = 45.0f;
		Rotation_speed = 1.2f;
	}
	
	// Update is called once per frame
	// Calculate the missile movement and destroy the missile when LifeTime == 0.0f
	void Update () 
	{
		if (LifeTime > 0.0f) 
		{
			if (Target != null) 
			{
				Move (Target.transform.position);
			} 
			else 
			{
				Move (transform.forward);
			}

			LifeTime = LifeTime - 0.1f * Time.deltaTime;
		} 
		else 
		{
			NetworkServer.Destroy (transform.gameObject);
		}
	}


	/**********************
	 *  Movement methods  *
	 **********************/


	/**
	* Method : RpcMove
	* Param :  void
	* Desc : Move the AI forward
	* Return : Void
	**/
	void Move(Vector3 target)
	{
		RpcRotateView (target);
		RpcMoveForward ();
	}


	/**
	* Method : MoveForward
	* Param :  void
	* Desc : Move the AI forward
	* Return : Void
	**/
	//[ClientRpc]
	void RpcMoveForward()
	{
		// Moving forward all the time
		transform.position += transform.forward * MissileSpeed * Time.deltaTime;
	}


	/**
	* Method : RotateView
	* Param :  void
	* Desc : Apply the rotation of the AI movement step by step to the target
	* Return : Void
	**/
	//[ClientRpc]
	void RpcRotateView(Vector3 target)
	{
		Vector3 MyOwnPosition = transform.position;
		// Rotate not instantly the AI ship
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - MyOwnPosition), Time.deltaTime * Rotation_speed);
	}


	/**
	 * Method : OnTriggerEnter
	 * Param : collider, enemy - enemy collider
	 * Desc : Send the message "TakeDamage" to the collider with the amount of damage as argument 
	 * Return : void
	 **/
	void OnTriggerEnter(Collider intruder)
	{
		if (string.Compare(intruder.tag, EnemyTag) == 0) 
		{
			intruder.GetComponent<AIBehaviour>().TakeDamage(Damage);
			NetworkServer.Destroy (transform.gameObject);
		}
	}


	/**
	 * Method : GoTo
	 * Param : ArrayList, args - arguments passed by the sendMessage when the laser is shot
	 * Desc : Extract information from the shooter, take the target position and tag and add potential additional damage of the shooter (due to its level)
	 *        Then set the Launch bool to true
	 * Return : void
	 **/
	public void GoTo(ArrayList args)
	{
		Target = (GameObject)args[0];
		EnemyTag = Target.tag;
		Damage += (float)args [1];
	}

}
