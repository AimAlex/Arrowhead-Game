using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;



public class AnalyticsScript : MonoBehaviour
{
    private string URL1="https://docs.google.com/forms/u/0/d/e/1FAIpQLSenoD2XZgSJYUWyKD6476Im4w61TmFIvpQY61F_M53KGcg1bw/formResponse";
    private string URL="https://docs.google.com/forms/u/0/d/e/1FAIpQLSfJ7BAQXGDj7dfM6hnzC8m66vb__tYQdJv93tpDl02j2G8PiA/formResponse";
    private long _sessionID;
    public int _level;
    private long _Ticks_collect1=0;
    private long _Ticks_collect2=0;
    private long _Ticks_collect3=0;
    private long _Ticks_collect4=0;
    private int _numHints=0;
    private int _success=0;
    private int _killByTrap=0;
    private int _restart=0;
    private long _endTicks;

    // Start is called before the first frame update
    void Start()
    {
        _sessionID=DateTime.Now.Ticks;
        Send1();
    }

        public void Collect1()
    {
        _Ticks_collect1=DateTime.Now.Ticks;
        // Send();
    }

        public void Collect2()
    {
        _Ticks_collect2=DateTime.Now.Ticks;
        // Send();
    }

        public void Collect3()
    {
        _Ticks_collect3=DateTime.Now.Ticks;
        // Send();
    }

            public void Collect4()
    {
        _Ticks_collect4=DateTime.Now.Ticks;
        // Send();
    }

    // Update is called once per frame
    public void UpdateNumHints()
    {
        _numHints++;
    }

    public void Success()
    {
        _success=1;
        Send();
    }

    public void KillByTrap()
    {
        _killByTrap=1;
        Send();
    }

    public void Restart(){
        _restart=1;
        Send();
    }

    public void Send1(){
        StartCoroutine(Post1(_sessionID.ToString(),_level.ToString()));

    }

    public void Send()
    {
        _endTicks=DateTime.Now.Ticks;
        StartCoroutine(Post(_sessionID.ToString(),_level.ToString(), _Ticks_collect1.ToString(), _Ticks_collect2.ToString(), _Ticks_collect3.ToString(), _Ticks_collect4.ToString(), _numHints.ToString(),_success.ToString(),_killByTrap.ToString(),_restart.ToString(), _endTicks.ToString()));
    }

    private IEnumerator Post1(string sessionID, string level){
        WWWForm form = new WWWForm();
        form.AddField("entry.1452716744", sessionID);
        form.AddField("entry.2028973347", level);

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

    private IEnumerator Post(string sessionID, string level, string collect1, string collect2, string collect3, string collect4, string numHints, string success, string killByTrap, string restart, string endTicks)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.941964954", sessionID);
        form.AddField("entry.1930068067", level);
        form.AddField("entry.643454633", collect1);
        form.AddField("entry.1800891819", collect2);
        form.AddField("entry.1396617648", collect3);
        form.AddField("entry.1101310629", collect4);
        form.AddField("entry.1590714955", numHints);
        form.AddField("entry.1182826110", success);
        form.AddField("entry.1818505519", killByTrap);
        form.AddField("entry.1080352547", restart);
        form.AddField("entry.1572406458", endTicks);

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

}
