using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//by Nathan URBAIN

public class menu : MonoBehaviour {

	private GameObject player;
	private GameObject vaisseau;
	public Text erreur;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void deconnexion()
	{
		player = GameObject.FindGameObjectWithTag ("Information");
		vaisseau = GameObject.FindGameObjectWithTag ("Vaisseaux");
		Destroy (player);
		Destroy (vaisseau);
		SceneManager.LoadScene ("Connexion");
	}

	public void toinscription()
	{
		SceneManager.LoadScene ("Inscription");
		player = GameObject.FindGameObjectWithTag ("Information");
		Destroy (player);
	}

	public void toconnexion()
	{
		SceneManager.LoadScene ("Connexion");
	}

	public void toplay()
	{
		player = GameObject.FindGameObjectWithTag ("Information");
		if (player.GetComponent<Information>().vaisseau_actif == null){
			erreur.text = "Vous n'avez choisi aucun vaisseau";
		} else
			SceneManager.LoadScene ("ChoixEquipe");
	}

	public void tovaisseau()
	{
		SceneManager.LoadScene ("ChoixVaisseau");
	}

	public void toprofil()
	{
		SceneManager.LoadScene ("Profil");
	}
}
