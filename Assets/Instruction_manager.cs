using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction_manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject instructionObj;
    // private bool instrShow;
    void Start()
    {
        // instrShow = false;
        instructionObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            setIntr();
        }
    }

    public void setIntr()
    {
        if (instructionObj.activeInHierarchy)
        {
            instructionObj.SetActive(false);
            Time.timeScale = 1;
        }
        else if (Time.timeScale != 0)
        {
            instructionObj.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
}
