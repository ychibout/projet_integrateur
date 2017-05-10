using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraReseau : NetworkBehaviour {

	public GameObject playervaisseau;
	public GameObject camera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//######################################################################
	//######################################################################

	[Command]
	public void CmdFollowStraight (float hauteur , float distance)
	{
		//Position du vaisseau par rapport a la camera suivant les valeurs données 
		Vector3 _vaisseaupos = playervaisseau.transform.TransformPoint(0, hauteur , -distance);

		//Position camera 
		camera.transform.position = Vector3.Lerp(camera.transform.position, _vaisseaupos, 0.09f);

		//Rotation suivant la direction de devant (positon actuelle - inital) et d'au dessus (la position actuelle)
		Quaternion _vaisseaurotat = Quaternion.LookRotation(playervaisseau.transform.position - _vaisseaupos, playervaisseau.transform.up);

		RpcFollowStraight(hauteur, distance);
	}


	[ClientRpc]
	void RpcFollowStraight (float hauteur , float distance)
	{
		//Position du vaisseau par rapport a la camera suivant les valeurs données 
		Vector3 _vaisseaupos = playervaisseau.transform.TransformPoint(0, hauteur , -distance);

		//Position camera 
		camera.transform.position = Vector3.Lerp(camera.transform.position, _vaisseaupos, 0.09f);

		//Rotation suivant la direction de devant (positon actuelle - inital) et d'au dessus (la position actuelle)
		Quaternion _vaisseaurotat = Quaternion.LookRotation(playervaisseau.transform.position - _vaisseaupos, playervaisseau.transform.up);
	}


	//######################################################################
	//######################################################################

	[Command]
	public void CmdlookVaisseau()
	{
		playervaisseau.transform.LookAt (playervaisseau.transform, camera.transform.up);
		RpclookVaisseau ();
	}


	[ClientRpc]
	void RpclookVaisseau()
	{
		playervaisseau.transform.LookAt (playervaisseau.transform, camera.transform.up);
	}

	//######################################################################
	//######################################################################

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
	}

	//######################################################################
	//######################################################################
}
