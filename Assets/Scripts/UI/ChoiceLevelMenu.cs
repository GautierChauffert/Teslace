using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceLevelMenu : MonoBehaviour {
    public int numberPlayers;
    public Sprite background;
    public GameObject playerPrefab;
    public void SetNumberPlayers(int numberPlayers)
    {
        this.numberPlayers = numberPlayers;
    }

    public void SetPlayerPrefab(GameObject playerPrefab)
    {
        this.playerPrefab = playerPrefab;
    }
    public void SetBackground(Sprite background)
    {
        this.background = background;
    }
    public void GoToChooseControllers(string levelName)
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GameManager gameManagerComponent = gameManager.GetComponent<GameManager>();
        gameManagerComponent.nextLevelName = levelName;
        gameManagerComponent.playerNumber = numberPlayers;
        gameManagerComponent.playerPrefab = playerPrefab;
        gameManagerComponent.background = background;
        SceneManager.LoadScene("ChooseControllers");
    }
}
