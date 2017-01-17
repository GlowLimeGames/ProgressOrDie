using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashTriggers : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (SplashTime ());
	}

	private IEnumerator SplashTime()
	{
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene (1);
	}
}
