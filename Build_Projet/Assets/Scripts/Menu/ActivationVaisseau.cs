using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//by Nathan URBAIN

public class ActivationVaisseau : MonoBehaviour {

	public GameObject txtactifspeed;
	public GameObject txtactiftank;
	public GameObject txtactifdamage;
	public GameObject modelspeed;
	public GameObject modeltank;
	public GameObject modeldmg;
	public GameObject actif;
	private GameObject player;
	private Information info;
	private GameObject[] vaisseaux;


	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Information");
		info = player.GetComponent<Information> ();
		txtactifspeed.SetActive (false);
		txtactiftank.SetActive (false);
		txtactifdamage.SetActive (false);
		vaisseaux = GameObject.FindGameObjectsWithTag("Vaisseaux");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void selectionspeed()
	{
		actif.SetActive (false);
		txtactifspeed.SetActive (true);
		actif =txtactifspeed;
		setActiveVaisseau ("X-Wing" , modelspeed);
	}

	public void selectiontank()
	{
		actif.SetActive(false);
		txtactiftank.SetActive(true);
		actif = txtactiftank;
	}

	public void selectiondamage()
	{
		actif.SetActive (false);
		txtactifdamage.SetActive (true);
		actif = txtactifdamage;
		setActiveVaisseau ("Nef Royale" , modeldmg);
	}


	int searchIndexVaisseau (string name)
	{
		int result = -1;
		for (int i=0; i < vaisseaux.Length; i++) {
			Vaisseau info = vaisseaux[i].GetComponent<Vaisseau> ();
			if (info.nom.Equals(name) == true) {
				result = i;
				return result;
			}
		}
		return result;
	}

	void setActiveVaisseau(string name , GameObject model)
	{
		int index = searchIndexVaisseau (name);
		GameObject vaisseau = vaisseaux[index];
		Vaisseau infovaisseau = vaisseau.GetComponent<Vaisseau>();
		player.GetComponent<Information>().vaisseau_actif = infovaisseau;
		player.GetComponent<Information> ().model = model.name;
		Debug.Log (player.GetComponent<Information> ().model);
	}

}
