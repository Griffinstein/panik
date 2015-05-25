using UnityEngine;
using System.Collections;

public class LaserSwitchDeactivation : MonoBehaviour {
	public GameObject laser;
	public Material unlockedMat;

	private GameObject player;

	private AudioSource audio;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag (Tags.player);
		audio = GetComponent<AudioSource> ();
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player) {
			if (Input.GetButton ("Switch"))
			{
				laserDeactivation();
			}
		}
	}

	void laserDeactivation()
	{
		laser.SetActive (false);

		Renderer screen = transform.Find ("prop_switchUnit_screen_001").GetComponent<Renderer>();
		screen.material = unlockedMat;
		audio.Play ();
	}
}
