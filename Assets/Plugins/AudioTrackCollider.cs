using UnityEngine;
using System.Collections;

public class AudioTrackCollider : MonoBehaviour {

	// Waits for a collision with an audio object and then 
	// attaches it to a designer-determined audio track.
	public int audioTrack = 0;
	
	AudioObject curr = null;
	
	void OnTriggerEnter(Collider c) {
		var obj = c.GetComponent<AudioObject>();
		if (obj && obj != curr) {
			curr = obj;
			AudioTrackController.inst.Attach(audioTrack, curr);
		}
	}
	
	void OnTriggerExit(Collider c) {
		if (curr != null) {
			var obj = c.GetComponent<AudioObject>();
			if (curr == obj) {
				curr = null;
				AudioTrackController.inst.Clear(audioTrack);
			}	
		}
	}
	
	
	
}
