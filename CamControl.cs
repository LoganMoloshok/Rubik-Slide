using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {

	public Transform target;

	private float distance;	//distance from camera to target

	private float xSpeed;
	private float ySpeed;
	private float xRot;
	private float yRot;

	void Start () {
		distance = Vector3.Distance(target.position, transform.position);
		xSpeed = 30f;
		ySpeed = 80f;
	}
	
	void LateUpdate () {
		// get input when mouse button is down
		if(Input.GetMouseButton(0)) {
			xRot += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
			yRot += Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
		}

		yRot = ClampAngle(yRot, -80f, 80f);

		Quaternion newRot = Quaternion.Euler(yRot, xRot, 0);
		Vector3 negDist = new Vector3(0.0f, 0.0f, -distance);
		Vector3 newPos = newRot * negDist + target.position;

		transform.position = newPos;

		transform.LookAt(target);
	}

	private static float ClampAngle(float angle, float min, float max) {
		if(angle < -360f) {
			angle += 360f;
		}
		if(angle > 360f) {
			angle = 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}
}
