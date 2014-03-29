using UnityEngine;
using System.Collections;

public class RainMaker : MonoBehaviour {
	public GameObject rainParticle;
	public bool rainOn = true;
	public bool beenaminute = false;
	public float timeTilNextDrop = 0.0f;
	public float currentTime = 0.0f;
	
	public float lowRange = 1.0f;
	public float highRange = 5.0f;
	public bool useAudio = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown("space"))
		{
		if(rainOn){
			rainOn = false;
		}else{
			rainOn = true;	
			}
		}
	if(rainOn){
			
			if(!audio.isPlaying){
			audio.Play();
			}
			
			if(beenaminute){
				beenaminute = false;
			GameObject clone;
			float range = 5.0f;
			Vector3 randomLocation = new Vector3(Random.Range(-range, range)+transform.position.x, Random.Range(-range, range)+transform.position.y,Random.Range(-range, range)+transform.position.z);		
            clone = Instantiate(rainParticle, randomLocation, transform.localRotation) as GameObject;
			//Color myColor = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), 0.5f); 
			//clone.GetComponent<panelColorer>().myColor = myColor;
			//clone.GetComponent<panelColorer>().emisColor = myColor;
				
				timeTilNextDrop = Random.Range(lowRange,highRange);
			}
			
	}else{
		if(audio.isPlaying){
			audio.Stop();
			}	
		}
		
		
		
		if(currentTime > timeTilNextDrop){
			beenaminute = true;
			currentTime = 0.0f;
		}else{
		currentTime += Time.fixedDeltaTime;
			beenaminute = false;
			
		}
		
	}
}
