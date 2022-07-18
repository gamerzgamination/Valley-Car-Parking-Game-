using UnityEngine;

public class Fift : MonoBehaviour
{
	public void animation_end()
	{
		Gmanager.gm.timer_barrier_cube_2();
	}

	public void level_18_start()
	{
		Toolbox.GameplayController.Selectedvehiclerigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}

	public void level_18_end()
	{
		Toolbox.GameplayController.Selectedvehiclerigidbody.constraints = RigidbodyConstraints.None;
		Toolbox.GameplayController.SelectedVehiclePrefab.transform.SetParent(null);
	}

	public void level_15_start()
	{
		Toolbox.GameplayController.Selectedvehiclerigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}

	public void level_15_end()
	{
		Toolbox.GameplayController.Selectedvehiclerigidbody.constraints = RigidbodyConstraints.None;
		Toolbox.GameplayController.SelectedVehiclePrefab.transform.SetParent(null);
	}

	public void lifter_stop()
	{
		//Gmanager.gm._to_stop_car();
		//this.gameObject.GetComponent<MeshCollider>().enabled = false;
		Toolbox.GameplayController.Selectedvehiclerigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}

	public void lifter_move()
	{
		//this.gameObject.GetComponent<MeshCollider>().enabled = true;
		Toolbox.GameplayController.Selectedvehiclerigidbody.constraints = RigidbodyConstraints.None;
		Toolbox.GameplayController.SelectedVehiclePrefab.transform.SetParent(null);
		//Gmanager.gm._to_move_car();
	}
}
