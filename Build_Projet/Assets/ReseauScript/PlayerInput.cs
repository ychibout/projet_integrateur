using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour {

	private PlayerReseau Action;
	public GameObject playerCamera;
	public GameObject playerMap;
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

	// Shoot control attributes
	private const float WaitTimeBeforShootMissile = 0.75f;
	private const float WaitTimeBeforeShootLaser = 0.3f;
	private const int MaxLasersShot = 10;
	private float TimerBeforeShootMissile;
	private float TimerBeforeShootLaser;
	private int nbLasersShot;


	// Use this for initialization
	void Start () {
		
		isLooping = false;
		isLoopingH = false;
		Action = transform.GetComponent<PlayerReseau> ();
		TimerBeforeShootMissile = 0.0f;
		TimerBeforeShootLaser = 0.0f;
		nbLasersShot = 0;
	}
	
	// Update is called once per frame
	void /*Fixed*/Update () {

		if (!isLocalPlayer) {
			playerCamera.SetActive (false);
			playerMap.SetActive (false);
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
			if ((TimerBeforeShootLaser <= 0.0f) && (nbLasersShot < MaxLasersShot)) 
			{
				Action.CmdUseWeaponery (0, Speed);
				nbLasersShot++;
			}
			else
			{
				if (TimerBeforeShootLaser <= 0.0f) 
				{
					TimerBeforeShootLaser = WaitTimeBeforeShootLaser;
				}
			}
		}

		// Action : shoot missile
		if (Input.GetKeyDown ("mouse 1") && isLoopingH == false && isLooping == false) 
		{
			if (TimerBeforeShootMissile <= 0.0f) 
			{
				Action.CmdUseWeaponery (1, Speed);

				if(TimerBeforeShootMissile <= 0.0f)
				{
					nbLasersShot = 0;
					TimerBeforeShootMissile = WaitTimeBeforShootMissile;
				}
			} 
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

		// Decrement timer if needed
		if(TimerBeforeShootLaser > 0.0f)
		{
			TimerBeforeShootLaser -= 0.1f * Time.deltaTime;
			nbLasersShot = 0;
		}
		if(TimerBeforeShootMissile > 0.0f)
		{
			TimerBeforeShootMissile -= 0.1f * Time.deltaTime;
		}
	}

	public bool estLocalPlayer(){
		return isLocalPlayer;
	}
}