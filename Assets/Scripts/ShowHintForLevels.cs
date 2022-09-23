using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class ShowHintForLevels : MonoBehaviour
{
    public GameObject player;
    public GameObject hint;
    // Start is called before the first frame update
    void Start()
    {
        hint.SetActive(false);
    }

    public void showhint()
    {
        hint.SetActive(true);
        FindObjectOfType<AnalyticsScript>().UpdateNumHints();
    }

    public void closehint()
    {
        hint.SetActive(false);
    }

}
