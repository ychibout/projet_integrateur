using UnityEngine;
using System.Collections;

//by Nathan URBAIN

public class Information : MonoBehaviour {

	public string joueur;
	public string lvl;
	public int id;
	public string experience;
	public Vaisseau vaisseau_actif;
	public string model;
	public int team;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
