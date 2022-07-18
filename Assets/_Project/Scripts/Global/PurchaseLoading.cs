using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseLoading : MonoBehaviour
{
    public Text Title;
    public Text Failed;
    public Text Cancel;
    public GameObject Spinner1;
    public GameObject Spinner2;


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Failed.gameObject.SetActive(false);
        Cancel.gameObject.SetActive(false);
        CancelInvoke();
    }
    public void FailedPurchase()
    {
        if (Toolbox.GameManager.ShowMegaOfferOnComplete)
        {
            Toolbox.GameManager.ShowMegaOfferOnComplete = false;
            return;
        }
        Title.text = "purchase processing . . . ".ToString();
        this.gameObject.SetActive(true);
        Failed.color = Color.red;
        Failed.text = "Purchase Failed !".ToString();
        Failed.gameObject.SetActive(true);
        Cancel.gameObject.SetActive(true);
        Spinner1.GetComponent<RotateAlways>().enabled = false;
        Spinner2.GetComponent<RotateAlways>().enabled = false;
        Invoke("Purchase_LoadingOf",3f);
    }
    public void completePurchase(string str)
    {
        if (Toolbox.GameManager.ShowMegaOfferOnComplete)
        {
            Toolbox.GameManager.ShowMegaOfferOnComplete = false;
            return;
        }
        Title.text = "purchase processing . . . ".ToString();
        this.gameObject.SetActive(true);
        Failed.gameObject.SetActive(true);
        Failed.color = Color.green;
        Failed.text = str.ToString();
        Spinner1.GetComponent<RotateAlways>().enabled = false;
        Spinner2.GetComponent<RotateAlways>().enabled = false;
        Invoke("Purchase_LoadingOf", 2f);
    }
    public void NotInitilizedPurchase(string str)
    {
        Title.text = "purchase processing . . . ".ToString();
        this.gameObject.SetActive(true);
        Failed.gameObject.SetActive(true);
        Failed.color = Color.red;
        Failed.text = str.ToString();
        Spinner1.GetComponent<RotateAlways>().enabled = false;
        Spinner2.GetComponent<RotateAlways>().enabled = false;
        Invoke("Purchase_LoadingOf", 1.5f);
    }
    public void InternetNotAvailabe()
    {
        Title.text = "purchase processing . . . ".ToString();
        this.gameObject.SetActive(true);
        Failed.gameObject.SetActive(true);
        Failed.color = Color.red;
        Failed.text = "Slow Internet!".ToString();
        Spinner1.GetComponent<RotateAlways>().enabled = false;
        Spinner2.GetComponent<RotateAlways>().enabled = false;
        Invoke("Purchase_LoadingOf", 1.5f);
    }
    public void Purchase_LoadingOf()
    {
        this.gameObject.SetActive(false);
        Spinner1.GetComponent<RotateAlways>().enabled = true;
        Spinner2.GetComponent<RotateAlways>().enabled = true;
    }

    public void AdLoading()
    {
        this.gameObject.SetActive(true);
        Title.text = "Ads Loading. . . ".ToString();
        Spinner1.GetComponent<RotateAlways>().enabled = true;
        Spinner2.GetComponent<RotateAlways>().enabled = true;
    }
}
