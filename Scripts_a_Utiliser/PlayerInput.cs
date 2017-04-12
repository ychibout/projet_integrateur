using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour {

	private PlayerReseau changement;
	public GameObject playerCamera;

	//Variable de speed
	public float Speed;
	public float SpeedMax;
	public float SpeedMin;

	public float tempsLooping = 0.1f;
	public GameObject camera;
	protected Vector3 posCamera;
	protected Quaternion rotCamera;

	protected bool isLooping ; //vrai uniquement lors d'un looping vertical
	protected bool isLoopingH; //vrai uniquement lors d'un looping horizontal


	//Varibale d'update
	private Vector3 rotate;
	private Vector3 translate;

	float progression = 0, valRotation = 0;


	// Use this for initialization
	void Start () {
		
		isLooping = false;
		isLoopingH = false;
		changement = transform.GetComponent<PlayerReseau> ();

	}
	
	// Update is called once per frame
	void /*Fixed*/Update () {

		if (!isLocalPlayer) {
			playerCamera.SetActive (false);
			return;
		} 


		if (Input.GetKeyDown ("space") && isLooping == false && isLoopingH == false) {
			isLooping = true;
			//camera.SendMessage("setSuivre", false);
		}

		if(Input.GetKeyDown("left ctrl") && isLoopingH == false && isLooping==false)
		{
			isLoopingH = true;
			//camera.SendMessage("setStraightFollow", true);
		}

		if (!isLooping && !isLoopingH)
		{
			//Recupere la postion de la souris 
			Vector3 MvtSouris = (Input.mousePosition - (new Vector3(Screen.width, Screen.height, 0) / 2f));

			//Donne le nouveau vecteur de rotation
			rotate = new Vector3(-MvtSouris.y, MvtSouris.x, 0) * 0.010f;
		}

		//Vecteur de translation
		translate = Vector3.forward * Time.deltaTime * Speed;

		//Envoie de la nouvelle pos au serveur et client -> fait l'echange 
		changement.CmdUpdatePosition(translate,rotate);

	}
}


/*Pour test*/
/*
var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
var z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;
changement.CmdUpdatePosition (z, x);
*/