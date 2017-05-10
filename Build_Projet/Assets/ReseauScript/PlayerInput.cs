using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour {

	private PlayerReseau Action;
	public GameObject playerCamera;

	// Weaponery prefab
	public GameObject _LaserPrefab;		   	// Laser prefab
	public GameObject _MissilePrefab; 		// Missile prefab

	//Variable de speed
	public float Speed;
	public float SpeedMax;
	public float SpeedMin;

	// si bonus en cours
	public bool bonus=false;

	// si le joueur est dehors
	public int playerOut = 0;

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
		Action = transform.GetComponent<PlayerReseau> ();

	}
	
	// Update is called once per frame
	void /*Fixed*/Update () {

		if (!isLocalPlayer) {
			playerCamera.SetActive (false);
			return;
		} 


		/*if (Input.GetKeyDown ("space") && isLooping == false && isLoopingH == false) {
			isLooping = true;
			//camera.SendMessage("setSuivre", false);
		}

		if(Input.GetKeyDown("left ctrl") && isLoopingH == false && isLooping==false)
		{
			isLoopingH = true;
			//camera.SendMessage("setStraightFollow", true);
		}*/

		// Action : shoot laser
		if (Input.GetKeyDown ("mouse 0") && isLoopingH == false && isLooping == false) 
		{
			Debug.Log ("ici1");
			Action.CmdUseWeaponery (_LaserPrefab, Speed);
		}

		// Action : shoot missile
		if (Input.GetKeyDown ("mouse 1") && isLoopingH == false && isLooping == false) 
		{
			Debug.Log ("ici2");
			Action.CmdUseWeaponery (_MissilePrefab, Speed);
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
		Action.CmdUpdatePosition(translate,rotate);
	}

	bool estLocalPlayer(){
		return isLocalPlayer;
	}
}