#pragma strict

var durationSeconds : float = 5;
var distance : float = 15;

function easeOut(u:float) {
	u = 1.0 - u;
	return 1.0 - u*u*u*u;
}

function Start () {
	var simBounds = GameObject.Find("SimulationBounds").transform;
	var box = simBounds.GetComponent(BoxCollider);
	var top = simBounds.position.y + box.center.y + 0.5 * box.size.y;
	
	var node = transform;
	var p0 = node.position;
	for(var time = 0.0; time<durationSeconds; time+=Time.deltaTime) {
		var u = easeOut(time / durationSeconds);
		node.position = p0 + Vector3(0, u*distance, 0);
		yield;
	}
}

function OnDrawGizmos() {
	Gizmos.color = Color.grey;
	var p0 = transform.position;
	var p1 = p0 + Vector3(0, distance, 0);
	Gizmos.DrawLine(p0, p1);
	Gizmos.DrawLine(p1, p1 + 0.3 * Vector3(-1, -1, 0));
	Gizmos.DrawLine(p1, p1 + 0.3 * Vector3(1, -1, 0));
}