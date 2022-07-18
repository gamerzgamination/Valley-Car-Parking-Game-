using UnityEngine;
using UnityEngine.UI;

public class RCC_Useless : MonoBehaviour
{
	public enum Useless
	{
		Controller,
		Behavior
	}

	public Useless useless;

	private void Awake()
	{
		int value = 0;
		if (useless == Useless.Behavior)
		{
			switch (RCC_Settings.Instance.behaviorType)
			{
			case RCC_Settings.BehaviorType.Simulator:
				value = 0;
				break;
			case RCC_Settings.BehaviorType.Racing:
				value = 1;
				break;
			case RCC_Settings.BehaviorType.SemiArcade:
				value = 2;
				break;
			case RCC_Settings.BehaviorType.Drift:
				value = 3;
				break;
			case RCC_Settings.BehaviorType.Fun:
				value = 4;
				break;
			case RCC_Settings.BehaviorType.Custom:
				value = 5;
				break;
			}
		}
		else
		{
			if (!RCC_Settings.Instance.useAccelerometerForSteering && !RCC_Settings.Instance.useSteeringWheelForSteering)
			{
				value = 0;
			}
			if (RCC_Settings.Instance.useAccelerometerForSteering)
			{
				value = 1;
			}
			if (RCC_Settings.Instance.useSteeringWheelForSteering)
			{
				value = 2;
			}
		}
		GetComponent<Dropdown>().value = value;
		GetComponent<Dropdown>().RefreshShownValue();
	}
}
