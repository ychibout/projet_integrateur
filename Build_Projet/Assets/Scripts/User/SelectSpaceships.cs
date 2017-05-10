using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine.SceneManagement;

//by Nathan URBAIN & Lucas Pierrat

public class SelectSpaceships : MonoBehaviour {

	private GameObject player;
	private GameObject[] vaisseau;

    WWW request;
    WWWForm form;

    private JsonData itemData;

	private string url = "localhost/Integrateur/selectPlayerSpaceships.php";
    private IEnumerator coroutine;

	public IEnumerator SelectPlayerSpaceship(int _joueur_id)
    {
        form = new WWWForm();
        form.AddField("cat", "selectSpaceships");
		form.AddField ("joueur_id", _joueur_id);
        request = new WWW(url, form);
        yield return request;

        string result = request.text;

        itemData = JsonMapper.ToObject(result);

		Debug.Log (itemData.Count);
        for (int i = 0; i < itemData.Count; i++)
        {
			//i = vaisseau , j = pv , degat , vitesse , nom , type
			vaisseau[i].GetComponent<Vaisseau>().pv = Int32.Parse(itemData[i][0].ToString());
			vaisseau[i].GetComponent<Vaisseau>().degat = Int32.Parse(itemData[i][1].ToString());
			vaisseau[i].GetComponent<Vaisseau>().vitesse = Int32.Parse(itemData[i][2].ToString());
			vaisseau[i].GetComponent<Vaisseau>().nom = itemData[i][3].ToString();
			vaisseau[i].GetComponent<Vaisseau>().type = itemData[i][4].ToString();
			vaisseau[i].GetComponent<Vaisseau>().id = i+1;           
        }

		SceneManager.LoadScene ("ChoixVaisseau");
        
    }

    // Use this for initialization
    void Start () {
    }

	public void bttnvaisseau(){
		player = GameObject.FindGameObjectWithTag ("Information");
		Information _infos =  player.GetComponent<Information>();

		searchvaisseau ();
		coroutine = SelectPlayerSpaceship(_infos.id);
		StartCoroutine(coroutine);

	}

	void searchvaisseau()
	{
		vaisseau = GameObject.FindGameObjectsWithTag("Vaisseaux");
	}
}
