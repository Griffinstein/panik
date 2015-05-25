using UnityEngine;
using System.Collections;

public class LaserBlinking : MonoBehaviour {
	public float onTime;
	public float offTime;

	private float timer;

	Renderer render;
	Light light;

	void Awake ()
	{
		render = GetComponent<Renderer> ();
		light = GetComponent<Light> ();
	}

	void Update()
	{
		timer += Time.deltaTime;

		if (render.enabled && timer >= onTime) {
			SwitchBeam();
		}

		if (!render.enabled && timer >= offTime) {
			SwitchBeam();
		}	
	}

	void SwitchBeam()
	{
		timer = 0f;

		render.enabled = !render.enabled;
		light.enabled = !light.enabled;
	}
}
