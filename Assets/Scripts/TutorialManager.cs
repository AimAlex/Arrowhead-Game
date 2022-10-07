using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] public int popUpIndex;
    [SerializeField] public CustomArrays[] popUps;
    private float jumpForce;
    private int moveCount;
    private int jumpCheck, puzzleCheck, collectCheck;

    [System.Serializable]
    public class CustomArrays
    {
        public GameObject[] Array;
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpForce = PlayerMovement.jumpForce;
        moveCount = 0;
        jumpCheck = 0;
        puzzleCheck = 0;
        collectCheck = 0;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "JumpCheck")
        {
            jumpCheck = 1;
        } else if (col.gameObject.name == "PuzzleCheck")
        {
            puzzleCheck = 1;
        } else if (col.gameObject.name == "CollectCheck")
        {
            collectCheck = 1;
        }
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
        } else if (popUpIndex == 6)
        {
            DoubleJumpTutorial();
        }
    }

    void MoveTutorial()
    {
        PlayerMovement.jumpForce = 0;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (moveCount < 0)
            {
                ++popUpIndex;
                PlayerMovement.jumpForce = jumpForce;
            }
            else if (moveCount == 0)
            {
                ++moveCount;
            }
        } else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (moveCount > 0)
            {
                ++popUpIndex;
                PlayerMovement.jumpForce = jumpForce;
            }
            else if (moveCount == 0)
            {
                --moveCount;
            }
        }
    }

    void JumpTutorial()
    {
        if (jumpCheck == 1)
        {
            ++popUpIndex;
        }
    }

    void PuzzleTutorial()
    {
        if (puzzleCheck == 1)
        {
            ++popUpIndex;
        }
    }
    
    void CollectTutorial()
    {
        if (collectCheck == 1)
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

    void DoubleJumpTutorial()
    {
        if (PlayerMovement.collectDoubleJump)
        {
            ++popUpIndex;
        }
    }
}
