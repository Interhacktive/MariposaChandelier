using UnityEngine;
using System.Collections;

public class panelColorer : MonoBehaviour {
	 public bool destroyObject = true;
	public float destroyTime = 20.0f;
	Color previousColor;
	public Color myColor = new Color(255,0,0,150);
	public Color emisColor = new Color(255,0,0,150);
	
	void OnTriggerEnter(Collider other) {
		//if(gameObject.CompareTag("LampSection")){
      	//previousColor = other.renderer.material.color; 
		previousColor = new Color(0.0f,0.0f,0.0f,	0.25f);
		other.renderer.material.color = myColor;
		other.renderer.material.SetColor("_Emission", emisColor);
		//}
    }
	
	void OnTriggerExit(Collider other){
		
		other.renderer.material.color = previousColor;
		other.renderer.material.SetColor("_Emission", previousColor);
	}
	
	void OnCollisionEnter(Collision collision) {
	//GetComponent<AudioSource>().Play();	
		 if (collision.relativeVelocity.magnitude > 2){
		//audio.Play();
	}
	}
	
	// Use this for initialization
	void Start () {
		if(destroyObject){
		Destroy(gameObject, destroyTime);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
