using UnityEngine;
using System.Collections;

public class TreeGrow : MonoBehaviour {
	public Vector3 startScale;
	public Vector3 endScale;
	public Vector3 speed;
	Transform node;

	// Use this for initialization
	void Start () {
		node = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(node.localScale.y > endScale.y){
			
			gameObject.SetActive(false);
			//go back to screensaver
		}else{
			node.localScale = node.localScale + speed * Time.deltaTime;
		}
	}

	void OnEnable(){
		node = this.transform;
		node.localScale = startScale;

	}

	void OnDisable(){

	}
}
