using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour {
	public Vector3 position = new Vector3 (1000f, 1000f, 1000f);
	public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);
	public float lightHighIntensity = 0.25f;
	public float lightLowIntensity = 0f;
	public float fadeSpeed = 7f;
	public float musicFadeSpeed = 1f;

	void Awake()
	{
		//alarm = GameObject.FindGameObjectWithTag (Tags.alarm).GetComponent<AlarmLight> ();
		//mainlight = GameObject.FindGameObjectWithTag (Tags.mainLight).GetComponent<Light>();
		//panicAudio = transform.Find ("secondaryMusic").GetComponent<AudioSource>();
		//GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag (Tags.siren);
		//sirens = new AudioSource[sirenGameObjects.Length];
		//music = GetComponent<AudioSource> ();

//		for (int i = 0; i < sirens.Length; i++) {
//			sirens[i] = sirenGameObjects[i].GetComponent<AudioSource>();
//		}
	}

	void Update()
	{
		SwitchAlarms ();
		MusicFading ();
	}

	void SwitchAlarms()
	{
//		alarm.alarmOn = (position != resetPosition);
//
//		float newIntensity;
//
//		if (alarm.alarmOn) {
//			newIntensity = lightLowIntensity;
//		} else {
//			newIntensity = lightHighIntensity;
//		}
//
//		mainlight.intensity = Mathf.Lerp (mainlight.intensity, newIntensity, fadeSpeed * Time.deltaTime);
//
//		for (int i = 0; i < sirens.Length; i++) {
//			if (alarm.alarmOn && !sirens[i].isPlaying){
//				sirens[i].Play();
//			} else if (!alarm.alarmOn) {
//				sirens[i].Stop();
//			}
//		}
	}

	void MusicFading()
	{
//		if (alarm.alarmOn) {
//			music.volume = Mathf.Lerp (music.volume, 0f, musicFadeSpeed * Time.deltaTime);
//			panicAudio.volume = Mathf.Lerp (panicAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
//		} else {
//			music.volume = Mathf.Lerp (music.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
//			panicAudio.volume = Mathf.Lerp (panicAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
//		}

	}
}
