using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] public int levelNo;
    [SerializeField] public CustomArrays[] popUps;
    [SerializeField] public CustomArrays[] cameraTours;

    [SerializeField] public GameObject camera;
    // [SerializeField] public GameObject Background;
    public int trapCheck, pauseCheck;
    private int popUpIndex;
    private List<Vector3> tourList;
    private SideScrolling cameraScroll;
    private Vector3 currCameraPos;

    [System.Serializable]
    public class CustomArrays
    {
        public GameObject[] Array;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        popUpIndex = 0;
        pauseCheck = 1;
        cameraScroll = camera.GetComponent<SideScrolling>();
        tourList = new List<Vector3>();
        currCameraPos = camera.transform.position;
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
                CameraMove();
            }
            else if (popUpIndex == 1)
            {
                PauseUntilPress();
            } else if (popUpIndex == 3)
            {
                PauseUntilPress();
            } else if (popUpIndex == 5)
            {
                PauseUntilPress();
            } else if (popUpIndex == 7)
            {
                PauseUntilPress();
            }
            else if (popUpIndex == 8)
            {
                ItemTutorial();
            }
        } else if (levelNo == 1)
        {
            // if (popUpIndex == 0)
            // {
            //     PauseUntilPress();
                
            // }
            if(popUpIndex == 0)
            {
                DoubleJumpTutorial();
            }
            else if(popUpIndex == 1)
            {
                PauseUntilPress();
            }
            else if(popUpIndex == 3)
            {
                PauseUntilPress();
            }
            else if(popUpIndex == 5)
            {
                PauseUntilPress();
            }
        } else if (levelNo == 2)
        {
            if (popUpIndex == 0)
            {
                CameraMove();
            }
            else if (popUpIndex == 1)
            {
                PauseUntilPress();
            }
            else if (popUpIndex == 2)
            {
                DashTutorial();
            }
            else if (popUpIndex == 3)
            {
                PauseUntilPress();
            } else if (popUpIndex == 6)
            {
                PauseUntilPress();
            }
        } else if (levelNo == 3)
        {
            if (popUpIndex == 0)
            {
                PauseUntilPress();
            }
            else if (popUpIndex == 1)
            {
                ShootingTutorial();
            }
            else if (popUpIndex == 2)
            {
                PauseUntilPress();
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
            else if (popUpIndex == 2)
            {
                PauseUntilPress();
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
        if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            ++popUpIndex;
        }
    }

    void CameraMove()
    {
        if (pauseCheck == 1)
        {
            if (tourList.Count == 0)
            {
                currCameraPos = camera.transform.position;
                cameraScroll.camaraMove = true;
            }
            
            if (cameraTours.Length < popUpIndex + 1 || cameraTours[popUpIndex].Array.Length == 0)
            {
                ++popUpIndex;
                return;
            }

            tourList.Clear();
            
            // zoom and speed
            List<float> zoom = new List<float>();
            List<float> speed = new List<float>();
            
            foreach (var obj in cameraTours[popUpIndex].Array)
            {
                tourList.Add(obj.transform.position);
                zoom.Add(39.306593f);
                speed.Add(1);
            }
            tourList.Add(currCameraPos);
            zoom.Add(9.306593f);
            speed.Add(2);

            if (!cameraScroll.tourPosition(tourList, zoom, speed))
            {
                tourList.Clear();
                cameraScroll.pathIndex = 0;
                ++popUpIndex;
            }
        }
    }

    bool PauseUntilPress()
    {
        if (pauseCheck == 1)
        {
            // darkBackground();
            Time.timeScale = 0;
            pauseCheck = 0;
            return true;
        }

        return false;
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
            pauseCheck = 1;
        }
    }

    void DashTutorial()
    {
        if (PlayerMovement.collectDash)
        {
            ++popUpIndex;
            pauseCheck = 1;
        }
    }

    void ShootingTutorial()
    {
        if (Weapon.laserGunPickUp)
        {
            ++popUpIndex;
            pauseCheck = 1;
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
