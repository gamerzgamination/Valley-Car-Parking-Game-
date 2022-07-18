using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InjectionListner : MonoBehaviour
{
    public Text Injectiontxt;
    public GameObject InjectionButton;
    // Start is called before the first frame update
    void OnEnable ()
    {
        DailyHealthInjectionTxtHandling();
    }
    void OnDisable()
    {
        StopCoroutine(CR_TimeHandling());
    }

    public void DailyHealthInjectionTxtHandling()
    {

        //if (DateTime.Now >= Toolbox.DB.Prefs.Dailyhealthinjectiontime)
        //{
        //    Toolbox.GameManager.Permanent_Log("Injection Ready");
        //    Injectiontxt.text = "Ready";
        //    InjectionButton.GetComponent<Button>().interactable = true;
        //}

        //else
        //{
        //    Toolbox.GameManager.Permanent_Log("Time ");
        //    InjectionButton.GetComponent<Button>().interactable = false;
        //    InjectionButton.GetComponent<Animator>().enabled =false;
        //    InjectionButton.GetComponent<Image>().color =Color.white;
        //    StartCoroutine(CR_TimeHandling());
        //}
    }
      IEnumerator CR_TimeHandling()
    {
        while (true)
        {
            Injectiontxt.text = Get_DailyHealthInjectionTimeString();
            yield return new WaitForSeconds(1);
        }
    }

    string Get_DailyHealthInjectionTimeString()
    {
        //TimeSpan diff = Toolbox.DB.Prefs.Dailyhealthinjectiontime - DateTime.Now;
        //int hours = diff.Hours;
        //hours += (diff.Days * 24);
        //return string.Format("{0}H {1}M {2}S", hours, diff.Minutes, diff.Seconds);
        return null;
    }

    public IEnumerator Set_InjectionTimeOnDelay()
    {
        yield return new WaitForSeconds(0.01f);
        //Toolbox.GameManager.Permanent_Log("Set_InjectionTimeOnDelay");
        //Toolbox.DB.Prefs.Dailyhealthinjectiontime = DateTime.Now.AddDays(1);
        //DailyHealthInjectionTxtHandling();
    }
    public void StartTimer()
    {
        StartCoroutine(Set_InjectionTimeOnDelay());
    }
}
