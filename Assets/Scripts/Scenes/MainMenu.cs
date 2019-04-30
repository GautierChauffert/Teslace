using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FindObjectOfType<AudioManager>().Play("MainTheme");
	}

    public void QuitApplication()
    {
        Application.Quit();
    }
}
