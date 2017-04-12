using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InscriptionUser : MonoBehaviour {

	public InputField joueur_nomInput;
	public InputField joueur_mdpInput;
	public InputField joueur_mdpconfirmInput;

    public string joueur_nom;
    public string joueur_mdp;
    public string joueur_mdp_confirm;
	public Text erreur;
	public Text nom;


    WWW request;
    WWWForm form;

	private string url = "localhost/Integrateur/insert_connect.php";
    // Update is called once per frame
    private IEnumerator coroutine;
    //Insert
    IEnumerator Insert()
    {
		joueur_nom = joueur_nomInput.text;
		joueur_mdp = joueur_mdpInput.text;
		joueur_mdp_confirm= joueur_mdpconfirmInput.text;

		if (joueur_mdp.Equals("") || joueur_nom.Equals(""))
		{
			erreur.text= "Veuillez saisir votre login et mot de passe";
			yield break;
		}

        //test si les deux mdp ok:
        if (joueur_mdp != joueur_mdp_confirm)
        {
			erreur.text = ("Les deux mots de passe ne correspondent pas");
            yield break;
        }

		erreur.text =("Mot de passe valide");

        //test si le login est unique
        form = new WWWForm();
        form.AddField("cat", "testLogin");
        form.AddField("joueur_nom", joueur_nom);

        request = new WWW(url, form);
        yield return request;

        string result = request.text;
		Debug.Log (result);
        
        if (result == "error")
        {
			erreur.text = ("Ce nom est déjà utilisé\n");
            yield break;
        }
        else
        {
			nom.text = "Nom utilisable";
        }

        //insertion
        form = new WWWForm();
        form.AddField("cat", "insert");
        form.AddField("joueur_nom", joueur_nom);
        form.AddField("joueur_mdp", joueur_mdp);

        request = new WWW(url, form);
        yield return request;

        result = request.text;

        if (result == "error")
        {
			erreur.text = ("Une erreur est survenue\n");
            yield break;
        }
        else
        {
			erreur.text = ("Joueur créé , Bienvenue");
        }

    }

    //to change
    void Update()
    {
        
    }

	public void bttninscription()
	{
			coroutine = Insert();
			StartCoroutine(coroutine);
	}
}
