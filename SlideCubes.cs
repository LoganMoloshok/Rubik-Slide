using UnityEngine;
using System.Collections;

public class SlideCubes : MonoBehaviour {

	private Vector3 pos;  //use for the current position when sliding
	public Vector3 originalPosition;
	private bool correctPosition;

	//private float speed = 0.5f;
	private Vector3 target;

	public GameObject slot;

	private bool over;

	private bool moving = false;


	void Start () {
		//register original position to check later
		originalPosition = transform.position;
	}

	void Update() {
		if(moving) {
			transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 5.0f);
			if(transform.position == target) {
				moving = false;
				slot.transform.position = pos;
			}
		}
		
	}
	
	private void OnMouseEnter() {
		over = true;
	}

	private void OnMouseExit() {
		over = false;
	}

	void OnMouseUp() {
		if(over) {
			Slide();
		}
	}

	public void Slide() {
	
		pos = transform.position; // current position

		//if we're adjacent to the open slot
		if (Vector3.Distance(transform.position, slot.transform.position) == 1)
		{
			//	transform.position = slot.transform.position;
			//	slot.transform.position = pos;
			target = slot.transform.position;
			moving = true;
		}
	}

	public void InstantSlide() {
		pos = transform.position; // current position
		//if we're adjacent to the open slot
		if (Vector3.Distance(transform.position, slot.transform.position) == 1)
		{
			transform.position = slot.transform.position;
			slot.transform.position = pos;
		}
	}

	public bool CheckCorrect() {
		if(transform.position == originalPosition) {
			correctPosition = true;
		}
		else {
			correctPosition = false;
		}

		return correctPosition;
	}
}
