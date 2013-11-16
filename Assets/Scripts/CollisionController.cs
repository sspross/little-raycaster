using UnityEngine;
using System.Collections;

public class CollisionController : MonoBehaviour {

	public bool blockedRight, blockedLeft, blockedUp, blockedDown = false;

	float fuzzyEdgeFactor = 0.01f;  // if 0 edge on edge connection would set two blocks true
	float fuzzySkinFactor = 1.1f;
	bool showRaycasts = true;

	float halfSelf, edgeCorrection;
	Vector3 selfTop, selfBottom, selfRight, selfLeft;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		halfSelf = transform.collider.bounds.size.x / 2;
		edgeCorrection = transform.collider.bounds.size.x * fuzzyEdgeFactor;  
	}
	
	// Update is called once per frame
	void Update () {
		CheckRaycasts();
	}

	void CheckRaycasts() {
		// Update position values
		selfTop = new Vector3(transform.position.x, transform.position.y + halfSelf - edgeCorrection, transform.position.z);
		selfBottom = new Vector3(transform.position.x, transform.position.y - halfSelf + edgeCorrection, transform.position.z);
		selfRight = new Vector3(transform.position.x + halfSelf - edgeCorrection, transform.position.y, transform.position.z);
		selfLeft = new Vector3(transform.position.x - halfSelf + edgeCorrection, transform.position.y, transform.position.z);

		// Check different directions
		CheckRight();
		CheckLeft();
		CheckUp();
		CheckDown();
	}

	void CheckRight() {
		Vector3 direction = transform.TransformDirection(Vector3.right);
		if (showRaycasts) {
			Debug.DrawRay(selfTop, direction * halfSelf * fuzzySkinFactor, Color.cyan);
			Debug.DrawRay(selfBottom, direction * halfSelf * fuzzySkinFactor, Color.cyan);
		}
		if (Physics.Raycast(selfTop, direction, halfSelf * fuzzySkinFactor) || Physics.Raycast(selfBottom, direction, halfSelf * fuzzySkinFactor)) {
			blockedRight = true;
		} else {
			blockedRight = false;
		}
	}

	void CheckLeft() {
		Vector3 direction = transform.TransformDirection(Vector3.left);
		if (showRaycasts) {
			Debug.DrawRay(selfTop, direction * halfSelf, Color.cyan);
			Debug.DrawRay(selfBottom, direction * halfSelf, Color.cyan);
		}
		if (Physics.Raycast(selfTop, direction, halfSelf) || Physics.Raycast(selfBottom, direction, halfSelf)) {
			blockedLeft = true;
		} else {
			blockedLeft = false;
		}
	}

	void CheckUp() {
		Vector3 direction = transform.TransformDirection(Vector3.up);
		if (showRaycasts) {
			Debug.DrawRay(selfRight, direction * halfSelf, Color.cyan);
			Debug.DrawRay(selfLeft, direction * halfSelf, Color.cyan);
		}
		if (Physics.Raycast(selfRight, direction, halfSelf) || Physics.Raycast(selfLeft, direction, halfSelf)) {
			blockedUp = true;
		} else {
			blockedUp = false;
		}
	}

	void CheckDown() {
		Vector3 direction = transform.TransformDirection(Vector3.down);
		if (showRaycasts) {
			Debug.DrawRay(selfRight, direction * halfSelf, Color.cyan);
			Debug.DrawRay(selfLeft, direction * halfSelf, Color.cyan);
		}
		if (Physics.Raycast(selfRight, direction, halfSelf) || Physics.Raycast(selfLeft, direction, halfSelf)) {
			blockedDown = true;
		} else {
			blockedDown = false;
		}
	}
}
