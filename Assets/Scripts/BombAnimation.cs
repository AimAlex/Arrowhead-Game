using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAnimation : MonoBehaviour

{
    public bool isExplode = false;

    public bool second1 = false;

    public bool second2 = false;
    public bool idle = false;

    public static Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isExplode)
        {
            Debug.Log("explode");
            anim.SetBool("explode", true);
            anim.SetBool("bomb2", false);
            isExplode = false;
            

        }
        else if (second1)
        {
            Debug.Log("bomb1");
            anim.SetBool("bomb1", true);
            anim.SetBool("Idle", false);
            second1 = false;
        }
        else if (second2)
        {
            Debug.Log("bomb2");
            anim.SetBool("bomb2", true);
            anim.SetBool("bomb1", false);
            second2 = false;
        }
        else if (idle)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("explode", false);
            idle = false;
            Debug.Log("Idle");
        }
    }
}