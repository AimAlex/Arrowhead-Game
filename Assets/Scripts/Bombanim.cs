using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombanim : MonoBehaviour

{
    public bool isExplode = false;

    public bool second1 = false;

    public bool second2 = false;
    public bool idle = true;

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
            anim.SetBool("explode", true);
        }
        else if (second1)
        {
            anim.SetBool("bomb1", true);
        }
        else if (second2)
        {
            anim.SetBool("bomb2", true);
        }
        else if (idle)
        {
            anim.SetBool("Idle", true);
        }
    }
}
