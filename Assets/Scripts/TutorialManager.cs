using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] public int levelNo;
    [SerializeField] public CustomArrays[] popUps;
    // [SerializeField] public GameObject Background;
    private int trapCheck, pauseCheck;
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
        pauseCheck = 1;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "TrapCheck")
        {
            trapCheck = 1;
        } else if (col.gameObject.CompareTag("CheckPoint"))
        {
            ++popUpIndex;
            Destroy(col.gameObject);
        } else if (col.gameObject.CompareTag("PausePoint"))
        {
            ++popUpIndex;
            pauseCheck = 1;
            Destroy(col.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePopUp();
        CheckPause();
        if (levelNo == 0)
        {
            if (popUpIndex == 0)
            {
                PauseUntilPress();
            } else if (popUpIndex == 2)
            {
                PauseUntilPress();
            } else if (popUpIndex == 4)
            {
                PauseUntilPress();
            } else if (popUpIndex == 5)
            {
                ItemTutorial();
            }
        } else if (levelNo == 1)
        {
            if (popUpIndex == 0)
            {
                DoubleJumpTutorial();
            }
        } else if (levelNo == 2)
        {
            
        } else if (levelNo == 3)
        {
            if (popUpIndex == 0)
            {
                ShootingTutorial();
            }
        } else if (levelNo == 4)
        {
            if (popUpIndex == 0)
            {
                TrapTutorial();
            } else if (popUpIndex == 1)
            {
                BombTutorial();
            }
        }
    }

    void UpdatePopUp()
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
    }

    void CheckPause()
    {
        if (Time.timeScale == 0 && Input.anyKeyDown)
        {
            Time.timeScale = 1;
            ++popUpIndex;
        }
    }

    void PauseUntilPress()
    {
        if (pauseCheck == 1)
        {
            // darkBackground();
            Time.timeScale = 0;
            pauseCheck = 0;
        }
    }

    // void darkBackground()
    // {
    //     foreach (Transform child in Background.transform.GetComponentInChildren<Transform>())
    //     {
    //         var renderer = child.GetComponent<Renderer>();
    //         renderer.material.color = new Color((float)(renderer.material.color.r * 0.9), (float)(renderer.material.color.g * 0.9), (float)(renderer.material.color.b * 0.9));
    //     }
    // }

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

    void ShootingTutorial()
    {
        if (Weapon.laserGunPickUp)
        {
            ++popUpIndex;
        }
    }

    void TrapTutorial()
    {
        if (trapCheck == 1)
        {
            ++popUpIndex;
        }
    }

    void BombTutorial()
    {
        if (Pickupbomb.collectBomb)
        {
            ++popUpIndex;
        }
    }
}
