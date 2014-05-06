using UnityEngine;

public class TintCollider : MonoBehaviour {
	
	// Tint colliders are meshes which "tint" lanterns 
	// when they come into contact.
	
	public Color tintColor;
	public float rate=0.2f;
	public bool destroyAutomatically = true;
	internal Collider c;
	public float originalRate = 0.2f;	
	
	void Awake() {
		gameObject.tag = "TintCollider";
		var r = this.renderer;
		if (r) {
			tintColor = r.sharedMaterial.color;
			tintColor.a = 1.0f;
		}
		c = this.collider;

	}
	
	void OnTriggerExit(Collider c) {
	
		// Destroy this object when it leaves the simulation bounds.
		if (destroyAutomatically && c.name == "SimulationBounds" || transform.position.y < 0) {
			Destroy(gameObject);
		}
	}

	void Update(){
		var r = this.renderer;
		if (r) {
			tintColor = r.sharedMaterial.color;
			tintColor.a = 1.0f;
		}
		if (destroyAutomatically && transform.position.y < 0) {
			Destroy(gameObject);
		}
	}
	void OnDisable(){
		originalRate = rate;
		rate = 0.0f;
	}

	void OnEnable(){
		rate = originalRate;
	}
}
