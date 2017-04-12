using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class connectionUser : MonoBehaviour
{
	public InputField joueur_nomInput;
	public InputField joueur_mdpInput;
	public Text erreur;

	public GameObject player;

	private String joueur_nom;
	private String joueur_mdp;

    WWW request;
    WWWForm form;

    private string url = "localhost/Integrateur/insert_connect.php";
    private IEnumerator coroutine;

    private bool connected;

    //Insert
    IEnumerator Connection()
    {


		joueur_nom = joueur_nomInput.text;
		joueur_mdp = joueur_mdpInput.text;

        connected = false;

        //Si le couple login/password n'est pas vide
        if (joueur_mdp.Equals("") || joueur_nom.Equals(""))
        {
			erreur.text= "Veuillez saisir votre login et mot de passe";
            yield break;
        }

        //test si le couple login/password existe dans la base
        form = new WWWForm();
        form.AddField("cat", "connection");
        form.AddField("joueur_nom", joueur_nom);
        form.AddField("joueur_mdp", joueur_mdp);

        request = new WWW(url, form);
        yield return request;

        string result = request.text;
		if (result == "Connection failed") {
			erreur.text = "Erreur connexion";
			yield break;
		}

        if (result != "1")
        {
			erreur.text="Login ou Mot de passe incorrect";
            yield break;
        }
        else
        {
            Debug.Log("OK, vous êtes connecté \n");
            connected = true;

        }

        if (connected)
        {
            form = new WWWForm();
            form.AddField("cat", "select");
            form.AddField("joueur_nom", joueur_nom);

            request = new WWW(url, form);
            yield return request;
            string data = request.text;

            if (data.Length == 0)
            {
                Debug.Log("error");
                yield break;
            }

            string[] infos = parser(data);

			Information _p = player.GetComponent<Information>();
			_p.joueur = infos [1];
			_p.id = Int32.Parse(infos [0]);
			_p.experience = infos [4];
			_p.lvl = infos [3];


			SceneManager.LoadScene ("Profil");
            yield break;



        }

    }



    //to change
    void Update()
    {
    }

	// id nom pwd lvl exp
	string[] parser(String data)
    {
        
        //delimiter
        char lineDelimiter = ';';
        char dataDelimiter = '|';

        //Split all lines
        string[] lines = data.Split(lineDelimiter);

        //size of lines and datas
        int lineSize = lines.Length - 1;
        int dataSize = lines[0].Split(dataDelimiter).Length;

        //result data array
        string[][] datas = new string[lineSize][];

        //init result data array
        for (int i = 0; i < lineSize; i++)
        {
            datas[i] = new string[dataSize];
        }

        //split lines and put result into result data array
        for (int i = 0; i < lines.Length - 1; i++)
        {
            datas[i] = lines[i].Split(dataDelimiter);
        }
			
       return datas[0];
    }



	public void bttnconnexion()
	{
		coroutine = Connection ();
		StartCoroutine (coroutine);
	}
}
