using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Lobby : MonoBehaviour {
    public string levelName;
    public GameObject[] playersPhysique;
    public Actionneur[] actionneurs;
    GameManager gameManager;

    [SerializeField]
    private TMP_Text timeLeft;
    [SerializeField]
    private GameObject loadingText;
    private float timer = 0.0f;
    public float timeToWait = 5.0f;
    void Awake()
    {
        try
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        } catch
        {
            Debug.Log("Aucun gameManager associé.");
        }
        // On associe les joueurs avec les caractéristiques physiques.
        for (int i = 0; i < Mathf.Min(playersPhysique.Length, gameManager.playerPlaces.Length); i++)
        {
            playersPhysique[i].transform.parent = gameManager.playerPlaces[i].transform;
            CharacterController2D characterController2D = playersPhysique[i].GetComponent<CharacterController2D>();
            gameManager.playerPlaces[i].GetComponent<PlayerMovement>().CharacterController = characterController2D;
        }
    }
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (!actionneurs.Any(a => !a.IsOn) && gameManager != null)
        {
            timer += Time.deltaTime;
            int timeLeftInt = (int)(timeToWait - timer);
            timeLeft.text = timeLeftInt.ToString();
            // On rajoute des infos bidons
            if (timeLeftInt < 1)
            {
                StartCoroutine(LoadSceneAsync());


                for (int j = 0; j < actionneurs.Length; j++)
                {
                    gameManager.playerPlaces[j] = actionneurs[j].ObjectDoingAction.transform.parent.gameObject;
                    DeleteChild(gameManager.playerPlaces[j].transform);
                }

            }
        }
        else if (timer != 0.0f)
        {
            timeLeft.text = "";
            timer = 0.0f;
        }
    }
    private void DeleteChild(Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private IEnumerator LoadSceneAsync()
    {
        
        if (loadingText != null)
            loadingText.SetActive(true);
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        
    }
}
