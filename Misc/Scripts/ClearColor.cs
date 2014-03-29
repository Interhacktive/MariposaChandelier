using UnityEngine;
using System.Collections;

public class ClearColor : MonoBehaviour {
	public GameObject lamp;
	public Color blankColor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	 //	lamp = GameObject.FindWithTag("LampSection");
		//lamp.renderer.material.color = blankColor;
	//	lamp.renderer.material.SetColor("_Emission", blankColor);
		/*
		GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("LampSection");
		
		  foreach (GameObject go in gos) {
			go.renderer.material.color = blankColor;
			go.renderer.material.SetColor("_Emission", blankColor);
		}
		*/
	}
}
