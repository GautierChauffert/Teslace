using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class ChooseControllers : MonoBehaviour {
    public PlayerPanel[] panelsToMap; 
    public GameObject[] players;
    public TextMeshProUGUI levelName;
    public SpriteRenderer backgroundSpriteRenderer;
    public GameObject mainPlayerPrefab;

    public string[] prefixes;
    public string[] mainWords;
    public string[] suffixes;
    public int digitNumber;

    private List<string> mappedControllers;
    GameManager gameManager;
	// Use this for initialization
	void Awake () {
        mappedControllers = new List<string>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        backgroundSpriteRenderer.sprite = gameManager.background;
        players = GameObject.FindGameObjectsWithTag("Player");
        if(players.Length < gameManager.playerNumber)
        {
            for(int i = players.Length; i < gameManager.playerNumber; i++)
            {
                Instantiate(mainPlayerPrefab);
            }
            players = GameObject.FindGameObjectsWithTag("Player");
        }
        if(players.Length > gameManager.playerNumber)
        {
            foreach (GameObject toDelete in players.Skip(gameManager.playerNumber))
                Destroy(toDelete);
        }
	}

    void Start ()
    {
        levelName.text = gameManager.nextLevelName;
        foreach(GameObject gameObjectToDelete in panelsToMap.Skip(gameManager.playerNumber).Select(p => p.gameObject))
        {
            Destroy(gameObjectToDelete);
        }
        panelsToMap = panelsToMap.Take(gameManager.playerNumber).ToArray();
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 1; i <= 4; i++)
        {
            foreach (ControllerType controllerType in (ControllerType[])System.Enum.GetValues(typeof(ControllerType)))
            {
                if(Input.GetButtonDown("J" + i + controllerType + "Jump") && !panelsToMap.Any(p => p.ControllerNumber == i && p.ControllerType == controllerType))
                {
                    Debug.Log("Input" + i + " for controller " + controllerType + " is pushed.");
                    mappedControllers.Add(controllerType.ToString() + i);
                    PlayerPanel panel = panelsToMap.FirstOrDefault(p => p.ControllerNumber == 0);
                    if (panel != null)
                    {
                        panel.SetInformations(i, controllerType, players.Where(p => !panelsToMap.Any(pa => pa.Player == p)).First());
                    }
                        
                    else
                        Debug.Log("No more player controller to assign");
                }
            }
        }

        if (!panelsToMap.Any(p => !p.Ready))
            GoToLobby();
    }

    void GoToLobby()
    {
        gameManager.playerPlaces = panelsToMap.Select(p => p.Player).ToArray();
        SceneManager.LoadScene(gameManager.nextLevelName);
    }

    public string GenerateNamePlayer()
    {
        string generatedName;
        System.Random rnd = new System.Random();
        do
        {
            generatedName = String.Concat(prefixes[rnd.Next(prefixes.Length)], mainWords[rnd.Next(mainWords.Length)], suffixes[rnd.Next(suffixes.Length)], rnd.Next(10 ^ digitNumber));
        } while (panelsToMap.Any(p => p.playerName == generatedName));
        return generatedName;
    }
}
