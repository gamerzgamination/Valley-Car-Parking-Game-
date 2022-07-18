using UnityEngine;
using UnityEngine.UI;

public class ColorPickerBySliders : MonoBehaviour
{
	public Color color;

	public Slider redSlider;

	public Slider greenSlider;

	public Slider blueSlider;

	public void Update()
	{
		color = new Color(redSlider.value, greenSlider.value, blueSlider.value);
	}
}
