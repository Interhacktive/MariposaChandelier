using UnityEngine;
using System.Collections;

public class ObjectGenerator : MonoBehaviour {
	public Transform objectToThrow;
	public float throwDelay = 1.0f;
	float lastTime = 0.0f;
	public float randomAmount = 1.0f;
	float randomValue = 0.0f;
	 float timeLeft = 0.0f;
	public Vector3 randomDistance = new Vector3(1,1,1);
	Vector3 randomDistanceValue;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft = Time.time-lastTime;
		throwObject ();
			
			

	}

	void throwObject(){
		randomValue  = Random.Range(-randomAmount, randomAmount);
		randomDistanceValue = new Vector3(
			Random.Range(-randomDistance.x, randomDistance.x), 
			Random.Range(-randomDistance.y, randomDistance.y),
			Random.Range(-randomDistance.z, randomDistance.z));

		if(Time.time-lastTime > throwDelay+randomValue){

			Instantiate(objectToThrow, transform.position+ randomDistanceValue, Quaternion.identity);

		lastTime = Time.time;
		}
	}

	void OnEnable(){
		lastTime = Time.time;
	}

	void OnDisable(){

	}
}
