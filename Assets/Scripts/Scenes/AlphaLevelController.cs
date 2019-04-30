using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaLevelController : MonoBehaviour {
    public GameObject[] playerPhysic;
    public string themeName;
    GameManager gameManager;


    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        AssignPlayersDefault();
    }
    void Start()
    {
        FindObjectOfType<AudioManager>().Play(themeName);
    }

    public void AssignPlayersDefault()
    {
        for (int i = 0; i < Mathf.Min(gameManager.playerPlaces.Length, playerPhysic.Length); i++)
        {
            playerPhysic[i].transform.parent = gameManager.playerPlaces[i].transform;
            CharacterController2D characterController2D = playerPhysic[i].GetComponent<CharacterController2D>();
            gameManager.playerPlaces[i].GetComponent<PlayerMovement>().CharacterController = characterController2D;
        }
    }
}