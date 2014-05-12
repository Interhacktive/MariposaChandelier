#pragma strict

var rainDropPrefab : GameObject;
var avgPeriod = 0.35;
private var nextDrop : RainDrop;


function Expovariate(dt : float) {
	// a random duration of time that "averages out" over the long-term
	return -dt * Mathf.Log(1.0 - Random.Range(0.0000001, 0.999999));
}

function Start () {
	var cables = GameObject.FindObjectsOfType(Cable);
	
	for(;;) {
		yield WaitForSeconds( Expovariate(avgPeriod) );
		
		var idx = Random.Range(0, cables.Length);
		var cable : Cable = cables[idx];
		
		// Instantiate a new drop only if we don't have an idle one
		// that we can reuse.		
		if (nextDrop == null) {			
			Instantiate(
				rainDropPrefab, 
				cable.transform.position,
				Quaternion.identity
			);
		} else {
			var drop = nextDrop;
			nextDrop = nextDrop.next;
			drop.Reuse(cable.transform.position);
			drop.gameObject.SetActive(true);
		}
	}
}

function DropFinished(drop:RainDrop) {
	drop.next = nextDrop;
	nextDrop = drop;
	drop.gameObject.SetActive(false);
}

//function Update() {
//	Debug.Log(Application.targetFrameRate);
//}