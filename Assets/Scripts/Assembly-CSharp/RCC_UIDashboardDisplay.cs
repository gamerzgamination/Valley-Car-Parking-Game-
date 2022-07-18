using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Dashboard Displayer")]
[RequireComponent(typeof(RCC_DashboardInputs))]
public class RCC_UIDashboardDisplay : MonoBehaviour
{
	private RCC_Settings RCCSettingsInstance;

	private RCC_DashboardInputs inputs;

	public Text RPMLabel;

	public Text KMHLabel;

	public Text GearLabel;

	public Image ABS;

	public Image ESP;

	public Image Park;

	public Image Headlights;

	public Image leftIndicator;

	public Image rightIndicator;

	private RCC_Settings RCCSettings
	{
		get
		{
			if (RCCSettingsInstance == null)
			{
				RCCSettingsInstance = RCC_Settings.Instance;
			}
			return RCCSettingsInstance;
		}
	}

	private void Start()
	{
		inputs = GetComponent<RCC_DashboardInputs>();
		StartCoroutine("LateDisplay");
	}

	private void OnEnable()
	{
		StopAllCoroutines();
		StartCoroutine("LateDisplay");
	}

	private IEnumerator LateDisplay()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.04f);
			if ((bool)RPMLabel)
			{
				RPMLabel.text = inputs.RPM.ToString("0");
			}
			if ((bool)KMHLabel)
			{
				if (RCCSettings.units == RCC_Settings.Units.KMH)
				{
					KMHLabel.text = inputs.KMH.ToString("0") + "\nKMH";
				}
				else
				{
					KMHLabel.text = (inputs.KMH * 0.62f).ToString("0") + "\nMPH";
				}
			}
			if ((bool)GearLabel)
			{
				if (!inputs.NGear && !inputs.changingGear)
				{
					GearLabel.text = ((inputs.direction != 1) ? "R" : (inputs.Gear + 1f).ToString("0"));
				}
				else
				{
					GearLabel.text = "N";
				}
			}
			if ((bool)ABS)
			{
				ABS.color = ((!inputs.ABS) ? Color.white : Color.red);
			}
			if ((bool)ESP)
			{
				ESP.color = ((!inputs.ESP) ? Color.white : Color.red);
			}
			if ((bool)Park)
			{
				Park.color = ((!inputs.Park) ? Color.white : Color.red);
			}
			if ((bool)Headlights)
			{
				Headlights.color = ((!inputs.Headlights) ? Color.white : Color.green);
			}
			if ((bool)leftIndicator && (bool)rightIndicator)
			{
				switch (inputs.indicators)
				{
				case RCC_CarControllerV3.IndicatorsOn.Left:
					leftIndicator.color = new Color(1f, 0.5f, 0f);
					rightIndicator.color = new Color(0.5f, 0.25f, 0f);
					break;
				case RCC_CarControllerV3.IndicatorsOn.Right:
					leftIndicator.color = new Color(0.5f, 0.25f, 0f);
					rightIndicator.color = new Color(1f, 0.5f, 0f);
					break;
				case RCC_CarControllerV3.IndicatorsOn.All:
					leftIndicator.color = new Color(1f, 0.5f, 0f);
					rightIndicator.color = new Color(1f, 0.5f, 0f);
					break;
				case RCC_CarControllerV3.IndicatorsOn.Off:
					leftIndicator.color = new Color(0.5f, 0.25f, 0f);
					rightIndicator.color = new Color(0.5f, 0.25f, 0f);
					break;
				}
			}
		}
	}
}
