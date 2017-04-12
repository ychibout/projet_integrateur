using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    //Variable de speed
    public float Speed;
    public float SpeedMax;
    public float SpeedMin;

    public float tempsLooping = 0.1f;
    public GameObject camera;
    protected Vector3 posCamera;
    protected Quaternion rotCamera;

    protected bool isLooping ; //vrai uniquement lors d'un looping vertical
    protected bool isLoopingH; //vrai uniquement lors d'un looping horizontal

    float progression = 0, valRotation = 0;
    // Use this for initialization
    void Start()
    {
        isLooping = false;
        isLoopingH = false;
}

    // Update is called once per frame
    void LateUpdate()
    {
        //Cas du looping "vertical", ->la camera ne bouge plus
        if (Input.GetKeyDown("space") && isLooping == false && isLoopingH== false)
        {
            isLooping = true;
            camera.SendMessage("setSuivre", false);
        }

        //cas du looping "horizontal" ->la camera ne ne fait plus de rotation
        if(Input.GetKeyDown("left ctrl") && isLoopingH == false && isLooping==false)
        {
            isLoopingH = true;
            camera.SendMessage("setStraightFollow", true);
        }

        if (!isLooping && !isLoopingH)
        {
            //Recupere la postion de la souris 
            Vector3 MvtSouris = (Input.mousePosition - (new Vector3(Screen.width, Screen.height, 0) / 2f));


            //Rotate suivant la position de la souris 
            transform.Rotate(new Vector3(-MvtSouris.y, MvtSouris.x, 0) * 0.010f);
        }

        //looping vertical
        if (isLooping)
        {
            valRotation += (Time.deltaTime * 360f) / tempsLooping;
            progression += valRotation;
            transform.Rotate(valRotation, 0, 0, Space.Self);
            if (progression >= 360f)
            {
                valRotation = progression = 0;
                isLooping = false;
                camera.SendMessage("setSuivre", true);
            }
        }

        if (isLoopingH)
        {
            valRotation += (Time.deltaTime * 360f) / tempsLooping;
            progression += valRotation;
            transform.Rotate(0, 0, valRotation, Space.Self);
            if (progression >= 360f)
            {
                valRotation = progression = 0;
                isLoopingH = false;
                camera.SendMessage("setStraightFollow", false);
            }
        }


        //avance tout le temps a une vitesse donné
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);

    }
}