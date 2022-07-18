using UnityEngine;
using UnityEngine.UI;

public class RCC_UISliderTextReader : MonoBehaviour
{
	public Slider slider;

	public Text text;

	private void Awake()
	{
		if (!slider)
		{
			slider = GetComponentInParent<Slider>();
		}
		if (!text)
		{
			text = GetComponentInChildren<Text>();
		}
	}

	private void Update()
	{
		text.text = slider.value.ToString("F1");
	}
}
