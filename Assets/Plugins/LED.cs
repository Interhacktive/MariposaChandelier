using System.Collections.Generic;
using UnityEngine;

public class LED : MonoBehaviour {
	
	const float FadeOutRate = 0.15f;
	
	#if UNITY_EDITOR
	Mesh mesh;
	#endif
	internal Color currentColor = Color.black;
	Color targetColor = Color.black;
	float fadeRate = FadeOutRate;
	Color32[] colors;
	List<Collider> overlaps = new List<Collider>();
	GameObject lastObject;
	void Awake() {
		
		#if UNITY_EDITOR
		// Aquire a unique copy of the mesh
		mesh = GetComponentInChildren<MeshFilter>().mesh; // implicit dup
		mesh.MarkDynamic();
		colors = new Color32[mesh.vertexCount];
		
		// intialize my instance-mesh's color
		Color32 cc = targetColor;
		for(int i=0; i<colors.Length; ++i) {
			colors[i] = cc;
		}
		mesh.colors32 = colors;
		#endif
		
		
	}
	
	void SetColor(Color c) {	
		// Set the target color.  We will fade-to this in the unity display.	
		targetColor = c;
	}
	
	void FixedUpdate() {
		if(lastObject != null){
		if (ColorsApprox(currentColor, targetColor) || !lastObject.activeSelf) {
			// we've achieved our target color - is it time to fade out
			//if (overlaps.Count == 0) {
			targetColor = Color.black;
			fadeRate = FadeOutRate;
			//}
		} else {
			// we're still fading.
			currentColor = EaseTowards(currentColor, targetColor, fadeRate);
			#if UNITY_EDITOR
			Color32 cc = currentColor;
			for(int i=0; i<colors.Length; ++i) {
				colors[i] = cc;
			}
			mesh.colors32 = colors;			
			#endif
		}
		}
		
	}
	
	void OnTriggerEnter(Collider c) {
		if (c.CompareTag("TintCollider")) {
			var tc = c.GetComponent<TintCollider>();
			if(tc != null){
				SetColor(tc.tintColor);
				fadeRate = tc.rate;
				lastObject = c.gameObject;
			}
		}
		/*
		// Look for collisions with a TintCollider
		if (c.CompareTag("TintCollider")) {
			if (!overlaps.Contains(c)) {
				overlaps.Add (c);
				var tc = c.GetComponent<TintCollider>();
				SetColor(tc.tintColor);
				fadeRate = tc.rate;
			}
			lastObject = c.gameObject;
		}
		*/
	}
	void OnTriggerStay(Collider c){
		if (c.CompareTag("TintCollider")) {
			var tc = c.GetComponent<TintCollider>();
			if(tc != null){
			SetColor(tc.tintColor);
			fadeRate = tc.rate;
			lastObject = c.gameObject;
			}
		}
	}
	void OnTriggerExit(Collider c) {
		
		if (c.CompareTag("TintCollider")) {
			var tc = c.GetComponent<TintCollider>();
			if(tc != null){
				SetColor(tc.tintColor);
				fadeRate = tc.rate;
				lastObject = c.gameObject;
			}
		}
		/*
		// Look for the TintCollider Exitting
		if (c.CompareTag("TintCollider") && overlaps.Contains(c)) {
			overlaps.Remove(c);
			if (overlaps.Count > 0) {
				var tc = overlaps[0].GetComponent<TintCollider>();
				SetColor(tc.tintColor);
				fadeRate = tc.rate;
			}
		}
		*/
		
	}
	
	static bool ColorsApprox(Color c1, Color c2) {
		var dr = (c1.r - c2.r);
		var dg = (c1.g - c2.g);
		var db = (c1.b - c2.b);
		return dr*dr + dg*dg + db*db < 0.05f * 0.05f;
		
	}
	
	static Color EaseTowards(Color curr, Color target, float easing) {
		// Utility for nice ease-out fading
		var result = Color.Lerp(curr, target, Mathf.Pow(easing, Mathf.Clamp01(40f * Time.fixedDeltaTime)));
		return ColorsApprox(result, target) ? target : result;
	}
	
}
