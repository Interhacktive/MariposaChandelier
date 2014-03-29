#pragma strict

private var speed = 0.0;
var maxSpeed = 10.0;
private var body : Rigidbody;
private var p : Vector3;
private var rain : Rain;

@System.NonSerialized
var next : RainDrop = null;

function Start () {
	rain = FindObjectOfType(Rain);
	body = rigidbody;
	p = body.position;
}

function Reuse(pos:Vector3) {
	speed = 0;
	p = pos;
	transform.position = pos;
	body.position = pos;
}

function FixedUpdate () {
	speed += 8 * Time.fixedDeltaTime;
	speed = Mathf.Min(speed, maxSpeed);
	p -= Vector3(0, speed * Time.fixedDeltaTime, 0);
	if (p.y < 0) {
//		rain.DropFinished(this);
	} else {
		body.MovePosition(p);
	}
}

function OnDrawGizmos() {
	var node = transform;
	var box = GetComponent(BoxCollider);
	Gizmos.color = Color.grey;
	Gizmos.DrawWireCube(
		node.TransformPoint(box.center), 
		node.TransformDirection(box.size)
	);
}