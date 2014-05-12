using UnityEngine;
using System.Collections;

public class ResetCube : MonoBehaviour {
	public Vector3 startPosition;
	public Vector3 endPosition;
	public Vector3 speed;
	Transform node;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(node.position.y > endPosition.y){
			
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

	void OnDisable(){

	}

}
