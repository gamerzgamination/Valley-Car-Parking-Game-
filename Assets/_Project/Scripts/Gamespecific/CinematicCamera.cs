using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCamera : MonoBehaviour
{
    public GameObject cinematiccamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
           Toolbox.GameplayController.Selectedvehiclerigidbody.drag = 10f;
            Toolbox.GameplayController.Selectedvehiclerigidbody.constraints = RigidbodyConstraints.FreezeAll;
            Toolbox.HUDListner.Set_CinematicEffectstatus(true);
            Toolbox.GameplayController.HUD_Status(false);
            cinematiccamera.SetActive(true);
            Toolbox.GameplayController.Rcccamera.SetActive(false);
            Invoke(nameof(Originalcam),3f);
        }

    }
    private void Originalcam()
    {
        cinematiccamera.SetActive(false);
        Toolbox.GameplayController.Rcccamera.SetActive(true);
        Toolbox.HUDListner.Set_CinematicEffectstatus(false);
        Toolbox.GameplayController.HUD_Status(true);
        Toolbox.GameplayController.Selectedvehiclerigidbody.drag = 0.01f;
        Toolbox.GameplayController.Selectedvehiclerigidbody.constraints = RigidbodyConstraints.None;

    }
}
