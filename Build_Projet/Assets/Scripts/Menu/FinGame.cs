using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class FinGame : MonoBehaviour {

	private NetworkManager nm ;
	// Use this for initialization
	void Start () {
		nm = GameObject.FindGameObjectWithTag ("Network").GetComponent<NetworkManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void quitter()
	{
		nm.StopServer();
		SceneManager.LoadScene ("ChoixEquipe");
	}
}
