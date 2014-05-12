using UnityEngine;
using System.Collections;

public class SunFader : MonoBehaviour {
	public Color startColor = Color.red;
	public Color endColor = Color.white;
	public Color currentColor = Color.red;
	public float currentTime = 0.0f;
	public float fadeSpeed = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += fadeSpeed/1000.0f;
		renderer.material.color = Color.Lerp(startColor, endColor, currentTime);
	}

	void OnEnable(){
			currentColor = startColor;
			currentTime = 0.0f;
	}
}
