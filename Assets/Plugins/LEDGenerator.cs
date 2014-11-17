using UnityEngine;
using System.Collections;

public class LEDGenerator : MonoBehaviour {
	public GameObject LedCubeReference;
	GameObject[] iLEDs;

	int ledAmount = 130;

	// Use this for initialization
	void Start () {

//		ledAmount = GetComponent<Cable>().leds.Length;
		iLEDs = new GameObject[ledAmount];

		for(int i = 0;i<ledAmount;i++){
			Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + (i*-0.5f), transform.position.z);
			GameObject clone;
			//clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;

			iLEDs[i] = Instantiate(LedCubeReference, newPosition, gameObject.transform.rotation) as GameObject;
			GetComponent<Cable>().leds[i] = iLEDs[i].GetComponent<LED>();
			iLEDs[i].name = "LEDCube" + i;
			iLEDs[i].transform.parent = this.gameObject.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKey (KeyCode.S)) {//SAVE
			
			for(int i = 0;i<ledAmount;i++){
				PlayerPrefs.SetFloat(iLEDs[i].name + this.name + "x",iLEDs[i].transform.position.x);
				PlayerPrefs.SetFloat(iLEDs[i].name + this.name + "y",iLEDs[i].transform.position.y);
				PlayerPrefs.SetFloat(iLEDs[i].name + this.name + "z",iLEDs[i].transform.position.z);

			}
			
			Debug.Log ("Saving LED positions");
				}
		if (Input.GetKey (KeyCode.L)) {//LOAD
			for(int i = 0;i<ledAmount;i++){
				 
					Vector3 ledPos = new Vector3(
					PlayerPrefs.GetFloat(iLEDs[i].name + this.name + "x"),
				PlayerPrefs.GetFloat(iLEDs[i].name + this.name + "y"),
						PlayerPrefs.GetFloat(iLEDs[i].name + this.name + "z"));
				iLEDs[i].transform.position = ledPos;

			}
			Debug.Log ("Loading LED positions");

		}


	}
}
