using UnityEngine;
using System;

public class Sensor : MonoBehaviour {
	
	// receives messages when the sensor is triggered

	public bool triggered = false;//was in nonserialized

	
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

	//[NonSerialized]
	public bool randomColor = true;
	public event Action<Sensor> onSignal;
	
	public void Signal(int signal) {
		
		// handle the given signal, checking against hystersis
		
		if (triggered) {



			if (signal < Calibration.inst.sensorThresholdOff) {
				triggered = false;
			}
		} else {
			if (signal > Calibration.inst.sensorThresholdOn) {
				triggered = true;


				//-----experiment


					//Destroy(gameObject);
					
				//------
				//Debug.LogWarning("Triggered!");
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
		
	}

	void Update(){
		if(triggered){
			if(randomColor){
				waveColor = new Color(UnityEngine.Random.Range(0.0f,1.0f), UnityEngine.Random.Range(0.0f,1.0f), UnityEngine.Random.Range(0.0f,1.0f));
			}
			if(upActive){
		Transform theClonedExplosionUp;
		theClonedExplosionUp = Instantiate(Up, transform.position, transform.rotation) as Transform;
				theClonedExplosionUp.GetComponent<TintCollider>().tintColor = waveColor;
				theClonedExplosionUp.renderer.material.color = waveColor;
				theClonedExplosionUp.GetComponent<AudioObject>().soundID = soundIDUp;
				theClonedExplosionUp.GetComponent<AudioObject>().loop = loopUp;
				theClonedExplosionUp.GetComponent<AudioObject>().spread = spreadUp;


			}
			if(downActive){
			Transform theClonedExplosionDown;
				theClonedExplosionDown = Instantiate(Down, transform.position, transform.rotation) as Transform;

				theClonedExplosionDown.GetComponent<TintCollider>().tintColor = waveColor;
				theClonedExplosionDown.renderer.material.color = waveColor;
				theClonedExplosionDown.GetComponent<AudioObject>().soundID = soundIDDown;
				theClonedExplosionDown.GetComponent<AudioObject>().loop = loopDown;
				theClonedExplosionDown.GetComponent<AudioObject>().spread = spreadDown;


			theClonedExplosionDown = Instantiate(Down, transform.position, transform.rotation) as Transform;
			}
			triggered = false;
		}else{
			//theClonedExplosionDown.GetComponent(renderer).material.color = Color.red;
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
		Gizmos.DrawIcon(transform.position, "sensorIcon.psd");
	}
	
}
