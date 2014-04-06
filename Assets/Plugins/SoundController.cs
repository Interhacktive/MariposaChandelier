using System.Collections;
using MakingThings;
using UnityEngine;

public class SoundController : MonoBehaviour {
	
	// act like a singleton
	public static SoundController inst = null;
	
	// Designers can specify a specific OSC controller or just let it find one
	// by default.
	public OscController oscController;
	
	// API Constants
	const int IdAll = 0;
	const int SndIdMute = 0;
	
	Osc osc;
	
	void Awake() {
		inst = this;
	}
	
	void OnDestroy() {
		if (inst == this) { inst = null; }
	}
	
	void Start() {
		// find osc controller if an explicit one was not specified, first by checking
		// the current game objects
		if (oscController == null) {
			oscController = GetComponent<OscController>();
		}
		// Otherwise, search the whole scene
		if (oscController == null) {
			oscController = FindObjectOfType(typeof(OscController)) as OscController;
		}
		// if we found and OSC controller, snag a reference to it's impl
		if (oscController != null) {
			osc = oscController.Client;
		}
		
	}
	
	public void SendTrigger(int id, int sndId) {
		var msg = new OscMessage() { Address = "/trigger/"+id };
		msg.Values.Add(sndId);
		Send(msg);
	}
	
	public void SendMute(int id) {
		SendTrigger(id, SndIdMute);
	}
	
	public void SentToAll(int sndId) {
		SendTrigger(IdAll, sndId);
	}
	
	public void MuteAll() {
		SendTrigger(IdAll, SndIdMute);
	}
	
	public void SendPosition(int id, float pos) {
		var msg = new OscMessage() { Address = "/position/"+id };
		msg.Values.Add(Mathf.Clamp01(pos));
		Send(msg);
	}
	
	public void SendSpread(int id, float spread) {
		var msg = new OscMessage() { Address = "/spread/"+id };
		msg.Values.Add(Mathf.Clamp01(spread));
		Send(msg); 
	}
	
	public void SendLoop(int id, bool loop) {
		var msg = new OscMessage() { Address = "/loop/"+id };
		msg.Values.Add(loop ? 1 : 0);
		Send(msg);
	}
	
	void Send(OscMessage msg) {
/*		Debug.Log(string.Format(
			"Sending Sound Message: {0} {1}", 
			msg.Address, 
			msg.Values[0]
		));
		*/
		// If we can't send messages, then NOOP
		if (osc != null) {
			osc.Send(msg);
		}
	}
	
	
}
