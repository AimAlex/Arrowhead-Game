using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;
 using TMPro;

public class countdown : MonoBehaviour
{
    private float startTime=5f;
    private float currentTime;
    private bool resetTime;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        currentTime=startTime;
        resetTime=true;
        // print("countdown start...");
        // gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(resetTime){
            timer=Time.realtimeSinceStartup;;
            gameObject.SetActive(true);
            resetTime=false;
            // print("reset time true");
        }
        currentTime=startTime-(Time.realtimeSinceStartup - timer);
        if(currentTime<=0){
        this.GetComponent<TextMeshProUGUI>().text="Go to next level after 0s";

        }else{
        this.GetComponent<TextMeshProUGUI>().text="Go to next level after "+currentTime.ToString("0")+"s";
        }
    }
}
