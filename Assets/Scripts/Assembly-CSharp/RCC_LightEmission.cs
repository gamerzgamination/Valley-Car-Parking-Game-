using UnityEngine;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Light/Light Emission")]
public class RCC_LightEmission : MonoBehaviour
{
	private Light sharedLight;

	public Renderer lightRenderer;

	public int materialIndex;

	public bool noTexture;

	private void Start()
	{
		sharedLight = GetComponent<Light>();
		Material material = lightRenderer.materials[materialIndex];
		material.EnableKeyword("_EMISSION");
	}

	private void Update()
	{
		if (!sharedLight.enabled)
		{
			lightRenderer.materials[materialIndex].SetColor("_EmissionColor", Color.white * 0f);
		}
		else if (!noTexture)
		{
			lightRenderer.materials[materialIndex].SetColor("_EmissionColor", Color.white * sharedLight.intensity);
		}
		else
		{
			lightRenderer.materials[materialIndex].SetColor("_EmissionColor", Color.red * sharedLight.intensity);
		}
	}
}
