using UnityEngine;
using System.Collections;

//by Nathan URBAIN

public class PlayerControl : MonoBehaviour {

	//Variable de speed
	public float Speed;
	public float SpeedMax;
	public float SpeedMin;
	public float bulletSpeed;
	public GameObject bullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate(){

		//Recupere la postion de la souris 
		Vector3 MvtSouris = (Input.mousePosition - (new Vector3 (Screen.width, Screen.height, 0) / 2f));
		//Rotate suivant la position de la souris 
		transform.Rotate (new Vector3 (-MvtSouris.y, MvtSouris.x, 0) * 0.010f);
		//avance tout le temps a une vitesse donné
		transform.Translate (Vector3.forward * Time.deltaTime * Speed);

		looping();
		tirer();

	}


	void tirer()
	{
		if(Input.GetKeyDown("mouse 0"))
		{
			GameObject newBullet;
			newBullet = Instantiate(bullet);
			newBullet.transform.position = transform.position;
			newBullet.transform.rotation = transform.rotation;
			Rigidbody rb = newBullet.GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * bulletSpeed * Time.deltaTime);
		}
	}

	void looping()
	{
		if (Input.GetKeyDown ("q")){
			transform.Rotate( Vector3.forward * Time.deltaTime);
			Debug.Log ("q");
			return;
		}

		if (Input.GetKeyDown ("d")) {
			Debug.Log ("Test");
			return;
		}

		if (Input.GetKeyDown ("z")) {
			Debug.Log ("Test");
			return;
		}

		if (Input.GetKeyDown ("s")) {
			Debug.Log ("Test");
			return;
		}

	}
		
}
