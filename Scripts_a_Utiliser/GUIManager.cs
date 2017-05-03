using UnityEngine;
using System.Collections;


public class GUIManager : MonoBehaviour {

	private GUIStyle guiStyleTEAM = new GUIStyle();
	private GUIStyle guiStylePLAYER = new GUIStyle();

	
	public static GUIManager Instance;
	public bool ShowBoard;



	void Start(){
		Instance = this;
	}
	
	void Update(){
		//if(Input.GetKeyDown(KeyCode.Tab))
		//	ShowBoard = !ShowBoard;
	
		if(Input.GetKey(KeyCode.Tab)){
			ShowBoard = true;
		}
		else
			ShowBoard = false;
	}
	
	void OnGUI(){
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
	
	


