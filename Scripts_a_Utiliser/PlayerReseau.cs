using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerReseau : NetworkBehaviour {

	public GameObject playervaisseau;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[Command]
	public void CmdUpdatePosition(/*float z , float x */ Vector3 translate , Vector3 rotate)
	{
		transform.Translate(translate);
		transform.Rotate(rotate);
		RpcUpdatePosition (translate, rotate);

		/*playervaisseau.transform.Translate(0,0,z);
		playervaisseau.transform.Rotate(0,x,0);

		RpcUpdatePosition (z, x);*/
	}


	[ClientRpc]
	void RpcUpdatePosition( /*float z , float x*/ Vector3 translate , Vector3 rotate )
	{
		/*transform.Translate(0,0,z);
		transform.Rotate(0,x,0);*/

		transform.Translate(translate);
		transform.Rotate(rotate);
	}

	/*
	[Command]
	public void CmdSmoothFollow(float hauteur , float distance){
		//Position du vaisseau par rapport a la camera suivant les valeurs données 
		Vector3 _vaisseaupos = playervaisseau.transform.TransformPoint(0, hauteur , -distance);

		//Position camera 
		camera.transform.position = Vector3.Lerp(camera.transform.position, _vaisseaupos, 0.09f);

		//Rotation suivant la direction de devant (positon actuelle - inital) et d'au dessus (la position actuelle)
		Quaternion _vaisseaurotat = Quaternion.LookRotation(playervaisseau.transform.position - _vaisseaupos, playervaisseau.transform.up);

		//Rotation camera 
		camera.transform.rotation = Quaternion.Slerp (camera.transform.rotation, _vaisseaurotat, 0.08f);

		RpcSmoothFollow (hauteur, distance);
	}

	[ClientRpc]
	void RpcSmoothFollow(float hauteur , float distance)
	{
		//Position du vaisseau par rapport a la camera suivant les valeurs données 
		Vector3 _vaisseaupos = playervaisseau.transform.TransformPoint(0, hauteur , -distance);

		//Position camera 
		camera.transform.position = Vector3.Lerp(camera.transform.position, _vaisseaupos, 0.09f);

		//Rotation suivant la direction de devant (positon actuelle - inital) et d'au dessus (la position actuelle)
		Quaternion _vaisseaurotat = Quaternion.LookRotation(playervaisseau.transform.position - _vaisseaupos, playervaisseau.transform.up);

		//Rotation camera 
		camera.transform.rotation = Quaternion.Slerp (camera.transform.rotation, _vaisseaurotat, 0.08f);
	}*/
}

/*Pour test*/
/*
public void CmdUpdatePosition(float z , float x)
{
	transform.translate(0,0,z);
	transform.rotate(0,x,0);
	RpcUpdatePosition (z, x);
}
*/