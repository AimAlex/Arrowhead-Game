using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;



public class AnalyticsScript : MonoBehaviour
{
    private string URL1="https://docs.google.com/forms/u/0/d/e/1FAIpQLSear_skIkyzM5-iBnyEDmQzrKk9aQXWe5iS7I1Fy2nHT2xbsA/formResponse";
    private string URL="https://docs.google.com/forms/u/0/d/e/1FAIpQLSe5TaZYcOrUZojFCUu9S-oPx66h3fgLw-MjGYd0gJyB6HHANg/formResponse";
    private string URL2="https://docs.google.com/forms/u/0/d/e/1FAIpQLSfzaDirqW_KWiGULACcU4JE4ZUhTjtI6lbJzaYb2aQLkSpt9w/formResponse";
    private long _sessionID;
    private int _numHints=0;
    private string success="NO";
    private string level;
    // private long _Ticks_collect1=0;
    // private long _Ticks_collect2=0;
    // private long _Ticks_collect3=0;
    // private long _Ticks_collect4=0;

     private int _killedByTrap=0;
     private int _restart=0;

     //  Added variables
     private int _killedByEnemy=0;
     private int _killedByTrap2=0;
     private int _killedByDeadzone=0;
     private int _wrongCollection=0;
    private long _endTicks;

    private string curScene;

    // Start is called before the first frame update
    void Start()
    {
        _sessionID=DateTime.Now.Ticks;
        curScene = SceneManager.GetActiveScene().name;
        level=curScene;
    }


    public void Success()
    {
        success="YES";
        Send();
        Send1();
    }

    public void KilledByTrap()
    {
        _killedByTrap += 1;
        _restart += 1;
        Send();
        _killedByTrap2 = 1;
        Send1();
        Send2();
    }

    public void KilledByDeadzone()
    {
        _killedByDeadzone = 1;
        _restart += 1;
        Send1();
        Send2();
    }

    public void KilledByEnemy()
    {
        _killedByEnemy = 1;
        _restart += 1;
        Send1();
        Send2();
    }

    public void WrongCollection()
    {
        _wrongCollection = 1;
        Send1();
        Send2();
    }

    public void Send1(){
        _endTicks = DateTime.Now.Ticks;
        StartCoroutine(Post1(_sessionID.ToString(), level, success, _killedByTrap2.ToString(), _killedByEnemy.ToString(), _killedByDeadzone.ToString(), _wrongCollection.ToString(), _endTicks.ToString(), _restart.ToString()));

    }

    public void Send()
    {
        // _endTicks=DateTime.Now.Ticks;
        StartCoroutine(Post(_sessionID.ToString(), _numHints.ToString(),success,level, _killedByTrap.ToString(), _restart.ToString()));
       
    }

    public void Send2()
    {
        _endTicks=DateTime.Now.Ticks;
        StartCoroutine(Post2(_sessionID.ToString(),level, _killedByTrap2.ToString(), _killedByEnemy.ToString(),_killedByDeadzone.ToString(),_wrongCollection.ToString(),_endTicks.ToString()));
       
    }

    private IEnumerator Post1(string sessionID, string level, string success, string killByTrap, string killedByEnemy, string killedByDeadzone, string wrongCollection, string endTicks, string restart)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.714224937", sessionID);
        form.AddField("entry.1153710383", success);
        form.AddField("entry.783274792", level);
        form.AddField("entry.1822068335", killByTrap);
        form.AddField("entry.1267997017", killedByEnemy);
        form.AddField("entry.2044875898", killedByDeadzone);
        form.AddField("entry.1198442690", wrongCollection);
        form.AddField("entry.799279765", endTicks);
        form.AddField("entry.1704069679", restart);

        using (UnityWebRequest www = UnityWebRequest.Post(URL1, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.error);
            }
            else
            {
                // Debug.Log("Form1 upload complete!");
            }
        }
    }

    private IEnumerator Post(string sessionID, string numHints, string success, string level, string killByTrap, string restart)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.28648695", sessionID);
        form.AddField("entry.359015480", numHints);
        form.AddField("entry.2002142994", success);
        form.AddField("entry.516466219", level);
        
        form.AddField("entry.1228259701", killByTrap);
        form.AddField("entry.397790580", restart);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.error);
            }
            else
            {
                // Debug.Log("Form upload complete!");
            }
        }
    }


    private IEnumerator Post2(string sessionID, string level, string killByTrap, string killedByEnemy, string killedByDeadzone,string wrongCollection,string endTicks){
        WWWForm form = new WWWForm();
        form.AddField("entry.245288514", sessionID);
        form.AddField("entry.2004691191", level);
        form.AddField("entry.377485916", killByTrap);
        form.AddField("entry.2115567755", killedByEnemy);
        form.AddField("entry.189172406", killedByDeadzone);
        form.AddField("entry.1724065906", wrongCollection);
        form.AddField("entry.647799259", endTicks);

        using (UnityWebRequest www = UnityWebRequest.Post(URL2, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.error);
            }
            else
            {
                // Debug.Log("Form2 upload complete!");
            }
        }
    }

}

    //     public void Collect1()
    // {
    //     _Ticks_collect1=DateTime.Now.Ticks;
    //     // Send();
    // }

    //     public void Collect2()
    // {
    //     _Ticks_collect2=DateTime.Now.Ticks;
    //     // Send();
    // }

    //     public void Collect3()
    // {
    //     _Ticks_collect3=DateTime.Now.Ticks;
    //     // Send();
    // }

    //         public void Collect4()
    // {
    //     _Ticks_collect4=DateTime.Now.Ticks;
    //     // Send();
    // }

    // Update is called once per frame
    // public void UpdateNumHints()
    // {
    //     _numHints += 1;
    // }