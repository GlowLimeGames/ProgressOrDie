using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCamera : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y == 135) {
			if (Input.GetMouseButtonDown (1)) {
				print ("Level 1");
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			if (transform.position.y == 0) {
				print ("Level 1");
			}
			if (transform.position.y > 0) {
				transform.position = transform.position - new Vector3 (0, 15, 0);
			}
		}
			
	}
}
