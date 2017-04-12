/*
 * Script permttant le mouvement circulaire des asteroides et contenant les fonctions:
 * - randomRotate : rotation aleatoire des asteroides sur eux-memes
 * - moveInCircle : rotation autour d'un centre passe en argument (une planete ou un gameObject autre)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// by Cyrielle Endenmann

public class asteroidBehavior : MonoBehaviour {

	public GameObject center;
	public float xDir, yDir, zDir;
	public int speed;
	private float x, y, z;
	private float xCenter, yCenter, zCenter;


	/*********************************************************************
	************************* START & UPDATE *****************************
	*********************************************************************/

	void Start () {
		// Coordonnees de rotation
		x = Random.Range (1, 30);
		y = Random.Range (1, 30);
		z = Random.Range (1, 30);
		// Coordonnees du centre du cercle de rotation
		xCenter = center.transform.position.x;
		yCenter = center.transform.position.y;
		zCenter = center.transform.position.z;
	}

	void Update () {
		randomRotation ();
		moveInCircle ();
	}



	/*********************************************************************
	************************** FONCTIONS *********************************
	*********************************************************************/

	void randomRotation() {
		transform.Rotate (new Vector3 (x, y, z) * Time.deltaTime);
	}

	void moveInCircle() {
		Vector3 center = new Vector3 (xCenter, yCenter, zCenter);
		// Declaration de la direction de rotation
		Vector3 dir = new Vector3 (xDir, yDir, zDir);
		GetComponent<Rigidbody>().transform.RotateAround(new Vector3(xCenter, yCenter, zCenter), dir, speed * Time.deltaTime);
	}
}
