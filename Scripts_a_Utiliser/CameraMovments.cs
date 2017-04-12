using UnityEngine;
using System.Collections;

public class CameraMovments : MonoBehaviour {

	//variable pour camera 
	public Ray viewligne;
	public Vector2 SourisPos;
    protected bool suivreJoueur;
    protected bool straightFollow;

	//Variable de player 
	private Transform _transformplayer;
	private GameObject _player;
	private Vector3 _vaisseaupos;
    //vitesse de rotation utilisé dans la fonction lookVaisseau ( pour les loopings)
    private float speedLooping = 1.5f;
    //vitesse de rotation relative au suivi du vaisseau
    private float speedRotate = 0.05f;
    //Variable de marge de deplacement
    public float hauteur = 3f;
	public float distance = 50f;
	public float marge = 0.00010f;


	// Use this for initialization
	void Start () {

		//Recherche de joueur 
		_player = GameObject.FindGameObjectWithTag ("Player");
		_transformplayer = _player.transform;
        suivreJoueur = true;
        straightFollow = false;
	}
	
	// Update is called once per frame
	//Effectue une ligne entre la position de la souris et le centre de la camera 
	void Update () {
		viewligne = Camera.main.ScreenPointToRay (SourisPos);
	}

	void LateUpdate(){
        if(!suivreJoueur && !straightFollow)
		    lookVaisseau ();
        if (straightFollow && suivreJoueur)
            FollowStraight();
        if(suivreJoueur && !straightFollow)
            SmoothFollow();
	}

	void SmoothFollow(){
	//Le joueur suit le curseur avec delai

		//Position du vaisseau par rapport a la camera suivant les valeurs données 
		_vaisseaupos = _transformplayer.TransformPoint(0, hauteur , -distance);

		//Position camera 
		transform.position = Vector3.Lerp (transform.position, _vaisseaupos, 0.09f);

		//Rotation suivant la direction de devant (positon actuelle - inital) et d'au dessus (la position actuelle)
		Quaternion _vaisseaurotat = Quaternion.LookRotation(_transformplayer.position -  _vaisseaupos , _transformplayer.up);

        //Rotation camera 
		transform.rotation = Quaternion.Slerp(transform.rotation, _vaisseaurotat , speedRotate);

		}


    //  /!\ pour la suite, penser à incliner la caméra dans le bonne axe avant de bloquer les rotations
    void FollowStraight()
    {
        lookVaisseau();

        //Position du vaisseau par rapport a la camera suivant les valeurs données 
        _vaisseaupos = _transformplayer.TransformPoint(0, hauteur, -distance);

        //Position camera 
        transform.position = Vector3.Lerp(transform.position, _vaisseaupos, 0.09f);
    }

    void lookVaisseau()
    {
        //calcule la même chose que transform.LookAt(_transformplayer, transform.up); mais ne l'applique pas à l'objet
        Quaternion _vaisseaurotat = Quaternion.LookRotation(_transformplayer.position - _vaisseaupos, transform.up);
        //on applique la rotation nécessaire de façon "smooth"
        transform.rotation = Quaternion.Slerp(transform.rotation, _vaisseaurotat, speedLooping * Time.deltaTime);
    }

    void setSuivre(bool b)
    {
        suivreJoueur = b;
    }

    void setStraightFollow(bool b)
    {
        straightFollow = b;
    }
}
