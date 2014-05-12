using UnityEngine;
using System.Collections;

public class ChandelierMode : MonoBehaviour {
	/// <summary>
	/// Chandelier can only be playing one animation at a time, otherwise it may seem chaotic. 
	/// </summary>
	// Use this for initialization


	//0 = screenSaver (orbs)
	//1 = Sunrise
	//2 = Sunset
	//3 = chocolate tree
	//4 = rain //never used
	//5 = thunder/lighting
	//6 = serpentine 
	//7 = birds
	//8 = bats
	//9 = none

	public int currentAnimation = 0;
	int prevValue = 1;
	public GameObject[] animation;
	public GameObject resetCube;
	/*
	public GameObject screenSaver;
	public GameObject sunRise;
	public GameObject sunSet;
	public GameObject chocolateTree;
	public GameObject rain;
	public GameObject thunder;
	public GameObject snake;
	public GameObject birds;
	public GameObject bats;
*/

	public void Start () {
	
	
	}
	
	// Update is called once per frame
	public void Update () {
				if (prevValue != currentAnimation) {
			resetCube.SetActive(true);
						for (int i = 0; i<9; i++) {
								animation [i].SetActive (false);
						}
						animation [currentAnimation].SetActive (true);
			for(int c = 0;c< animation[currentAnimation].transform.childCount;c++){
				animation[currentAnimation].transform.GetChild(c).gameObject.SetActive(true);

			}
	
				
	
						
				}
		prevValue = currentAnimation;
		}
}
