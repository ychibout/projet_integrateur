using UnityEngine;
using System.Collections;

//by Nathan URBAIN

public class Exist : MonoBehaviour {

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
