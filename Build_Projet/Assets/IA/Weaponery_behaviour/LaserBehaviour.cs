using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LaserBehaviour : NetworkBehaviour {

	private bool Launch; // Boolean to know if the missle has to be launched or not
	private float LifeTime; // Life time of the missile
	private float LaserSpeed; // Speed of the missile
	private float Damage; // Amount of damage dealed by laser
	private string EnemyTag; // enemy tag

	// Use this for initialization
	void Start () 
	{
		Damage = 2.0f;
		LifeTime = 3.0f;
		LaserSpeed = 150.0f;
	}

	// Update is called once per frame
	void Update () 
	{
		if (LifeTime > 0.0f) 
		{
			RpcMoveForward();
			LifeTime = LifeTime - 0.1f * Time.deltaTime;
		} 
		else // If the missile was on the field during a specified amount of seconds
		{
			NetworkServer.Destroy (transform.gameObject);
		}
	}

	/**
	* Method : RpcMoveForward
	* Param :  void
	* Desc : Move the AI forward
	* Return : Void
	**/
	//[ClientRpc]
	void RpcMoveForward()
	{
		// Moving forward all the time
		transform.position += transform.forward * LaserSpeed * Time.deltaTime;
	}


	/**
	 * Method : OnTriggerEnter
	 * Param : collider, enemy - enemy collider
	 * Desc : Send the message "TakeDamage" to the collider with the amount of damage as argument 
	 * Return : void
	 **/
	void OnTriggerEnter(Collider intruder)
	{

		if (string.Compare (intruder.tag, EnemyTag) == 0) {
			intruder.GetComponent<AIBehaviour> ().TakeDamage (Damage);
			Destroy (transform.gameObject);
		}
	}



	/**
	 * Method : GoTo
	 * Param : ArrayList, args - arguments passed to the laser when the laser is shot
	 * Desc : Extract information from the shooter, take the target position and tag and add potential additional damage of the shooter (due to its level)
	 *        Then set the Launch bool to true
	 * Return : void
	 **/
	public void GoTo(ArrayList args)
	{
		GameObject Target = (GameObject)args [0];
		GameObject Launcher = (GameObject)args [2];
		if (Target == null) 
		{
			LifeTime = 0.5f;
			if (Launcher.tag == "Equipe1") 
			{
				EnemyTag = "Equipe2";
			}
			else
			{
				EnemyTag = "Equipe1";
			}
		}
		else
		{
			EnemyTag = Target.tag;
		}
		Damage += (float)args [1];
		transform.rotation = Launcher.transform.rotation;
	}

}
