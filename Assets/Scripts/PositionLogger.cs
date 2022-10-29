using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;
 using TMPro;
 
 public class PositionLogger : MonoBehaviour {
    //  public GameObject myCube;
    //  public Text positionText;
    public TextMeshProUGUI TextPro;
    private string x="-13.1";
    private string y="-8.8";
     // Use this for initialization
     void Start () {
         
     }
     
     // Update is called once per frame
     void Update () {
        x=(Mathf.Round(this.transform.position.x*100.0f)*0.01f).ToString();
        y=(Mathf.Round(this.transform.position.y*100.0f)*0.01f).ToString();
        TextPro.text=x+" "+y;
    //      if (Input.GetKeyDown ("w")) {
    //          myCube.transform.position += transform.forward;
    //      } else if (Input.GetKeyDown ("s")) {
    //          myCube.transform.position -= transform.forward;
    //      } else if (Input.GetKeyDown ("d")) {
    //          myCube.transform.position += transform.right;
    //      } else if (Input.GetKeyDown ("a")) {
    //          myCube.transform.position -= transform.right;
    //      }
    //      LogMyPosition ();
    //  }
    //  public void LogMyPosition(){
    //      myText.text = "MY POSITION IS: " + myCube.transform.position;
     }
 }