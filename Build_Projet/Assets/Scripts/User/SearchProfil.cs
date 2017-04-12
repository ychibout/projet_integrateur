using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SearchProfil : MonoBehaviour {


	public Text name;
	public Text xp;
	public Text lvl;

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Information");
		Information _infos =  player.GetComponent<Information>();

		name.text = _infos.joueur;
		xp.text = _infos.experience;
		lvl.text = _infos.lvl;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
