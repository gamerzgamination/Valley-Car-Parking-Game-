using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public float time, delay;
    Image img;
    Color Tempcolor;
    float delayTemp;
    public bool isOnce;









    ////[HideInInspector]
    //private Image painImageComponent;

    //void Start()
    //{
    //    painImageComponent = GetComponent<Image>();
    //    StartCoroutine(FadIn(Color.white, time));
    //}

    //public IEnumerator FadIn(Color color, float fadeLength)
    //{
    //    //Create a temporary Color var and make alpha of color = 0 (transparent for starting fade out)
    //    Color tempColor = color;
    //    tempColor.a = 0.0f;//store the color's alpha amount
    //    painImageComponent.color = tempColor;//set the guiTexture's color to the value of our temporary color var
    //    color.a = Mathf.Clamp01(color.a);
    //    print("FadIn");
    //    //Fade texture out
    //    float time = 0.0f;
    //    while (time < fadeLength * 3.0f)
    //    {
    //        time += Time.deltaTime * 1.15f;
    //        tempColor.a = Mathf.InverseLerp(fadeLength, 0.0f, time) * color.a;
    //        painImageComponent.color = tempColor;
    //        yield return null;
    //    }
    //    //StartCoroutine(DoFadeout(time));
    //    //StopCoroutine(FadIn(Color.white, time));
    //}

    //IEnumerator DoFadeout(float fadeLength)
    //{
    //	print("DoFadeout");

    //	float time = 0.0f;
    //	while (time < fadeLength * 3.0f)
    //	{
    //		      //Create a temporary Color var and make alpha of color = 0 (transparent for starting fade out)
    //			Color tempColor = painImageComponent.color;
    //			time += Time.deltaTime* 1.15f;
    //			tempColor.a = Mathf.InverseLerp(0.0f, fadeLength, time);//smoothly fade alpha in
    //			painImageComponent.color = tempColor;

    //		yield return null;
    //	}
    //	StartCoroutine(FadIn(Color.white, time));
    //	//StopCoroutine(DoFadeout(time));
    //}



    //Start is called before the first frame update

    void Start()
    {
        img = GetComponent<Image>();
      // ChangeValue(0);
        delayTemp = delay;
    }


    public void ChangeValue(float value)
    {
        GetComponent<Image>().fillAmount = value;
    }

    bool tempDone;
    // Update is called once per frame
    void Update()
    {
        if (!isOnce)
        {
            if (Tempcolor.a < 1 && delay > 0)
            {
                //  img.fillAmount += Time.deltaTime * time;
                Tempcolor.a = Mathf.InverseLerp(Tempcolor.a, 0.0f, time);
                img.color = Tempcolor;

            }
            else
            {
                delay -= Time.deltaTime;
                if (delay < 0)
                {
                    Tempcolor.a = Mathf.InverseLerp(0.0f,Tempcolor.a, time);
                    img.color = Tempcolor;
                    delay = delayTemp;
                }

            }
        }
        if (!tempDone)
        {
            if (img.fillAmount < 1 && delay > 0)
            {
                img.fillAmount += Time.deltaTime * time;
            }
            tempDone = true;
        }
    }
}
