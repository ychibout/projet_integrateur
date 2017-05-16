using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GUIManager : NetworkBehaviour {

	private GUIStyle guiStyleTEAM = new GUIStyle();
	private GUIStyle guiStylePLAYER = new GUIStyle();

	private GUIStyle guiStyleVieAMI = new GUIStyle ();

	private GUIStyle guiStyleVieENEMI = new GUIStyle ();

	private NetworkManager nm = GameObject.FindGameObjectWithTag ("Network").GetComponent<NetworkManager>();

	public static GUIManager Instance;
	public bool ShowBoard;



	void Start(){
		Instance = this;
		guiStyleVieAMI.fontSize = 20;
		guiStyleVieAMI.normal.textColor = Color.red;

		guiStyleVieENEMI.fontSize = 20;
		guiStyleVieENEMI.normal.textColor = Color.green;
	}

	void Update(){
		if (!isLocalPlayer)
			return;

		if(Input.GetKey(KeyCode.Tab)){
			ShowBoard = true;
		}
		else
			ShowBoard = false;
	}

	void OnGUI(){
		GUILayout.BeginArea (new Rect (0, 0, 100, 100));
		/*foreach(Croiseur cr in NetworkManager.Instance.CroiseurList){
		 * //si même team 
			GUILayout.Label(cr.LifePoints, guiStyleVieAMI)

		   //si team enemie
			GUILayout.Label(cr.LifePoints, guiStyleVieENEMI);

		} */

		GUILayout.Label ("Vie1", guiStyleVieAMI); //A retirer au merge

		GUILayout.Label ("Vie2", guiStyleVieENEMI); //A retirer au merge
		GUILayout.EndArea ();

		if(ShowBoard){
			GUILayout.BeginArea(new Rect(Screen.width/4, Screen.height/4, (Screen.width) - (Screen.width/2), (Screen.height) - (Screen.height/2)),GUIContent.none,"box");
			/*foreach(Player pl in NetworkManager.Instance.PlayerList){
				GUILayout.BeginHorizontal();
				GUILayout.Label(pl.PlayerName);
				GUILayout.Label(pl.Kills.ToString());
				GUILayout.Label(pl.Death.ToString());
				GUILayout.Label(pl.Score.ToString());
				GUILayout.EndHorizontal();*/

			//TEAM RED
			GUILayout.BeginHorizontal();

			guiStyleTEAM.fontSize = 20;
			guiStyleTEAM.normal.textColor = Color.red;
			guiStyleTEAM.alignment = TextAnchor.MiddleCenter;
			GUILayout.Label("Team RED", guiStyleTEAM);
			//joueurs RED


			// ICI IL FAUT LES INFOS JOUEUR

			/*foreach (Player pl in NetworkManager.Instance.PlayerList) {
				if (pl.Team == "red") {
					GUILayout.BeginHorizontal ();
					GUILayout.Label (pl.PlayerName);
					GUILayout.Label(pl.Kills.ToString());
					GUILayout.Label(pl.Death.ToString());
					GUILayout.Label(pl.Score.ToString());
					GUILayout.EndHorizontal ();
				
				}
			}*/

			//fin joueurs RED

			GUILayout.EndHorizontal ();


			//TEAM BLUE
			GUILayout.BeginHorizontal ();

			guiStyleTEAM.normal.textColor = Color.blue;
			GUILayout.Label("Team BLUE", guiStyleTEAM);
			//joueurs BLUE

			/*	foreach (Player pl in NetworkManager.Instance.PlayerList) {
				if (pl.Team == "blue") {
					GUILayout.BeginHorizontal ();
					GUILayout.Label (pl.PlayerName);
					GUILayout.Label(pl.Kills.ToString());
					GUILayout.Label(pl.Death.ToString());
					GUILayout.Label(pl.Score.ToString());
					GUILayout.EndHorizontal ();

				}
			} */
			//fin joueurs BLUE

			GUILayout.EndHorizontal();


			GUILayout.EndArea();

		}
	}
}




