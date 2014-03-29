using UnityEngine;
using System.Collections;

public class Lantern : MonoBehaviour {

	public string column = "";
	public int index = 0;
	public Transform myCable;
	public Cable cable;
	internal LED[] leds = new LED[13];



 	void Awake(){
//		myCable = transform.Find("Cables");

//			cable = myCable.GetComponent<Cable>();
	}

	void Update() {
//		Debug.Log ("is this running");

		/*
		for(int i=0; i<13; ++i) {
			leds[i] = GetLED(i).GetComponent<LED>();
			cable.leds[index*13] = GetLED(i).GetComponent<LED>();
		}
		*/
	}
	
	internal Color AverageColor() {
		Color result = Color.black;
		float k = 1.0f / 13.0f;
		for(int i=0; i<leds.Length; ++i) {
			result += k * leds[i].currentColor;
		}	
		return result;
	}

	public Transform GetLED(int i) { 
		i = 12 - i; // reverse lanterns top-bottom :P
		
		//	return transform.Find(string.Format("LED{0}", i+1));//transform.GetChild (i);//
		
		return transform.GetChild (i);//
	}	

}
