using UnityEngine;
using System;

public class Sensor : MonoBehaviour {
	
	// receives messages when the sensor is triggered
	public bool sensorLive = false;

	public bool triggered = false;//was in nonserialized
	public int currentReading = 0;
	public int triggerOff = 150;
	public int triggerOn = 200;

	public bool upActive = true;

	public int soundIDUp = 1;
	public bool loopUp = false;
	public float spreadUp = 0.1f;
	public Transform Up;


	
	public bool downActive = false;

	public int soundIDDown = 1;
	public bool loopDown = false;
	public float spreadDown = 0.1f;
	public Transform Down;


	public Color waveColor;

	public GameObject sensorDelegate;

	public int average = 0;
	public int threshold  = 100;
	int[] readings;
	int total = 0;
	int index = 0;
		
	int numReadings = 5000;


	//[NonSerialized]
	public bool randomColor = true;
	public event Action<Sensor> onSignal;


	bool ableToTrigger = false;

	public void Signal(int signal) {
		//Debug.Log (signal);
		currentReading = signal;
		// handle the given signal, checking against hystersis

		if (signal < triggerOff) {//Calibration.inst.sensorThresholdOff
			ableToTrigger = true;
		}

		if (triggered) {

			triggered = false;


		} else {
			if (signal > triggerOn && ableToTrigger) {
				triggered = true;
				ableToTrigger = false;


				//-----experiment


					//Destroy(gameObject);
					
				//------
				Debug.LogWarning("Triggered!");
				if (onSignal != null) {
					onSignal(this);
				}
				sensorDelegate.SendMessage(
					"OnSensorTriggered", this, 
					SendMessageOptions.DontRequireReceiver
				);
			}
		}
	} 
	
	void Start() {
		if(randomColor){
			waveColor = new Color(UnityEngine.Random.Range(0.0f,1.0f), UnityEngine.Random.Range(0.0f,1.0f), UnityEngine.Random.Range(0.0f,1.0f));
		 }	
		//waveColor =  Color.red;//(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));

		// make sure we're calibrated
		if (!Calibration.inst) {
			Debug.LogWarning("Calibration Settings Missing!");
		}
		
		// figure out delegate
		if (sensorDelegate == null) {
			sensorDelegate = gameObject;
		}

		//average = new int[numReadings];
		///total = new int[numReadings];
		readings = new int[numReadings];
		for (int i = 0; i<numReadings; i++) {
		//	average[i] = 0;
		//	total[i]=0;
			readings[i] = 0;
				}

		
	}

	
	void averageData(){
		
			// subtract the last reading:
			total = total - readings[index];         
			// read from the sensor:  
		readings[index] = currentReading;//analogRead(inputPin); 
			// add the reading to the total:
			total = total + readings[index];       
			// advance to the next position in the array:  
			index = index + 1;                    
			
			// if we're at the end of the array...
			if (index >= numReadings)              
				// ...wrap around to the beginning: 
				index = 0;                           
			
			// calculate the average:
			average = (total / numReadings);         
			// send it to the computer as ASCII digits
			// Serial.print(average[sensorNum]);   
			//Serial.print(' ');
			//delay(1);        // delay in between reads for stability  
		triggerOn = average + threshold;
		triggerOff = average;

	}


	void Update(){
		if (sensorLive) {
						if (triggered) {
								if (randomColor) {
										waveColor = new Color (UnityEngine.Random.Range (0.0f, 1.0f), UnityEngine.Random.Range (0.0f, 1.0f), UnityEngine.Random.Range (0.0f, 1.0f));
								}
								if (upActive) {
										Transform theClonedExplosionUp;
										theClonedExplosionUp = Instantiate (Up, transform.position, transform.rotation) as Transform;
										theClonedExplosionUp.GetComponent<TintCollider> ().tintColor = waveColor;
										theClonedExplosionUp.renderer.material.color = waveColor;
										theClonedExplosionUp.GetComponent<AudioObject> ().soundID = soundIDUp;
										theClonedExplosionUp.GetComponent<AudioObject> ().loop = loopUp;
										theClonedExplosionUp.GetComponent<AudioObject> ().spread = spreadUp;


								}
								if (downActive) {
										Transform theClonedExplosionDown;
										theClonedExplosionDown = Instantiate (Down, transform.position, transform.rotation) as Transform;

										theClonedExplosionDown.GetComponent<TintCollider> ().tintColor = waveColor;
										theClonedExplosionDown.renderer.material.color = waveColor;
										theClonedExplosionDown.GetComponent<AudioObject> ().soundID = soundIDDown;
										theClonedExplosionDown.GetComponent<AudioObject> ().loop = loopDown;
										theClonedExplosionDown.GetComponent<AudioObject> ().spread = spreadDown;


										theClonedExplosionDown = Instantiate (Down, transform.position, transform.rotation) as Transform;
								}
								triggered = false;
						} else {
								//theClonedExplosionDown.GetComponent(renderer).material.color = Color.red;
						}
			averageData();
				}
	}
	
	void OnDrawGizmos() {
		if(triggered){
		Gizmos.DrawIcon(transform.position, "hands.png");
		}else{
			Gizmos.DrawIcon(transform.position, "sensorIcon.psd");

		}
	}

	void OnDrawGizmoSelected() {
		if(triggered){
			Gizmos.DrawIcon(transform.position, "hands.png");
		}else{
			Gizmos.DrawIcon(transform.position, "sensorIcon.psd");
			
		}
	}
	
}
