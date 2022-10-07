using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;



public class AnalyticsScript : MonoBehaviour
{
    private string URL1="https://docs.google.com/forms/u/0/d/e/1FAIpQLSfQCOaBBMmCmLS9Rpsd9uacn7b_6p68T_0pBiFELfBHjI9YCQ/formResponse";
    private string URL="https://docs.google.com/forms/u/0/d/e/1FAIpQLSe5TaZYcOrUZojFCUu9S-oPx66h3fgLw-MjGYd0gJyB6HHANg/formResponse";
    private string URL2="https://docs.google.com/forms/u/0/d/e/1FAIpQLSfzaDirqW_KWiGULACcU4JE4ZUhTjtI6lbJzaYb2aQLkSpt9w/formResponse";
    private long _sessionID;
    private int _numHints=0;
    private string success="NO";
    public string level;
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

    // Start is called before the first frame update
    void Start()
    {
        _sessionID=DateTime.Now.Ticks;
        Send1();
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

    public void Success()
    {
        success="YES";
        Send();
    }

    public void KilledByTrap()
    {
        _killedByTrap += 1;
        _restart += 1;
        Send();
        _killedByTrap2 = 1;
        Send2();
    }

    public void KilledByDeadzone()
    {
        _killedByDeadzone = 1;
        _restart += 1;
        Send2();
    }

    public void KilledByEnemy()
    {
        _killedByEnemy = 1;
        _restart += 1;
        Send2();
    }

    public void WrongCollection()
    {
        _wrongCollection = 1;
        _restart += 1;
        Send2();
    }

    //public void Restart(){
         //_restart+=1;
        // Send();
    //}

    public void Send1(){
        StartCoroutine(Post1(_sessionID.ToString(),level));

    }

    public void Send()
    {
        // _endTicks=DateTime.Now.Ticks;
        // StartCoroutine(Post(_sessionID.ToString(),_level.ToString(), _Ticks_collect1.ToString(), _Ticks_collect2.ToString(), _Ticks_collect3.ToString(), _Ticks_collect4.ToString(), _numHints.ToString(),_success.ToString(),_killByTrap.ToString(),_restart.ToString(), _endTicks.ToString()));
        StartCoroutine(Post(_sessionID.ToString(), _numHints.ToString(),success,level, _killedByTrap.ToString(), _restart.ToString()));
       
    }

    public void Send2()
    {
        _endTicks=DateTime.Now.Ticks;
        // StartCoroutine(Post(_sessionID.ToString(),_level.ToString(), _Ticks_collect1.ToString(), _Ticks_collect2.ToString(), _Ticks_collect3.ToString(), _Ticks_collect4.ToString(), _numHints.ToString(),_success.ToString(),_killByTrap.ToString(),_restart.ToString(), _endTicks.ToString()));
        StartCoroutine(Post2(_sessionID.ToString(),level, _killedByTrap2.ToString(), _killedByEnemy.ToString(),_killedByDeadzone.ToString(),_wrongCollection.ToString(),_endTicks.ToString()));
       
    }

    private IEnumerator Post1(string sessionID, string level){
        WWWForm form = new WWWForm();
        form.AddField("entry.229747758", sessionID);
        form.AddField("entry.530952496", level);

        using (UnityWebRequest www = UnityWebRequest.Post(URL1, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form1 upload complete!");
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


        // form.AddField("entry.643454633", collect1);
        // form.AddField("entry.1800891819", collect2);
        // form.AddField("entry.1396617648", collect3);
        // form.AddField("entry.1101310629", collect4);
        // form.AddField("entry.1818505519", killByTrap);
        // form.AddField("entry.1080352547", restart);
        // form.AddField("entry.1572406458", endTicks);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
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
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form2 upload complete!");
            }
        }
    }

}
