#pragma strict

var speed = 1.0;
private var seed : Vector2;

private var p0 : Vector3;
private var p1 : Vector3;

private var prog = 0.0;

function Start () {
	
	seed = 256 * Vector2(Random.value-0.5f, Random.value-0.5f);	

	var simBounds = GameObject.Find("SimulationBounds").GetComponent(BoxCollider);
	p0 = simBounds.transform.TransformPoint(simBounds.center - 0.4f * simBounds.size);
	p1 = simBounds.transform.TransformPoint(simBounds.center + 0.4f * simBounds.size);
	
	SetPosition(0);
	
}

function FixedUpdate() {
	prog += speed * Time.fixedDeltaTime;
	SetPosition(prog);
}

function SetPosition(u:float) {
	var ux = u / (p1.x - p0.x);
	var uy = u / (p1.y - p0.y);
	var uz = u / (p1.z - p0.z);
	rigidbody.MovePosition(Vector3(
		Mathf.Lerp(p0.x, p1.x, Mathf.PerlinNoise(seed.x+32, seed.y+ux)),
		Mathf.Lerp(p0.y, p1.y, Mathf.PerlinNoise(seed.x, seed.y+uy)),
		Mathf.Lerp(p0.z, p1.z, Mathf.PerlinNoise(seed.x-32, seed.y+uz))
	));
}
