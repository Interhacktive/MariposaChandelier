using System.Collections;
using MakingThings;
using UnityEngine;

public class SensorOscDelegate : MonoBehaviour {
	
	public Sensor[] sensors;
	
	void OnOscMessage(OscMessage msg) {
		
		// forward message signals to sensor instances
		for(int i=0; i<msg.Values.Count; ++i) {
			if (i < sensors.Length) {
				sensors[i].Signal((int) msg.Values[i]);
			}
		}
		
	}
	
}
