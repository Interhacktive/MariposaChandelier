using UnityEngine;
using System.Collections;

public class AudioTrackController : MonoBehaviour {
	
	// Manages a list of audio "tracks" which audio objects
	// can "attach" to.  When an audio object is displaced it
	// cross-fades out.
	
	// Track index N will use audio trigger id N+1.  Avoid calls
	// to SoundController on this id while the track is enabled to
	// avoid undefined behaviour.
	
	// act like a singleton
	public static AudioTrackController inst;

	public int numTracks = 6;
	
	public AudioObject[] trackToObject;

	float oldPos;
	public  static int currentTrack = 0;


	float bottom, top;
		
	void Awake() {
		inst = this;
	}
	
	void Start() {
		
		// initialize trackToObject to null
		trackToObject = new AudioObject[numTracks];
		
		// find sim bounds to determine y-position range
		var box = GameObject.Find ("AudioBounds").GetComponent<BoxCollider>();
		bottom = box.transform.TransformPoint(box.center - 0.5f * box.size).y;
		top = box.transform.TransformPoint(box.center + 0.5f * box.size).y;
		
		for(int i=0; i<numTracks; ++i) {
			StartCoroutine(DoAudioTrack(i));
		}
		
	}
	
	void OnDestroy() {
		if (inst == this) { inst = null; }
		StopAllCoroutines();
		for(int i=0; i<numTracks; ++i) {
//			SoundController.inst.SendMute(i+1);
		}
	}
	
	public void Attach(int track, AudioObject obj) {
		// attach the given object to the given track
		if(track<numTracks){
			Debug.Log(track);
			Debug.Log (numTracks);
			trackToObject[track] = obj;

		}
	}
	
	public void Clear(int track) {
		// clear an object on the given track
		if(track<numTracks){
			trackToObject[track] = null;
		}
	}

	IEnumerator DoAudioTrack(int i) {
		
		AudioObject curr = null;
		
		// track loop
		for(;;) {
		
			// wait for an object to attach
			while(curr == null) {
				curr = trackToObject[i];
				yield return null;
			}
			
			// play sound while it's still "current"
			// ?? Do these need to be re-sent to deal with 
			//    UDP fiddlyness ??
			// ?? If so, does that belong here or in
			//    sound controller ??
			SoundController.inst.SendTrigger(i+1, curr.soundID);
			SoundController.inst.SendLoop(i+1, curr.loop);
			SoundController.inst.SendSpread(i+1, curr.spread);
			while(curr == trackToObject[i] && trackToObject[i] != null) {
				var pos = Mathf.InverseLerp(bottom, top, curr.transform.position.y);
				//var pos = curr.transform.position.y*200.0f;

				if(pos != oldPos){
				SoundController.inst.SendPosition(i+1, pos);
				}
				pos = oldPos;
				yield return new WaitForFixedUpdate();			
			}
			
			// chris would like fade out here (howto??)
			// I don't have volume control implemented, but could add it easily -M
			curr = null;
			SoundController.inst.SendMute(i+1);
			
		}
			
	}
	
	
	
	
}
