using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/**
 * Class : PlayerHealth
 * Desc : This class is used to modelize the player health. The method TakeDamage
 * 		  is called by a missile or a laser, the health is decreased by the server.
 * 		  This prevent the client from cheating by increase his own life. If the 
 * 		  health is equal to zero, the player respawn.
 * How to : Place this script on the player prefab and add the player game object on the 
 * 			public reference Player.
 **/

public class PlayerHealth : NetworkBehaviour 
{

	// Public attributes
	public GameObject[] _SpawnPoints;		// All availaible spawn points for the player
	public const float MaxHealth = 30.0f;	// Maximum Health than player can have

	// Synchronized variable
	[SyncVar]
	public float Life; 						// Current health of the player


	// Use this for initialization
	void Start () 
	{
		Life = MaxHealth;
	}


	/**
	 * Method : TakeDamage
	 * Param : float, damage - Amount of taken damage
	 * Desc : Apply the taken damage on the current object life. If life <= 0, then the object is destroyed
	 * Return : void
	 **/
	public void TakeDamage(int damage)
	{
		// Verify if we are on the server side
		if (!isServer) // We can't call the method on the client side -- don't cheat !
			return;

		// Damage taken is 
		Life -= damage;
		if (Life < 1.0f) // The player die
		{ 
			Life = MaxHealth;
			Respawn ();
		}
	}


	/**
	 * Method : Respawn
	 * Param : void
	 * Desc : Called by the server, this method tell to the player that he has taken too much damage and he is dead.
	 * Return : void
	 **/
	public void Respawn()
	{
		Vector3 SpawnPoint = Vector3.zero;
		int i = 0;

		if (!isServer) // Verify if we are on server side -- only server can bring back AI to life
			return;

		if ((_SpawnPoints != null) && (_SpawnPoints.Length > 0)) 
		{
			i = Random.Range (0, _SpawnPoints.Length);
			SpawnPoint = _SpawnPoints [i].transform.position;
		}

		transform.position = SpawnPoint;
	}

}
