using UnityEngine;
using System.Collections;

public class SunMover : MonoBehaviour {
	public Vector3 startPosition;
	public Vector3 endPosition;
	public Vector3 speed;
	Transform node;
	public bool movingUp = true;
	// Use this for initialization
	void Start () {
		node = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(node.position.y > endPosition.y && movingUp){
			
			gameObject.SetActive(false);
			//go back to screensaver
		}else{
			node.position = node.position + speed * Time.deltaTime;
		}

		if(node.position.y < endPosition.y && !movingUp){
			
			gameObject.SetActive(false);
			//go back to screensaver
		}else{
			node.position = node.position + speed * Time.deltaTime;
		}
	}

	void OnEnable(){
		node = this.transform;
		node.position = startPosition;

		
	}

}
