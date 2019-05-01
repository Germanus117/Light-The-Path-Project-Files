using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycast : MonoBehaviour {
	public enemyDetect detection;

	void Update () {
		int layerMask = 1 << 12;
//		layerMask = ~layerMask;
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f, layerMask)) {
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
			Debug.Log ("raycast");
			detection.RaycastDetect();
		}
	}
}
