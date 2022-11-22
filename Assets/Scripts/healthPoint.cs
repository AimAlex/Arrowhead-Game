using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class healthPoint : MonoBehaviour
{
    private int hurt=0;
    // private int health = 4;
    private PlayerMovement playerMovement;
    private AudioClip healthPointDownAudio;
    private healthbar h1;
    // Start is called before the first frame update
    void Start()
    {
        hurt=0;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        healthPointDownAudio = Resources.Load<AudioClip>("music/DM-CGS-30");
        if (GameObject.Find("Health Bar") != null){
            h1 = GameObject.Find("Health Bar").GetComponent<healthbar>();
        }
    }


    // Update is called once per frame
    public bool UpdateHurt()
    {
        hurt++;
        if (hurt != 4){
            playerMovement.PlayAudio(healthPointDownAudio);
        }
        if(hurt==1){
            h1.SetHealth(3);

        }
        else if(hurt==2){
            h1.SetHealth(2);

        }
        else if(hurt==3){
            h1.SetHealth(1);

        }
        else if(hurt==4){
            h1.SetHealth(0);

            return false;
        }
        return true;
    }
}
