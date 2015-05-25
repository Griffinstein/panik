using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour {
	private GameObject player;
	private LastPlayerSighting lastPlayerSighting;
	
	Renderer render;
	
	void Awake()
	{
		render = GetComponent<Renderer> ();
		player = GameObject.FindGameObjectWithTag (Tags.player);
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
	}
	
	void OnTriggerStay(Collider other)
	{
		if (render.enabled) {
			if (other.gameObject == player)
			{
				lastPlayerSighting.position = other.transform.position;
			}
		}
	}
}