using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;
 using TMPro;

public class countdown : MonoBehaviour
{
    private float startTime=3f;
    private float currentTime;
    // public TextMeshProUGUI TextPro;
    public static bool resetTime;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime=startTime;
        resetTime=true;
        print("countdown start...");
        // gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(resetTime){
            gameObject.SetActive(true);
            resetTime=false;
            currentTime=startTime;
            print("reset time true");
        }
        currentTime-=1*Time.deltaTime;
        print("currentTime="+currentTime.ToString());
        if(currentTime<=0){
            gameObject.SetActive(false);
            print("hide countdown");
        }
        this.GetComponent<TextMeshProUGUI>().text=currentTime.ToString("0")+"s left before restart";
    }
}
