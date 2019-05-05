using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour {
	public float timer = 20f;
	
	// Update is called once per frame
	void Start () {
		Invoke ("returnToMenu", timer);
	}

	void returnToMenu() {
		SceneManager.LoadScene ("MainMenu");
	}
}
