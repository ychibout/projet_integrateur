using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DisconnectClient : MonoBehaviour  {

	private NetworkManager nm ;
	// Use this for initialization
	void Start () {
		nm = GetComponent<NetworkManager> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void disconnectClient(GameObject client)
	{
		nm.playerPrefab = client;
		nm.StopClient ();
	}
}
