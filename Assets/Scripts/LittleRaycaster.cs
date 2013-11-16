using UnityEngine;
using System.Collections;

public class LittleRaycaster : MonoBehaviour {

	public bool blockedRight = false;

	RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 rightOfSelf = transform.TransformDirection(Vector3.right);
		float halfSelf = transform.collider.bounds.size.x / 2;

		Vector3 selfTop = new Vector3(transform.position.x, transform.position.y + halfSelf, transform.position.z);
		Vector3 selfBottom = new Vector3(transform.position.x, transform.position.y - halfSelf, transform.position.z);
		Debug.DrawRay(selfTop, rightOfSelf * halfSelf, Color.red);
		Debug.DrawRay(selfBottom, rightOfSelf * halfSelf, Color.red);

		if (Physics.Raycast(selfTop, rightOfSelf, halfSelf) || Physics.Raycast(selfBottom, rightOfSelf, halfSelf)) {
			blockedRight = true;
		} else {
			blockedRight = false;
		}
	}
}
