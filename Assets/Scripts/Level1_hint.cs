using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_hint : MonoBehaviour
{
    public GameObject player;
    public GameObject hint1_panel;
    public GameObject hint2_panel;
    public GameObject hint3_panel;
    // Start is called before the first frame update
    void Start()
    {
        hint1_panel.SetActive(false);
        hint2_panel.SetActive(false);
        hint3_panel.SetActive(false);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void showhint()
    {
        float player_x = player.transform.position.x;
        if(player_x > -19 && player_x < 28)
        {
            // hint1_panel = GameObject.Find("Canvas/Hint1_panel");
            hint1_panel.SetActive(true);
            // hint2_panel.SetActive(false);
            // hint3_panel.SetActive(false);

            //Analytics codes
            FindObjectOfType<AnalyticsScript>.UpdateNumHints();
        }
        else if(player_x > 28 && player_x < 86)
        {
            // hint1_panel.SetActive(false);
            hint2_panel.SetActive(true);
            // hint3_panel.SetActive(false);

            //Analytics codes
            FindObjectOfType<AnalyticsScript>.UpdateNumHints();
        }
        else
        {
            hint3_panel.SetActive(true);

            //Analytics codes
            FindObjectOfType<AnalyticsScript>.UpdateNumHints();
        }
    }

    public void closehint()
    {
        float player_x = player.transform.position.x;
        if(player_x < 18)
        {
            // hint1_panel = GameObject.Find("Canvas/Hint1_panel");
            hint1_panel.SetActive(false);
            // hint2_panel.SetActive(false);
            // hint3_panel.SetActive(false);
        }
        else if(player_x < 100)
        {
            // hint1_panel.SetActive(false);
            hint2_panel.SetActive(false);
            // hint3_panel.SetActive(false);
        }
        else
        {
            hint3_panel.SetActive(false);
        }
    }

}
