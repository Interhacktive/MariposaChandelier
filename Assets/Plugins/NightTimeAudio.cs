using UnityEngine;
using System.Collections;

public class NightTimeAudio : MonoBehaviour {
	public float dayTimeVolume = 0.75f;
	public float nightTimeVolume = 0.25f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (System.DateTime.Now.Hour > 22 || System.DateTime.Now.Hour < 9) {
						AudioListener.volume = nightTimeVolume;
				} else {
			AudioListener.volume = dayTimeVolume;
				}
	}
}
