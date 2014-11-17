using UnityEngine;
using System.Collections;

public class AudioReactive : MonoBehaviour {
	FFTWindow myWindow;
	float[] mySamples;
	int sampleCount = 64;
	public int whichSample = 2;
	public float audioVolume = 0.0f;
	public Color objectColor;
	float audioLength = 0.0f;
	public float volumeScale = 30.0f;

	// Use this for initialization
	void Start () {
		mySamples = new float[sampleCount];
		for(int i = 0;i<sampleCount;i++){
			mySamples[i] = 0.0f;
		}
	
		//objectColor = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0;i<sampleCount;i++){
			mySamples[i] = 0.0f;
		}
		//AudioListener.GetOutputData(mySamples, 0);
//		AudioListener.GetSpectrumData(mySamples, 0, FFTWindow.Rectangular);
		transform.parent.GetComponent<AudioSource>().GetSpectrumData(mySamples, 0, FFTWindow.Rectangular);
			//	Debug.Log ("myAudio sample: " + mySamples[10]);
		//for(int i = 0;i<sampleCount;i++){

			//Debug.DrawLine(new Vector3(i/10.0f, 0, 0), new Vector3(i/10.0f, mySamples[i]*10,0));
		//		}
		audioVolume = mySamples[whichSample]* volumeScale;
		renderer.material.color = Color.Lerp (Color.black, objectColor, audioVolume);
		transform.localScale = new Vector3(1,audioVolume, 1);


	}
}
