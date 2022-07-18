using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Dashboard Button")]
public class RCC_UIDashboardButton : MonoBehaviour
{
	public enum ButtonType
	{
		Start,
		ABS,
		ESP,
		TCS,
		Headlights,
		LeftIndicator,
		RightIndicator,
		Gear,
		Low,
		Med,
		High,
		SH,
		GearUp,
		GearDown,
		HazardLights,
		SlowMo
	}

	public ButtonType _buttonType;

	private Scrollbar gearSlider;

	private RCC_CarControllerV3[] carControllers;

	private int gearDirection;

	private void Start()
	{
		if (_buttonType == ButtonType.Gear && (bool)GetComponentInChildren<Scrollbar>())
		{
			gearSlider = GetComponentInChildren<Scrollbar>();
			gearSlider.onValueChanged.AddListener(delegate
			{
				ChangeGear();
			});
		}
	}

	private void OnEnable()
	{
		Check();
	}

	public void OnClicked()
	{
		carControllers = Object.FindObjectsOfType<RCC_CarControllerV3>();
		switch (_buttonType)
		{
		case ButtonType.Start:
		{
			for (int num2 = 0; num2 < carControllers.Length; num2++)
			{
				if (carControllers[num2].canControl && !carControllers[num2].AIController)
				{
					carControllers[num2].KillOrStartEngine();
				}
			}
			break;
		}
		case ButtonType.ABS:
		{
			for (int i = 0; i < carControllers.Length; i++)
			{
				if (carControllers[i].canControl && !carControllers[i].AIController)
				{
					carControllers[i].ABS = !carControllers[i].ABS;
				}
			}
			break;
		}
		case ButtonType.ESP:
		{
			for (int l = 0; l < carControllers.Length; l++)
			{
				if (carControllers[l].canControl && !carControllers[l].AIController)
				{
					carControllers[l].ESP = !carControllers[l].ESP;
				}
			}
			break;
		}
		case ButtonType.TCS:
		{
			for (int num4 = 0; num4 < carControllers.Length; num4++)
			{
				if (carControllers[num4].canControl && !carControllers[num4].AIController)
				{
					carControllers[num4].TCS = !carControllers[num4].TCS;
				}
			}
			break;
		}
		case ButtonType.SH:
		{
			for (int n = 0; n < carControllers.Length; n++)
			{
				if (carControllers[n].canControl && !carControllers[n].AIController)
				{
					carControllers[n].steeringHelper = !carControllers[n].steeringHelper;
				}
			}
			break;
		}
		case ButtonType.Headlights:
		{
			for (int j = 0; j < carControllers.Length; j++)
			{
				if (carControllers[j].canControl && !carControllers[j].AIController)
				{
					if (!carControllers[j].highBeamHeadLightsOn && carControllers[j].lowBeamHeadLightsOn)
					{
						carControllers[j].highBeamHeadLightsOn = true;
						carControllers[j].lowBeamHeadLightsOn = true;
						break;
					}
					if (!carControllers[j].lowBeamHeadLightsOn)
					{
						carControllers[j].lowBeamHeadLightsOn = true;
					}
					if (carControllers[j].highBeamHeadLightsOn)
					{
						carControllers[j].lowBeamHeadLightsOn = false;
						carControllers[j].highBeamHeadLightsOn = false;
					}
				}
			}
			break;
		}
		case ButtonType.LeftIndicator:
		{
			for (int num5 = 0; num5 < carControllers.Length; num5++)
			{
				if (carControllers[num5].canControl && !carControllers[num5].AIController)
				{
					if (carControllers[num5].indicatorsOn != RCC_CarControllerV3.IndicatorsOn.Left)
					{
						carControllers[num5].indicatorsOn = RCC_CarControllerV3.IndicatorsOn.Left;
					}
					else
					{
						carControllers[num5].indicatorsOn = RCC_CarControllerV3.IndicatorsOn.Off;
					}
				}
			}
			break;
		}
		case ButtonType.RightIndicator:
		{
			for (int num3 = 0; num3 < carControllers.Length; num3++)
			{
				if (carControllers[num3].canControl && !carControllers[num3].AIController)
				{
					if (carControllers[num3].indicatorsOn != RCC_CarControllerV3.IndicatorsOn.Right)
					{
						carControllers[num3].indicatorsOn = RCC_CarControllerV3.IndicatorsOn.Right;
					}
					else
					{
						carControllers[num3].indicatorsOn = RCC_CarControllerV3.IndicatorsOn.Off;
					}
				}
			}
			break;
		}
		case ButtonType.HazardLights:
		{
			for (int num = 0; num < carControllers.Length; num++)
			{
				if (carControllers[num].canControl && !carControllers[num].AIController)
				{
					if (carControllers[num].indicatorsOn != RCC_CarControllerV3.IndicatorsOn.All)
					{
						carControllers[num].indicatorsOn = RCC_CarControllerV3.IndicatorsOn.All;
					}
					else
					{
						carControllers[num].indicatorsOn = RCC_CarControllerV3.IndicatorsOn.Off;
					}
				}
			}
			break;
		}
		case ButtonType.Low:
			QualitySettings.SetQualityLevel(1);
			break;
		case ButtonType.Med:
			QualitySettings.SetQualityLevel(3);
			break;
		case ButtonType.High:
			QualitySettings.SetQualityLevel(5);
			break;
		case ButtonType.GearUp:
		{
			for (int m = 0; m < carControllers.Length; m++)
			{
				if (carControllers[m].canControl && !carControllers[m].AIController && carControllers[m].currentGear < carControllers[m].totalGears - 1 && !carControllers[m].changingGear)
				{
					if (carControllers[m].direction != -1)
					{
						carControllers[m].StartCoroutine("ChangingGear", carControllers[m].currentGear + 1);
					}
					else
					{
						carControllers[m].StartCoroutine("ChangingGear", 0);
					}
				}
			}
			break;
		}
		case ButtonType.GearDown:
		{
			for (int k = 0; k < carControllers.Length; k++)
			{
				if (carControllers[k].canControl && !carControllers[k].AIController && carControllers[k].currentGear >= 0)
				{
					carControllers[k].StartCoroutine("ChangingGear", carControllers[k].currentGear - 1);
				}
			}
			break;
		}
		case ButtonType.SlowMo:
			if (Time.timeScale != 0.2f)
			{
				Time.timeScale = 0.2f;
			}
			else
			{
				Time.timeScale = 1f;
			}
			break;
		}
		Check();
	}

	public void Check()
	{
		carControllers = Object.FindObjectsOfType<RCC_CarControllerV3>();
		if (!GetComponent<Image>())
		{
			return;
		}
		switch (_buttonType)
		{
		case ButtonType.ABS:
		{
			for (int j = 0; j < carControllers.Length; j++)
			{
				if (!carControllers[j].AIController && carControllers[j].canControl && carControllers[j].ABS)
				{
					GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
				}
				else if (!carControllers[j].AIController && carControllers[j].canControl)
				{
					GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1f);
				}
			}
			break;
		}
		case ButtonType.ESP:
		{
			for (int l = 0; l < carControllers.Length; l++)
			{
				if (!carControllers[l].AIController && carControllers[l].canControl && carControllers[l].ESP)
				{
					GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
				}
				else if (!carControllers[l].AIController && carControllers[l].canControl)
				{
					GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1f);
				}
			}
			break;
		}
		case ButtonType.TCS:
		{
			for (int m = 0; m < carControllers.Length; m++)
			{
				if (!carControllers[m].AIController && carControllers[m].canControl && carControllers[m].TCS)
				{
					GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
				}
				else if (!carControllers[m].AIController && carControllers[m].canControl)
				{
					GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1f);
				}
			}
			break;
		}
		case ButtonType.SH:
		{
			for (int k = 0; k < carControllers.Length; k++)
			{
				if (!carControllers[k].AIController && carControllers[k].canControl && carControllers[k].steeringHelper)
				{
					GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
				}
				else if (!carControllers[k].AIController && carControllers[k].canControl)
				{
					GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1f);
				}
			}
			break;
		}
		case ButtonType.Headlights:
		{
			for (int i = 0; i < carControllers.Length; i++)
			{
				if ((!carControllers[i].AIController && carControllers[i].canControl && carControllers[i].lowBeamHeadLightsOn) || carControllers[i].highBeamHeadLightsOn)
				{
					GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
				}
				else if (!carControllers[i].AIController && carControllers[i].canControl)
				{
					GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1f);
				}
			}
			break;
		}
		}
	}

	public void ChangeGear()
	{
		if (gearDirection == Mathf.CeilToInt(gearSlider.value * 2f))
		{
			return;
		}
		gearDirection = Mathf.CeilToInt(gearSlider.value * 2f);
		for (int i = 0; i < carControllers.Length; i++)
		{
			if (!carControllers[i].AIController && carControllers[i].canControl)
			{
				carControllers[i].semiAutomaticGear = true;
				switch (gearDirection)
				{
				case 0:
					carControllers[i].StartCoroutine("ChangingGear", 0);
					carControllers[i].NGear = false;
					break;
				case 1:
					carControllers[i].NGear = true;
					break;
				case 2:
					carControllers[i].StartCoroutine("ChangingGear", -1);
					carControllers[i].NGear = false;
					break;
				}
			}
		}
	}

	private void OnDisable()
	{
		if (_buttonType != ButtonType.Gear)
		{
			return;
		}
		carControllers = Object.FindObjectsOfType<RCC_CarControllerV3>();
		RCC_CarControllerV3[] array = carControllers;
		foreach (RCC_CarControllerV3 rCC_CarControllerV in array)
		{
			if (!rCC_CarControllerV.AIController && rCC_CarControllerV.canControl)
			{
				rCC_CarControllerV.semiAutomaticGear = false;
			}
		}
	}
}
