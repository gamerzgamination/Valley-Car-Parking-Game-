using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FPSFixingState
{
    None,
    MildFPSFixing,
    RigorousFPSFixing,
    ExtremeFPSFixing
}

public enum TextureSizes
{ 
    High = 0,
    Half = 1,
    Quater = 2,
    Eighth= 3
}

public delegate void LowFPS();

public class RenderSettingsManager : MonoBehaviour
{

    public string deviceModel;

    public static RenderSettingsManager instance;
    //public GameObject performancePopup;
    public float minimumMemory;
    public float machineScore;
    private int FramesPerSec;

    public float totalRam;
    public float minimumDeviceScore = 3;
    
    public LowFPS onLowFPS;
    

    public void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        DontDestroyOnLoad(this);
//#if UNITY_ANDROID
//        Application.targetFrameRate = 60;
//#endif

//#if UNITY_IOS
//        Application.targetFrameRate = 30;
//#endif
        this.CalculateSystemScore();
    }

    [ContextMenu("ChangesForLowFPS")]
    public void ChangesForLowFPS()
    {
        this.LowerTextureLimit();
    }

    public void LowerTextureLimit(TextureSizes textureSizes)
    {
        if (QualitySettings.masterTextureLimit < (int)TextureSizes.Eighth)
        {
            QualitySettings.masterTextureLimit = (int)textureSizes;
        }
        this.DisableAnisotropicTextureAndVSync();
    }

     [ContextMenu("Report LowMemory")]
    public void OnLowMemory()
    {
        this.OptimizeGameplay();
    }

    #region LegacyStuff
    // int memoryReportCount = 0;

    // public void ReportForLowMemory()
    // {
    //     if(ActivityIndicator.instance)
    //     {
    //     }
    ////     memoryReportCount++;

    // //    if(memoryReportCount>=2)
    // //    {
    // ////        Application.lowMemory -= this.ReportForLowMemory;
    // //    }
    // }
    public void SwitchingTurnOffCamera()
    {
        //if (GameManager.Instance)
        //{
        //    if (GameManager.Instance.mhud.isSecondaryCameraEnabled)
        //    {
        //        GameManager.Instance.mhud.ToggleSecondaryCameraFromHud(false);
        //        GameManager.Instance.mhud.ToggleSecondaryPerformance(true);
        //        GameManager.Instance.Toggle_SecondaryCamera(false);
        //        GameManager.Instance.mhud.isSecondaryCamActive = false;
        //    }
        //}
    }

    public void ChangeCamera(float value)
    {
        //if(GameManager.Instance)
        //GameManager.Instance.playerCam.GetComponent<Camera>().farClipPlane = value;
    }

    //[ContextMenu("ChangesForLowFPS")]
    //public void ChangesForLowFPS()
    //{

    //    //this.SwitchingTurnOffCamera();
    //    this.lowFPSTimer = 0;

    //    if (this.FPSState.Equals(FPSFixingState.ExtremeFPSFixing))
    //        return;

    //    switch (this.FPSState)
    //    {
    //        case FPSFixingState.None:
    //            //   this.ChangeCamera(30f);
    //            this.FPSState = FPSFixingState.MildFPSFixing;

    //            break;

    //        case FPSFixingState.MildFPSFixing:

    //            //if (this.renderSettingsCanvas)
    //            //    this.renderSettingsCanvas.gameObject.SetActive(true);

    //            this.FPSState = FPSFixingState.RigorousFPSFixing;
    //            //this.ChangeCamera(25f);
    //            //this.OnLowMemory();
    //            //Invoke("LowerTextureLimit", 0.5f);
    //            ActivityIndicator.instance.PrintStackTrace("Rigorous FPS Fixing");
    //            //       this.LowerTextureLimit();

    //            break;

    //        case FPSFixingState.RigorousFPSFixing:

    //            //if (this.renderSettingsCanvas)
    //            //    this.renderSettingsCanvas.gameObject.SetActive(true);

    //            this.FPSState = FPSFixingState.ExtremeFPSFixing;
    //            //this.ChangeCamera(20f);
    //            //Invoke("LowerTextureLimit", 0.5f);
    //            ActivityIndicator.instance.PrintStackTrace("Extreme FPS Fixing");
    //            //  this.LowerTextureLimit();
    //            break;
    //    }
    //    Constant.LogDesignEvent("OptimizationLevel:" + this.FPSState);
    //}
    public void ResizeTexture(Texture2D texture)
    {
        //int width = texture.width;
        //int height = texture.height;

        //if (texture.isReadable)
        //{
        //    texture.Resize(width / 2, height / 2);
        //    texture.Apply();

        //}
    }

    //public void OnLowMemory()
    //{
    //    this.LowerTextureLimit();
    //    //this.SwitchingTurnOffCamera();
    //    //   Constant.LogDesignEvent("RenderSettings:LowMemory:OptimizingMemory");
    //    //bool textureResolutionLowered = false;
    //    //if (TextureCarrier.instance)
    //    //{
    //    //    TextureCarrier.instance.AssignCommonSharedMaterials(ref textureResolutionLowered);
    //    //}

    //    //if (QualitySettings.anisotropicFiltering != AnisotropicFiltering.Disable)
    //    //    QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;

    //    //if (!textureResolutionLowered)
    //    //    Resources.UnloadUnusedAssets();
    //}
    #endregion


    public void DisableAnisotropicTextureAndVSync()
    {
        if (QualitySettings.anisotropicFiltering != AnisotropicFiltering.Disable)
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        }

        if (QualitySettings.vSyncCount > 0)
            QualitySettings.vSyncCount = 0;
    }

    public void LowerTextureLimit()
    {
        if (QualitySettings.masterTextureLimit < (int)TextureSizes.Eighth)
        {
            QualitySettings.masterTextureLimit = QualitySettings.masterTextureLimit + 1;
        }
        this.DisableAnisotropicTextureAndVSync();
    }
    bool isLowEndDevice = false;
    bool OptimizationMessageGiven = false;
    [ContextMenu("Optimize")]
    public void OptimizeGameplay()
    {
        isLowEndDevice = true;
        this.LowerTextureLimit(TextureSizes.Eighth);

        //Constant.explicitPlayerCameraFar = 15f;
        //Constant.explicitSecondaryCameraFar = 10f;

        if (!OptimizationMessageGiven)
        {
            OptimizationMessageGiven = true;
            Debug.Log("Splash:LowSpecsDevice:DeviceMemory:" + this.totalRam);
           // Toolbox.GameManager.ShowMessage("Optimizing Graphics Of Game :+", "Low Specs Device");
        }
    }

    public void CalculateSystemScore()
    {
        this.deviceModel = SystemInfo.deviceModel;
        this.totalRam = SystemInfo.systemMemorySize/1000f;
       
        Debug.Log("GraphicsMemorySize :" + SystemInfo.graphicsMemorySize);
        Debug.Log("DeviceModel :"+ deviceModel);
       // Debug.Log("SystemInfo.systemMemorySize: " + SystemInfo.systemMemorySize);
        Debug.Log("Total Ram: " + totalRam);
        
        float memoryScore = totalRam / this.minimumMemory;

        Debug.Log("Total MemoryScore: " + memoryScore);

        this.machineScore = memoryScore;

        if (this.machineScore <= this.minimumDeviceScore)
        {
            this.OptimizeGameplay();
        }

    }
}
