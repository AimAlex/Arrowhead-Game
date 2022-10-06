using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] public CustomArrays[] popUps;
    private int popUpIndex;


    [System.Serializable]
    public class CustomArrays
    {
        public GameObject[] Array;
    }

    // Start is called before the first frame update
    void Start()
    {
        popUpIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < popUps.Length; ++i)
        {
            if (i == popUpIndex)
            {
                foreach (var popUp in popUps[i].Array)
                {
                    popUp.SetActive(true);
                }
                break;
            }

            foreach (var popUp in popUps[i].Array)
            {
                popUp.SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            MoveTutorial();
        } else if (popUpIndex == 1)
        {
            JumpTutorial();
        } else if (popUpIndex == 2)
        {
            PuzzleTutorial();
        } else if (popUpIndex == 3)
        {
            CollectTutorial();
        } else if (popUpIndex == 4)
        {
            ItemTutorial();
        }
    }

    void MoveTutorial()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            ++popUpIndex;
        }
    }

    void JumpTutorial()
    {
        if (Input.GetButtonDown("Jump"))
        {
            ++popUpIndex;
        }
    }

    void PuzzleTutorial()
    {
        if (EventSystem.current.currentSelectedGameObject &&
            EventSystem.current.currentSelectedGameObject.name == "Button_showhint")
        {
            ++popUpIndex;
        }
    }
    
    void CollectTutorial()
    {
        if (EventSystem.current.currentSelectedGameObject &&
            EventSystem.current.currentSelectedGameObject.name == "Button_start")
        {
            ++popUpIndex;
        }
    }

    void ItemTutorial()
    {
        if (itemCollect.bagStack.Count > 0)
        {
            ++popUpIndex;
        }
    }
}
