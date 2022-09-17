using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;



public class AnalyticsScript : MonoBehaviour
{
    private string URL="https://docs.google.com/forms/u/0/d/e/1FAIpQLSfJ7BAQXGDj7dfM6hnzC8m66vb__tYQdJv93tpDl02j2G8PiA/formResponse";
    private long _sessionID;
    private int _numHints=0;
    private int _success=0;
    private int _killByTrap=0;
    private int _restart=0;
    private long _endTicks;
    // private double _usedMinutes=0;

    // Start is called before the first frame update
    void Start()
    {
        _sessionID=DateTime.Now.Ticks;
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

    public void Send()
    {
        _endTicks=DateTime.Now.Ticks;
        // TimeSpan elapsedSpan=new TimeSpan(_endTicks-_sessionID);
        // _usedMinutes=elapsedSpan.TotalMinutes;
        StartCoroutine(Post(_sessionID.ToString(),_numHints.ToString(),_success.ToString(),_killByTrap.ToString(),_restart.ToString(), _endTicks.ToString()));
        // Debug.Log(_sessionID);
        // Debug.Log(_numHints);
        // Debug.Log(_success);
        // Debug.Log(_killByTrap);
        // Debug.Log(_restart);
        // Debug.Log(_usedMinutes);
        // Debug.Log(_endTicks-_sessionID);

    }

    private IEnumerator Post(string sessionID, string numHints, string success, string killByTrap, string restart, string endTicks)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.941964954", sessionID);
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
