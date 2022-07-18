using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModesData : MonoBehaviour
{
    public enum Mode {Day,Night };
     public Mode modes;
    public GameObject Environment;
    public Material skyboxes;
    public int TorchInput;
    // Start is called before the first frame update
    void Start()
    {
        switch (modes)
        {
            case Mode.Day:
                Constants.DayNight = 0;
                break;
            case Mode.Night:
                Constants.DayNight = 1;
                break;
        }
        Environment.SetActive(true);
        SetEnvironmentSkybox();
        //Toolbox.PlayerWeapons.TorchLight(TorchInput);
    }

    private void SetEnvironmentSkybox()
    {
        try
        {
            RenderSettings.skybox = skyboxes;
        }
        catch (System.Exception ex)
        {

            RenderSettings.skybox = skyboxes;

        }
    }
}
