using UnityEngine;
using System.Collections;

public class SunColorChanger : MonoBehaviour {
	public Color startColor = Color.red;
	public Color endColor = Color.white;
	public float currentFade = 0.0f;
	public float fadeTime = 5.0f;
	public bool fading = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		StartFade();
	}

	void StartFade(){
		if (currentFade < 1 && fading) {
						currentFade += Time.deltaTime * fadeTime/100;
						renderer.material.color = Color.Lerp (startColor, endColor, currentFade);
				} else {
			currentFade = 0;
			fading = false;
				}
	}
	void OnEnable(){
		fading = true;
		StartFade ();
	}
}
