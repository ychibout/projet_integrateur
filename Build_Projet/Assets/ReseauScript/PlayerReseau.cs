using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerReseau : NetworkBehaviour {

	// Public attributres
	public GameObject playervaisseau;
	public GameObject _Prey;

	// Private attributes
	[SyncVar]
	private int AdditionalDamage;

	// Use this for initialization
	void Start () 
	{
		_Prey = null;
		AdditionalDamage = 0;
	}

	[Command]
	public void CmdUpdatePosition(/*float z , float x */ Vector3 translate , Vector3 rotate)
	{
		transform.Translate(translate);
		transform.Rotate(rotate);
		RpcUpdatePosition (translate, rotate);
	}


	[ClientRpc]
	void RpcUpdatePosition( /*float z , float x*/ Vector3 translate , Vector3 rotate )
	{
		/*transform.Translate(0,0,z);
		transform.Rotate(0,x,0);*/

		transform.Translate(translate);
		transform.Rotate(rotate);
	}


	/**
	 * Methdo : changeAdditionalDamage
	 * Param : int newAddDamage
	 * Desc : Change the AdditionalDamage attribute of the player.
	 * Return : void
	 **/
	public void changeAdditionalDamage(int newAddDamage)
	{
		if (!isServer) // A player can't change its own additional damage, the server only get access to this method
			return;
		
		AdditionalDamage = newAddDamage;
	}


	/**
	 * Method : setPreyGO
	 * Param : GameObject, prey - The new GameObject reference to set into _Prey
	 * Desc : Set the given GameObject reference as the _Prey reference
	 * Return : void
	 **/
	public void setPreyGO(GameObject prey)
	{
		_Prey = prey;
	}


	/**
	 * Method : CmdUseWeaponery
	 * Param : GameObject, weaponery - the weapon use to attack the enemy
	 * Desc : Create an instance of the weaponery prefab and send it to the aim by calling SendMessage 
	 *        with the function GoTo and an ArrayList as arguments. The ArrayList is composed as the 
	 *        following : agrs[0] is the _Prey GameObject and args[1] is the AdditionalDamage that the 
	 *        shooter deal due to its level (or randomly chosen for AI)
	 * Return : void
	 **/
	[Command]
	public void CmdUseWeaponery(GameObject weaponery, float current_speed)
	{
		// Create a list of arguments to simplify the argument communication
		ArrayList args = new ArrayList (2);
		args.Add (_Prey);
		args.Add (AdditionalDamage);
		args.Add (transform.gameObject);
		// Instantiate an object of the used weapon and create it on each client
		Vector3 spawnPosition = transform.position + (transform.forward * current_speed);
		GameObject weapon = Instantiate (weaponery, spawnPosition, Quaternion.identity);
		NetworkServer.Spawn (weapon);

		// Call the good appropriate method on each case
		if (weapon.GetComponent<LaserBehaviour> () != null) // If we shoot a laser
		{
			weapon.GetComponent<LaserBehaviour> ().GoTo (args);
		} 
		else // If we launch a missile
		{
			weapon.GetComponent<MissileBehaviour> ().GoTo (args);
		}
	}



}
	