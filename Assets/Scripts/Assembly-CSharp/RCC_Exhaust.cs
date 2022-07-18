using UnityEngine;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/Exhaust")]
public class RCC_Exhaust : MonoBehaviour
{
	private RCC_Settings RCCSettingsInstance;

	private RCC_CarControllerV3 carController;

	private ParticleSystem particle;

	private ParticleSystem.EmissionModule emission;

	private ParticleSystem.MinMaxCurve emissionRate;

	public ParticleSystem flame;

	private ParticleSystem.EmissionModule subEmission;

	private ParticleSystem.MinMaxCurve subEmissionRate;

	private Light flameLight;

	public float flameTime;

	private AudioSource flameSource;

	public Color flameColor = Color.red;

	public Color boostFlameColor = Color.blue;

	public bool previewFlames;

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
		if (RCCSettings.dontUseAnyParticleEffects)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		carController = GetComponentInParent<RCC_CarControllerV3>();
		particle = GetComponent<ParticleSystem>();
		emission = particle.emission;
		if ((bool)flame)
		{
			subEmission = flame.emission;
			flameLight = flame.GetComponentInChildren<Light>();
			flameSource = RCC_CreateAudioSource.NewAudioSource(base.gameObject, "Exhaust Flame AudioSource", 10f, 25f, 1f, RCCSettings.exhaustFlameClips[0], loop: false, playNow: false, destroyAfterFinished: false);
			flameLight.renderMode = ((!RCCSettings.useLightsAsVertexLights) ? LightRenderMode.ForcePixel : LightRenderMode.ForceVertex);
		}
	}

	private void Update()
	{
		if (!carController || !particle)
		{
			return;
		}
		if (carController.engineRunning)
		{
			if (carController.speed < 150f)
			{
				if (!emission.enabled)
				{
					emission.enabled = true;
				}
				if (carController._gasInput > 0.05f)
				{
					emissionRate.constantMax = 50f;
					emission.rate = emissionRate;
					particle.startSpeed = 5f;
					particle.startSize = 5f;
				}
				else
				{
					emissionRate.constantMax = 5f;
					emission.rate = emissionRate;
					particle.startSpeed = 0.5f;
					particle.startSize = 2.5f;
				}
			}
			else if (emission.enabled)
			{
				emission.enabled = false;
			}
			if (carController._gasInput >= 0.25f)
			{
				flameTime = 0f;
			}
			if ((carController.useExhaustFlame && carController.engineRPM >= 5000f && carController.engineRPM <= 5500f && carController._gasInput <= 0.25f && flameTime <= 0.5f) || carController._boostInput >= 1.5f || previewFlames)
			{
				flameTime += Time.deltaTime;
				subEmission.enabled = true;
				if ((bool)flameLight)
				{
					flameLight.intensity = flameSource.pitch * 3f * Random.Range(0.25f, 1f);
				}
				if (carController._boostInput >= 1.5f && (bool)flame)
				{
					flame.startColor = boostFlameColor;
					flameLight.color = flame.startColor;
				}
				else
				{
					flame.startColor = flameColor;
					flameLight.color = flame.startColor;
				}
				if (!flameSource.isPlaying)
				{
					flameSource.clip = RCCSettings.exhaustFlameClips[Random.Range(0, RCCSettings.exhaustFlameClips.Length)];
					flameSource.Play();
				}
			}
			else
			{
				subEmission.enabled = false;
				if ((bool)flameLight)
				{
					flameLight.intensity = 0f;
				}
				if (flameSource.isPlaying)
				{
					flameSource.Stop();
				}
			}
		}
		else
		{
			if (emission.enabled)
			{
				emission.enabled = false;
			}
			subEmission.enabled = false;
			if ((bool)flameLight)
			{
				flameLight.intensity = 0f;
			}
			if (flameSource.isPlaying)
			{
				flameSource.Stop();
			}
		}
	}
}
