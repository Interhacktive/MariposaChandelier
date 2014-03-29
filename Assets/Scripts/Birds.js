#pragma strict


function Start () {
	for(var sensor in FindObjectsOfType(Sensor)) {
		sensor.onSignal += OnSensorSignal;
	}
	
}


function OnSensorSignal(sensor:Sensor) {
	print("pew pew");
}