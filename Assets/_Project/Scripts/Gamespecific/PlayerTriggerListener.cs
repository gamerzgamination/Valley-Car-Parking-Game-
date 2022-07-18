using UnityEngine;

public class PlayerTriggerListener : MonoBehaviour
{
    // [HideInInspector]
    public Transform Lastsavepoint;
    public int Totalcoins;
    public GameObject orbit_camera;
    //private HandleTyreGrip tyregrip ; 
    private void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Obstacle")
        {
            print("Name :" + collision.gameObject.name);
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.CarHitAlarm);
            collision.gameObject.GetComponent<MeshRenderer>().material = Toolbox.GameplayController.Redmaterial;
            GetComponent<Rigidbody>().isKinematic = true;
            Handheld.Vibrate();
            Toolbox.GameplayController.LevelFail_Delay(2f);
        }
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Obstacle")
        {
            print("Name :" + collision.gameObject.name);
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.CarHitAlarm);
            collision.gameObject.GetComponent<MeshRenderer>().material = Toolbox.GameplayController.Redmaterial;
            GetComponent<Rigidbody>().isKinematic = true;
            Handheld.Vibrate();
            Toolbox.GameplayController.LevelFail_Delay(2f);
        }
        if (collision.gameObject.tag == "Yellow Destination Point" || collision.gameObject.tag == "Yellow Destination Point" || collision.gameObject.tag == "Yellow Destination Point")
        {
            orbit_camera.GetComponent<ob>().autoRotateOn = true;
            GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.levelComplete);
            Toolbox.GameplayController.StartCoroutine(Toolbox.GameplayController.LevelComplete_Delay(3f));
        }
    }





    void OnTriggerEnter(Collider col)
    {
        //if (col.gameObject.tag == "checkpoint")
        //{

        //    Lastsavepoint = col.gameObject.transform;
        //    col.gameObject.SetActive(false);
        //}
        //if (col.gameObject.tag == "coins")
        //{
        //    Totalcoins++;
        //    Toolbox.DB.Prefs.GoldCoins = Totalcoins;
        //    Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.singleCoinsSound);
        //    col.gameObject.SetActive(false);
        //}

        //if (col.gameObject.CompareTag("ep"))
        //{
        //    col.gameObject.GetComponent<MeshRenderer>().material = Toolbox.GameplayController.FadedMaterial;
        //}
        if (col.gameObject.tag == "Yellow Destination Point" || col.gameObject.tag == "Yellow Destination Point" || col.gameObject.tag == "Yellow Destination Point")
        {
            orbit_camera.GetComponent<ob>().autoRotateOn = true;
            GetComponent<Rigidbody>().isKinematic = true;
            col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.levelComplete);
            Toolbox.GameplayController.StartCoroutine(Toolbox.GameplayController.LevelComplete_Delay(3f));
        }
        if (col.gameObject.CompareTag("Pole"))
        {
            for (int i = 0; i < col.transform.childCount; i++)
            {
                col.transform.GetChild(i).gameObject.GetComponentInParent<Animator>().enabled = true;
                 print("Name :" + col.transform.GetChild(i).gameObject.name);
            }
            //Toolbox.GameplayController.SelectedVehiclePrefab.transform.SetParent(col.gameObject.GetComponentInParent<Animator>().transform);
        }
        if (col.gameObject.CompareTag("upwards")) 
        {
            print("Name :" + col.gameObject.name);
            col.gameObject.GetComponentInParent<Animator>().enabled = true;
        }
        if (col.gameObject.CompareTag("cargolift")) 
        {
             print("Name :" + col.gameObject.name);
            col.gameObject.GetComponentInParent<Animator>().enabled = true;
            ob.obi.distance = 2.5f;
            ob.obi.rotationXAxis = 30f;
            //Toolbox.GameplayController.SelectedVehiclePrefab.transform.SetParent(col.gameObject.transform);
        }
        if (col.gameObject.CompareTag("container"))
        {
             print("Name :" + col.gameObject.name);
            col.gameObject.GetComponent<MeshRenderer>().material = Toolbox.GameplayController.FadedMaterial;
            //  col.gameObject.GetComponentInParent<Animator>().enabled = true;
            Toolbox.GameplayController.SelectedVehiclePrefab.transform.SetParent(col.gameObject.transform);
        }
        //if (col.gameObject.CompareTag("downwards") )
        //{
        //    print("Name :" + col.gameObject.name);
        //    col.gameObject.GetComponentInParent<Animator>().enabled = true;
        //    Toolbox.GameplayController.SelectedVehiclePrefab.transform.SetParent(col.gameObject.GetComponentInParent<Animator>().transform);
        //}

        //if (col.gameObject.CompareTag("forward"))
        //{
        //    print("Name :" + col.gameObject.name);
        //    col.gameObject.GetComponentInParent<Animator>().enabled = true;
        //    Toolbox.GameplayController.SelectedVehiclePrefab.transform.SetParent(col.gameObject.GetComponentInParent<Animator>().transform);
        //}

        //if (col.gameObject.CompareTag("sidelift"))
        //{
        //    print("Name :" + col.gameObject.name);
        //    col.gameObject.GetComponentInParent<Animator>().enabled = true;
        //    Toolbox.GameplayController.SelectedVehiclePrefab.transform.SetParent(col.gameObject.GetComponentInParent<Animator>().transform);
        //}

        //if (col.gameObject.CompareTag("base"))
        //{
        //    col.gameObject.SetActive(false);
        //    ob.obi.distance = 2.5f;
        //    ob.obi.rotationXAxis = 50f;
        //}

        //if (col.gameObject.CompareTag("gate"))
        //{
        //    col.gameObject.GetComponentInParent<Animator>().enabled = true;
        //}
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("cargolift"))
        {
            print("Name :" + col.gameObject.name);
            col.gameObject.GetComponentInParent<Animator>().enabled = true;
            ob.obi.distance = 7f;
            ob.obi.rotationXAxis = 30f;
            Toolbox.GameplayController.SelectedVehiclePrefab.transform.SetParent(col.gameObject.transform);
        }
    }

    public void set_StatusVehicleReset()
    {
        if (Lastsavepoint)
        {
            this.transform.position = Lastsavepoint.position;
            this.transform.rotation = Lastsavepoint.rotation;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //this.GetComponent<HandleTyreGrip>().enabled = false;
        }
        else
        {

        }

    }

    
}
