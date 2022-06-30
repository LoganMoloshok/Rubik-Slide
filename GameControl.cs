using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public GameObject[] cubes;	//array of cube objects in the puzzle
	public GameObject slot;		//the empty slot object
	public Text shuffleText;    //ui text for the slider
	public Text winText;
	public float numShuffle = 1f; //number of times to shuffle puzzle, default 1

	private bool running = false;

	void Update() {
		if(running) {
			CheckWin();
		}
	}

		//based on numShuffle, iterate through cube array and move every cube if it can be moved
	public void ShuffleCubes() {

		ResetPuzzle();
		ShuffleArray();

		for (int i=0; i<numShuffle; i++) {
			foreach (GameObject go in cubes) {
				SlideCubes tScrip = go.GetComponent(typeof(SlideCubes)) as SlideCubes;
				tScrip.InstantSlide();
			}
		}

		running = true;
	}

		// puts everything back to its original position
	public void ResetPuzzle() {

		running = false;
		winText.text = "";

		foreach(GameObject go in cubes) {
			SlideCubes tScrip = go.GetComponent(typeof(SlideCubes)) as SlideCubes;
			Vector3 rPos = tScrip.originalPosition;
			go.transform.position = rPos;
		}
		slot.transform.position = new Vector3(0, 1, 0);
	}

		//tied to the UI slider, dynamically changes number of times to shuffle and updates text
	public void SetShuffle(float newNum) {
		numShuffle = newNum;
		shuffleText.text = numShuffle.ToString();
	}

	private void ShuffleArray() {
		Debug.Log("Shuffle Array");
		int pFrom = cubes.Length-1; //number we can pick from
		float picked;
		GameObject temp;

		for(; pFrom > 1; pFrom--) {
			//pick a random element
			picked = Mathf.Floor(Random.value * pFrom);
			//swap that element with the last one
			temp = cubes[(int)picked];
			cubes[(int)picked] = cubes[pFrom];
			cubes[pFrom] = temp;
		}
	}

	public void CheckWin() {
		int correct = 0;
		int wrong = 0;
		foreach(GameObject go in cubes) {
			SlideCubes tScrip = go.GetComponent(typeof(SlideCubes)) as SlideCubes;
			if (tScrip.CheckCorrect()) {
				correct++;
			}
			else {
				wrong++;
			}
		}

		if(wrong < 1) {
			//Debug.Log("Puzzle Solved!");
			winText.text = "Puzzle complete!";
			running = false;
		}

	}
}
