using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CommonMethods : MonoBehaviour
{
    [Header("Values")]
    public float runAfterTime = 0;

    [Header("Events")]
    public UnityEvent OnStart;

    private void Start()
    {
        if (OnStart == null)
            OnStart = new UnityEvent();

        StartCoroutine(FireEvent());
    }

    public void LoadLevelWithLoading(int _index) {

        Toolbox.GameManager.LoadLevel(_index, true);
    }
    public void LoadLevelWithoutLoading(int _index)
    {
        Toolbox.GameManager.LoadLevel(_index, false);
    }

    IEnumerator FireEvent() {

        yield return new WaitForSeconds(runAfterTime);

        OnStart.Invoke();
    }

    public void InstantiateFromResources(string _path)
    {
        Instantiate(Resources.Load(_path), Vector3.zero, Quaternion.identity);
    }

    public void DestroyCurrentObject() {

        Destroy(this.gameObject);
    }

    public void Blink() {

      //  Toolbox.GameManager.InstantiateUI_Blink();
    }

    public void MuteAudioSource_IfDefaultIsMute()
    {

        if (Toolbox.DB.Prefs.SoundVolume <= 0)
            this.GetComponent<AudioSource>().volume = 0;
    }

    #region Game Specific Helping Functions

    public void ExtrFunction_AssignCameraCinematic (GameObject _obj) {
        
    }

    public void ExtraFunction_GasForCarStartAnimation() {

        //Debug.LogError("STARTED");
        //HUDListner.gasInput = 0.4f;
        //Toolbox.GameplayController.SpawnedVehicle.transform.parent = null;
    }

    //public void HUDBUTTONSTATUS_SkipStartAnimation(bool _val)
    //{
    //    Toolbox.HUDListner.SetStatus_SkipAnimationButton(_val);
    //}

    public void EnableLevelStartAnimation() {

   //     Toolbox.GameplayController.VehicleSpawnPoint.GetComponentInParent<Animator>().enabled = true;
    }


    #endregion
}
