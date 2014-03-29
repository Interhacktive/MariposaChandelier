using UnityEngine;
using System.Collections;

public class MoveADirection : MonoBehaviour {

	public Vector3 speed;
	
	// cached references
	Transform node;
	
	void Awake() {
		node = this.transform;
	}
	
	void Update() {
		node.position = node.position + speed * Time.deltaTime;
	}

}
