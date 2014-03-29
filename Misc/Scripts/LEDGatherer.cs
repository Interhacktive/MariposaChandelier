using UnityEngine;
using System.Collections;

public class LEDGatherer : MonoBehaviour {
	Transform []LED;
	public Color []LEDColors;
	public int lampNumber;
	public int LEDAmount = 13;
	
	// Use this for initialization
	void Start () {
		LED = new Transform[LEDAmount];
		LEDColors = new Color[LEDAmount];
		for(int i = 0;i<LEDAmount;i++){
			LED[i] = transform.Find("LED"+ (i+1));
			LEDColors[i] = LED[i].gameObject.renderer.material.color;
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0;i<LEDAmount;i++){
			LEDColors[i] = LED[i].gameObject.renderer.material.color;
		}	
	}
}
