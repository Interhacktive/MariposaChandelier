using UnityEngine;
using System.Collections;

public class disableMover : MonoBehaviour {
	public bool killable = true;
	public float autoKillTimer = 10.0f;
	public float timeLeft = 0.0f;
	float startTime = 0.0f;
	ChandelierMode currentMode;
	public int currentAnimation = 0;
	GameObject modeSelector;
	// Use this for initialization
	void Start () {
		modeSelector = GameObject.Find("Modes");
		currentAnimation = modeSelector.GetComponent<ChandelierMode>().currentAnimation;
	}
	
	// Update is called once per frame
	void Update () {
		if (killable) {
						timeLeft = (Time.time - startTime);
						if (Time.time - startTime > autoKillTimer) {
								gameObject.SetActive (false);

						}
				}
	}
	void OnEnable()
	{
		if (killable) {
						startTime = Time.time;
				}

	}

	void  OnDisable()
	{
		//transform.position = new Vector3 (0, 25, 0);
		modeSelector.GetComponent<ChandelierMode>().currentAnimation = 0;
	}
}
