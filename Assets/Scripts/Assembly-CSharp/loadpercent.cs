using UnityEngine;
using UnityEngine.UI;

public class loadpercent : MonoBehaviour
{
	public Text number;

	public Slider number_slider;

	public void ADD()
	{
		number.text = number_slider.value + "%";
	}
}
