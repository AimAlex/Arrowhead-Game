using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBar : MonoBehaviour
{
    private Transform bar;
    private int hurt = 0;
    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool UpdateHurt()
    {
        hurt++;

        if (hurt == 1)
        {
            bar.localScale = new Vector3(.9f, 1f);

        }
        else if (hurt == 2)
        {
            bar.localScale = new Vector3(.8f, 1f);

        }
        else if (hurt == 3)
        {
            bar.localScale = new Vector3(.7f, 1f);

        }
        else if (hurt == 4)
        {
            bar.localScale = new Vector3(.6f, 1f);

        }
        else if (hurt == 5)
        {
            bar.localScale = new Vector3(.5f, 1f);

        }
        else if (hurt == 6)
        {
            bar.localScale = new Vector3(.4f, 1f);

        }
        else if (hurt == 7)
        {
            bar.localScale = new Vector3(.3f, 1f);

        }
        else if (hurt == 8)
        {
            bar.localScale = new Vector3(.2f, 1f);

        }
        else if (hurt == 9)
        {
            bar.localScale = new Vector3(.1f, 1f);

        }
        else if (hurt == 10)
        {
            bar.localScale = new Vector3(0, 1f);
            return false;

        }
        return true;
    }
}
