using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance;

    public string nextLevelName;
    public int playerNumber;
    public Sprite background;
    public GameObject playerPrefab;

    public GameObject[] playerPlaces;

	void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else if (instance == null)
            instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
