#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;

public class Cable : MonoBehaviour {
	public int universe;
	public LED[] leds;
	internal Lantern[] lan;
	short univ;
	internal byte[] workingBuffer;
	
	void Awake() {
		univ = System.Convert.ToInt16(universe);
		workingBuffer = new byte[3 * leds.Length];
		//Transform lamp = transform.Find("Lantern");
//		lan = lamp.GetComponents<Lantern>();
		/*
		for(int i=0;i<130;i++){

			if(gameObject.name.CompareTo(lan[i].column) != 0){
				Debug.Log ("found the cable");
				//leds[lamp.GetComponents<Lantern>().index*13] = lamp.GetComponent<Lantern>().GetLED(i).GetComponent<LED>();

			}
			/*
			if(lamp.GetComponents<Lantern>() == gameObject.name){
					}
			*/

		//}

	}
	
	internal void WriteWorkingBuffer() {
		for(int i=0; i<leds.Length; ++i) {
			Color32 c = leds[i].currentColor;
			workingBuffer[i+i+i  ] = c.r;
			workingBuffer[i+i+i+1] = c.g;
			workingBuffer[i+i+i+2] = c.b;
		}
	}
	
	public void SendPacket() {
		if (DmxController.inst) {
			// populate working buffer
			WriteWorkingBuffer();
			
			// dispatch to dmx
			DmxController.inst.Send(univ, workingBuffer, workingBuffer.Length);
		}		
	}
	
	// This class is only used during lantern import to assign
	// letter-labels to each lantern
	
	void OnDrawGizmosSelected() {
		// draw a line and a label when selected
		Gizmos.color = Color.grey;
		var p = transform.position;
		Gizmos.DrawLine(p, new Vector3(p.x, 0, p.z));
		#if UNITY_EDITOR
		Handles.Label(p, name);
		#endif
	}
	
	public float XZNorm(Transform node) {
		// Distance-sq in the horizontal plane
		var delta = node.position - transform.position;
		return delta.x * delta.x + delta.z * delta.z;		
	}	
	
	public bool Approx(Transform node, float distance=0.15f) {
		// Is the fiven node within distance meters of this cable in the
		// XZ (horizontal) plane?
		return XZNorm(node) < distance * distance;
	}
	
}
