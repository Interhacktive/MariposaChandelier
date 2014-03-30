using UnityEngine;
using System.Collections;

public class AudioObject : MonoBehaviour
{

	// Audio object just endows an object with a sound ID.
	// Audio objects can be "attached" the the AudioTrackController
	// to cause an audio sample to actually play.

	// Designers can attach colliders to audio objects if they'd like them
	// to interact with AudioTrackColliders, or else they can be attached
	// dynamically in scripts due to any other kind of content logic.

	public int soundID = 0;
	public bool loop = false;
	public float spread = 0;

	AudioObject curr = null;
	int curTrack = 0;
	void Start ()
	{
		int tracks = AudioTrackController.inst.numTracks;
		if(AudioTrackController.currentTrack >= tracks-1){
			AudioTrackController.currentTrack = 0;
		} else{
			AudioTrackController.currentTrack++;

		}
		curTrack = AudioTrackController.currentTrack;
			//if (AudioTrackController.inst.trackToObject [curTrack] != curr) {
				AudioTrackController.inst.Attach (curTrack, this);
	//	}

		}


}