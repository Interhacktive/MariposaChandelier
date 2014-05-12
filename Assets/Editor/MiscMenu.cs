using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MiscMenu : MonoBehaviour {

	[MenuItem("Misc/Compute Processed Lanterns")]
	static void BatchProcessLanterns() {
		
		// Iterates through the artwork in the Lanterns group, converting the
		// raw meshes from SketchUp into active game objects that can be used
		// in the simulation.
		
		// Store the original lanterns in a special isolated node
		var go = GameObject.Find("LanternsOriginal");
		
		// Load the prefab that's going to be populated
		var prefab = AssetDatabase.LoadAssetAtPath(
			"Assets/Scenes/Lantern.prefab", 
			typeof(GameObject)
		) as GameObject;
		
		// Delete previous results
		var resultName = "LanternsProcessed";
		var prevResult = GameObject.Find(resultName);
		if (prevResult) { DestroyImmediate(prevResult); }
		
		var cables = GameObject.FindObjectsOfType(typeof(Cable))
			.Select(obj => (Cable) obj)
			.ToArray();
		
		var cableToLanterns = cables
			.Select(cable => new List<Lantern>())
			.ToArray();
		
		// Populate new Results
		var resultGroup = new GameObject(resultName).transform;
		foreach(
			var r in go
				.GetComponentsInChildren<MeshRenderer>()
				.Where(r => r.sharedMaterial.name.ToLower().Trim() != "steel")
		) {
			
			// Instantiate prefab, set bounds based on mesh, and group under the result node
			var bbox = r.bounds;
			var prefabInst = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
			var node = prefabInst.transform;
			var lantern = prefabInst.GetComponent<Lantern>();
			node.position = bbox.center;
			node.parent = resultGroup;
			
			// Set the local position/size of all the LEDs
			var ledSize = bbox.size;
			ledSize.y /= 13.0f;
			for(int i=0; i<13; ++i) {
				var led = lantern.GetLED(i);
				led.localPosition = new Vector3(0, (i-6) * ledSize.y, 0);
				var bsize = 2 * ledSize; // make boxes a little bigger
				bsize.y *= 2;
				led.GetComponent<BoxCollider>().size = bsize;
				led.GetChild(0).localScale = ledSize;
			}
			
			// Determine which cable this lantern belongs to
			var idx=0;
			var cable = cables[0];
			var norm = cable.XZNorm(node);
			for(var j=1; j<cables.Length; ++j) {
				var jNorm = cables[j].XZNorm(node);
				if (jNorm < norm) {
					idx = j;
					norm = jNorm;
					cable = cables[j];
				}
			}
			
			lantern.column = cable.name;
			cableToLanterns[idx].Add(lantern);
		}
		
		// Sort lanterns top-to-bottom and assign indices
		for(int i=0; i<cables.Length; ++i) {
			var list = cableToLanterns[i];
			list.Sort((a,b) => (-a.transform.position.y).CompareTo(-b.transform.position.y));
			var cable = cables[i];
			cable.leds = new LED[13 * list.Count];
			for(int idx=0; idx<list.Count; ++idx) {
				list[idx].index = idx + 1;
				for(int j=0; j<13; ++j) {
					cable.leds[13 * idx + j] = list[idx].GetLED(j).GetComponent<LED>();
				}
			}
			
			
		}
		
		
		go.SetActive(false);
	}

	[MenuItem("Misc/Line Up Sensors")]
	static void LineUpSensors() {
	
		// Select a bunch of sensors, identity
		// the left/right nodes, and the line them
		// all up in-between along a straight line.
		
		var nodes = Selection.gameObjects
			.Select(go => go.transform)
			.ToArray();
			
		if (nodes.Length <= 2) {
			return;
		}

		Transform left=nodes[0], right=nodes[0];
		foreach(var node in nodes) {
			if (node.position.x < left.position.x) {
				left = node;
			}
			if (node.position.x > right.position.x) {
				right = node;
			}
		}
		
		var p0 = left.position;
		var p1 = right.position;
		
		var du = 1.0f / (nodes.Length-1);
		for(int i=0; i<nodes.Length; ++i) {
			nodes[i].position = Vector3.Lerp(p0, p1, du * i);
		}
		
		
	}
	
	[MenuItem("Misc/Count LEDs")]
	static void CountLEDs() {
		Debug.Log(GameObject.FindObjectsOfType(typeof(LED)).Length/13);
	}
}
