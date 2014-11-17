using UnityEngine;
using System.Collections;

public class twinkle : MonoBehaviour {
	public float currentTime = 1.0f;

	
	public Color startColor = Color.red;
	public Color endColor = Color.white;
	public float currentFade = 0.0f;
	public float fadeTime = 5.0f;
	public bool fading = false;
	public float deathTime = 2.0f;
	public Vector3 randRange = Vector3.zero;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, deathTime);
	}
	
	// Update is called once per frame
	void Update () { 
		currentTime -= Time.deltaTime * 1.0f;
		//= new Color (currentTime, currentTime, currentTime);
		StartFade ();
	}
	
	void StartFade(){
		if (currentFade < 1 && fading) {
			currentFade += Time.deltaTime * fadeTime;
			startColor = new Color (startColor.r+ Random.Range(-randRange.x,randRange.x), startColor.g+ Random.Range(-randRange.y,randRange.y), startColor.b+ Random.Range(-randRange.z,randRange.z));
			GetComponent<TintCollider> ().tintColor  = Color.Lerp (startColor, endColor, currentFade);
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
