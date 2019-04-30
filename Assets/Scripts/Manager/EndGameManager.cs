using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EndGameManager : Resettable {

    private GameObject levelController;
    private GameObject[] playersPhysics;
    private float timer = 0;
    public PlayerEndTime[] players;
    public GameObject winnerObjet;
    public string DefaultText;
    private GameObject winner;
    private bool hasAWinner = false;
    public GameObject Winner
    {
        get
        {
            return winner;
        }

        set
        {
            winner = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController");
        playersPhysics = GameObject.FindGameObjectsWithTag("PlayerPhysic");
        players = playersPhysics.Select(p => new PlayerEndTime
        {
            playersPhysic = p,
            timer = 0
        }).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasAWinner)
        {
            timer = +Time.deltaTime;
            if (players.Any(p => !p.hasReachedEnd))
                return;

            FindWinner();
        }
    }


    void OnCollisionEnter2D(Collision2D collider)
    {
        EnterEndZone(collider.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        EnterEndZone(collider.gameObject);
    }

    private void EnterEndZone(GameObject gameObject)
    {
        if (gameObject.CompareTag("PlayerPhysic"))
        {
            foreach (PlayerEndTime player in players)
            {
                if (gameObject == player.playersPhysic)
                {
                    player.timer = timer;
                    player.hasReachedEnd = true;
                }
            }
        }
    }

    void FindWinner()
    {
        hasAWinner = true;
        float timemin = 10000000;
        foreach(PlayerEndTime player in players)
        {
            int death = player.playersPhysic.GetComponent<Health>().DeathCount;
            float timetotal = player.timer + (float) 0.5 * death;
            if(timemin>timetotal){
                timemin = timetotal;
                Winner = player.playersPhysic;
            }
        }
        levelController.GetComponent<LifeBarManager>().timeLeft = 10;
        if (winnerObjet != null)
        {
            ShowWinner();
        }
    }

    public void ShowWinner()
    {
        winnerObjet.SetActive(true);
        string name = winner.GetComponentInParent<PlayerCharacteristics>().playerName;
        Text displayWinner = winnerObjet.GetComponentInChildren<Text>();
        if (displayWinner == null)
            displayWinner = GetComponent<Text>();
        if (string.IsNullOrEmpty(DefaultText))
            displayWinner.text = name + " a gagné la partie!";
        else
            displayWinner.text = DefaultText;
    }

    public override void Reset()
    {
        foreach(PlayerEndTime player in players)
        {
            player.timer = 0;
            player.hasReachedEnd = false;
        }

        winnerObjet.SetActive(false);
    }
}

[System.Serializable]
public class PlayerEndTime
{
    public GameObject playersPhysic;
    public float timer;
    public bool hasReachedEnd = false;
}
