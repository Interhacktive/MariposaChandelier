using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {
	public GameObject[] tail;
	public int tailLength = 1;
	public Color headColor = Color.green;
	public Color tailColor = Color.blue;
	public Vector3 startPosition;
	public Vector3 endPosition;
	public Vector3 speed;
	Transform node;
	public GameObject resetCube;
	// Use this for initialization
	void Start () {
		node = this.transform;
		//resetCube = transform.Find ("ResetCube").gameObject;
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
		tailLength = transform.childCount;
		tail = new GameObject[tailLength];
	
		for(int i = 0;i<tailLength;i++){
			float tailSection = i/(float)tailLength;
			tail[i] = transform.GetChild(i).gameObject;
			tail[i].renderer.material.color = Color.Lerp(headColor, tailColor,tailSection );
		}
		renderer.material.color = headColor;

	}

	void OnDisable(){
		//resetCube.SetActive(true);
	}
}



