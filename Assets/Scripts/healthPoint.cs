using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class healthPoint : MonoBehaviour
{
    public Image blood0;
    public Image blood1;
    public Image blood2;
    public Image blood3;
    public Image blood4;
    private int hurt=0;
    private PlayerMovement playerMovement;
    private AudioClip healthPointDownAudio;
    // Start is called before the first frame update
    void Start()
    {
        hurt=0;
        blood1.enabled=false;
        blood2.enabled=false;
        blood3.enabled=false;
        blood4.enabled=false;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        healthPointDownAudio = Resources.Load<AudioClip>("music/DM-CGS-30");
    }

    // Update is called once per frame
    public bool UpdateHurt()
    {
        hurt++;
        if (hurt != 4){
            playerMovement.PlayAudio(healthPointDownAudio);
        }
        if(hurt==1){
            blood0.enabled=false;
            blood1.enabled=true;
        }else if(hurt==2){
            blood1.enabled=false;
            blood2.enabled=true;
        }else if(hurt==3){
            blood2.enabled=false;
            blood3.enabled=true;
        }else if(hurt==4){
            blood3.enabled=false;
            blood4.enabled=true;
            return false;
        }
        return true;
    }
}
