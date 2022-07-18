using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicleselection : MonoBehaviour
{
    public MeshRenderer Spoilerren;
    public List<Material> Playersmaterials;
    private Material mat;
    private MeshRenderer ren;
    void Start()
    {
        ren = GetComponent<MeshRenderer>();
        ren.materials[6].CopyPropertiesFromMaterial(Playersmaterials[Toolbox.DB.Prefs.LastSelectedVehicle]);
        Spoilerren.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(Playersmaterials[Toolbox.DB.Prefs.LastSelectedVehicle]);
       
        print("Name :" + ren.sharedMaterials.Length);
        print("Name :"+ren.materials[6]);
        print("Name :" + Playersmaterials[Toolbox.DB.Prefs.LastSelectedVehicle]);
    }

  
}
