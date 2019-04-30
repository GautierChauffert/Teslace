using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {
    public static bool isPaused;
    public GameObject panel;
    public Text godModeText;
    bool godMode;
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
	}

    public void Resume()
    {
        panel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        panel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void GodMode()
    {
        godMode = !godMode;
        godModeText.text = (godMode ? "Désa" : "A") + "ctiver GodMode";
        foreach(GameObject playerPhysic in GameObject.FindGameObjectsWithTag("PlayerPhysic"))
        {
            playerPhysic.GetComponent<Health>().godMode = godMode;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("LevelController").GetComponent<LifeBarManager>().timeLeft = 0;
    }
}
