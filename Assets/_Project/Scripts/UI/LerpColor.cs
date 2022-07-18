using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpColor : MonoBehaviour
{
    public bool isText = true;
    public Color StartColor, EndColor;
    public float time = 1;
    Text t;
    Material m;
    void Start()
    {
        if (isText)
            t = GetComponent<Text>();
        else
            m = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        Color lerpedColor = Color.Lerp(StartColor, EndColor, Mathf.PingPong(Time.time, time));
        if (isText)
            t.color = lerpedColor;
        else
            m.color = lerpedColor;
    }
}
