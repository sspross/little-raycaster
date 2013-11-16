using UnityEngine;
using System.Collections;

public class PositionController : MonoBehaviour {

	public bool lookRight;

	float velocityX, velocityY = 0f;
	CollisionController cc;

	// Use this for initialization
	void Start () {
		cc = transform.GetComponent<CollisionController>();
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		InputCheck();
		Move();
	}

	void GetInput() {
		// Simple touch handler
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Moved) {
				velocityX = touch.deltaPosition.x;
				velocityY = touch.deltaPosition.y;
				if (velocityX > 0)
					velocityX = 1;
				if (velocityX < 0)
					velocityX = -1;
				if (velocityY > 0)
					velocityY = 1;
				if (velocityY < 0)
					velocityY = -1;
			}
		// Standard axis handler
		} else {
			velocityX = Input.GetAxisRaw("Horizontal");
			velocityY = Input.GetAxisRaw("Vertical");
		}

		// Set look direction
		if (velocityX > 0)
			lookRight = true;
		if (velocityX < 0)
			lookRight = false;
	}

	void InputCheck() {
		if (velocityY < 0 && cc.blockedDown)
			velocityY = 0;
		if (velocityY > 0 && cc.blockedUp)
			velocityY = 0;
		if (velocityX < 0 && cc.blockedLeft)
			velocityX = 0;
		if (velocityX > 0 && cc.blockedRight)
			velocityX = 0;
	}

	void Move() {
		Vector3 moveDirection = new Vector3(velocityX, velocityY, 0f);  // fix z axis
		moveDirection *= Time.deltaTime;
		transform.position += moveDirection;
	}
}
