#pragma strict

import MakingThings;

function Start() {
	var osc = GameObject.FindObjectOfType(OscController);
	osc.onMessage += OnOscMessage;
}

function OnOscMessage(msg:OscMessage) {
	print("Received Message: " + msg.Address + " " + msg.Values.Count);
}

