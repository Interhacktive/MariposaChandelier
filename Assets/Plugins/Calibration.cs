using UnityEngine;
using System.Collections;

public class Calibration : MonoBehaviour {
	
	public int sensorThresholdOff = 140;
	public int sensorThresholdOn = 150;
	
	// act like a singleton
	public static Calibration inst;
	
	void Awake() {
		
		// validate settings before exposing them to the public
		var isValid = true;
		
		if (sensorThresholdOff > sensorThresholdOn) {
			Debug.LogWarning("Sensor Thresholds Invalid");
			isValid = false;
		}
	
		if (isValid) {
			inst = this;
		}
		
	}
	
	void OnDestroy() {
		if (inst == this) { inst = null; }
	}
	
}
