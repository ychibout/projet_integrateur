using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraInput : NetworkBehaviour {

	//Objet camera
	public Camera camera;
	public GameObject vaisseau;
	private CameraReseau changement;


	//variable pour camera 
	public Ray viewligne;
	public Vector2 SourisPos;
	protected bool suivreJoueur;
	protected bool straightFollow;

	//Variable de player 
	private Transform _transformplayer;
	private Vector3 _vaisseaupos;

	//Variable de marge de deplacement
	public float hauteur = 3f;
	public float distance = 50f;
	public float marge = 0.00010f;

	// Use this for initialization
	void Start () {
		suivreJoueur = true;
		straightFollow = false;
		_transformplayer = vaisseau.transform;
		changement = transform.GetComponent<CameraReseau> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isLocalPlayer) {
			return;
		}
			
		viewligne = camera.ScreenPointToRay (SourisPos);

		if(!suivreJoueur && !straightFollow)
			lookVaisseau ();
		if (straightFollow && suivreJoueur)
			FollowStraight();
		if(suivreJoueur && !straightFollow)
			SmoothFollow();
	}

	/*void LateUpdate(){
		if(!suivreJoueur && !straightFollow)
			lookVaisseau ();
		if (straightFollow && suivreJoueur)
			FollowStraight();
		if(suivreJoueur && !straightFollow)
			SmoothFollow();
	}*/


	void SmoothFollow(){
		changement.CmdSmoothFollow (hauteur, distance);
	}

	void FollowStraight(){
		//Le joueur suit le curseur avec delai
		changement.CmdFollowStraight(hauteur, distance);
	}

	void lookVaisseau()
	{
		changement.CmdlookVaisseau ();
	}

	void setSuivre(bool b)
	{
		suivreJoueur = b;
	}

	void setStraightFollow(bool b)
	{
		straightFollow = b;
	}
}
