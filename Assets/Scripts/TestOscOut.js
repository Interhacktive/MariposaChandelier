#pragma strict

import MakingThings;

private var osc : OscController;

function Start() {
	osc = GameObject.FindObjectOfType(OscController);
}

function Update () {
	
	if (Input.GetMouseButton(0)) {
		print("Testing OSC Out...");
		var msg = new OscMessage();
		msg.Address = "hello/from/unity";
		msg.Values.Add(Mathf.PI);	
		osc.Client.Send(msg);
	}
}