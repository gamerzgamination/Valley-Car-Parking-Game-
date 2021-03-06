using System;
using UnityEngine;

[Serializable]
public class RCC_ChangableWheels : ScriptableObject
{
	[Serializable]
	public class ChangableWheels
	{
		public GameObject wheel;
	}

	public static RCC_ChangableWheels instance;

	public ChangableWheels[] wheels;

	public static RCC_ChangableWheels Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load("RCCAssets/RCC_ChangableWheels") as RCC_ChangableWheels;
			}
			return instance;
		}
	}
}
