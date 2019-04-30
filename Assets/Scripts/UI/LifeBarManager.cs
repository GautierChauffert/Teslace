using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class LifeBarManager : MonoBehaviour {

    public Slider[] lifeBarObjects;
    public List<LifeBarPlayer> lifeBarPlayers = new List<LifeBarPlayer>();
    public Text[] deathCounterObjects;
    public Text[] joueurPosition;
    public float timeLeft = 1200f;
    public Text timer;
    
    
    GameManager gameManager;



    void Awake ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        AssignHealth();
        AssignPlayer();
    }

    void AssignHealth()
    {
        for (int i = 0; i < Mathf.Min(gameManager.playerPlaces.Length, lifeBarObjects.Length); i++)
        {
            lifeBarPlayers.Add(new LifeBarPlayer
            {
                lifeBar = lifeBarObjects[i],
                deathCounter = deathCounterObjects[i],
                playerHealth = gameManager.playerPlaces[i].transform.GetComponentInChildren<Health>()
            });
        }
    }

    void AssignPlayer()
    {
        for (int i = 0; i < Mathf.Min(gameManager.playerPlaces.Length, joueurPosition.Length); i++)
        {
            joueurPosition[i].text = gameManager.playerPlaces[i].GetComponent<PlayerCharacteristics>().playerName;
        }
    }

    void Update () {
        foreach(LifeBarPlayer lifeBarPlayer in lifeBarPlayers)
        {
            lifeBarPlayer.deathCounter.text = "Mort " + lifeBarPlayer.playerHealth.DeathCount;
            lifeBarPlayer.lifeBar.value = lifeBarPlayer.playerHealth.LifeLeft;
        }
        if (timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
            timer.text = "" + (int)timeLeft;
        }
        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        foreach (GameObject playerPlaces in GameObject.FindGameObjectsWithTag("PlayerPhysic"))
        {
            Destroy(playerPlaces);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
[SerializeField]
public class LifeBarPlayer
{
    public Slider lifeBar { get; set; }
    public Text deathCounter { get; set; }
    public Health playerHealth { get; set; }
}
