using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {

    public int ControllerNumber { get; private set; }
    public ControllerType ControllerType { get; private set; }
    public GameObject selected;
    public GameObject NotSelected;
    public Text playerNamePlaceHolder;
    public Image playerImage;
    public Image controllerImage;
    public Sprite keyboardSprite;
    public Sprite controllerSprite;
    public int playerNumber;


    private GameObject player;
    internal string playerName;
    private bool _ready;
    public bool justAdded;

    public void SetInformations(int controllerNumber, ControllerType controllerType, GameObject player)
    {
        this.ControllerNumber = controllerNumber;
        this.ControllerType = controllerType;
        this.Player = player;
        playerName = GameObject.FindGameObjectWithTag("LevelController").GetComponent<ChooseControllers>().GenerateNamePlayer();
        player.GetComponent<PlayerCharacteristics>().playerName = playerName;
        player.GetComponent<PlayerCharacteristics>().color = (CharacterColor)playerNumber;
        playerNamePlaceHolder.text = playerName;
        playerImage.sprite = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().playerPrefab.GetComponent<SpriteRenderer>().sprite;
        if (controllerType == ControllerType.Keyboard)
            controllerImage.sprite = keyboardSprite;
        else
            controllerImage.sprite = controllerSprite;
        selected.SetActive(true);
        NotSelected.SetActive(false);
        justAdded = true;
    }

    public GameObject Player
    {
        get
        {
            return player;
        }

        private set
        {
            player = value;
            if (ControllerNumber > 0)
            {
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.playerNumber = this.ControllerNumber;
                playerMovement.controllerType = this.ControllerType;
            }
        }
    }

    public bool Ready
    {
        get
        {
            return _ready;
        }

        set
        {
            if(value)
                GetComponent<Image>().color = new Color(0, 255, 0, 0.215f);
            else
                GetComponent<Image>().color = new Color(0, 0, 0, 0.215f);

            _ready = value;
        }
    }

    // Update is called once per frame
    void Update () {
        if(ControllerNumber > 0 && Input.GetButtonDown("J" + ControllerNumber + ControllerType + "Action"))
        {
            UnJoin();
        }
        if (!justAdded && ControllerNumber > 0 && Input.GetButtonDown("J" + ControllerNumber + ControllerType + "Jump"))
        {
            ToggleReady();
        }
        else if (justAdded && ControllerNumber > 0 && Input.GetButtonUp("J" + ControllerNumber + ControllerType + "Jump"))
        {
            justAdded = false;
        }
    }

    public void ToggleReady()
    {
        Ready = !Ready;
    }
    public void UnJoin()
    {
        ControllerNumber = 0;
        selected.SetActive(false);
        NotSelected.SetActive(true);
        player = null;
        Ready = false;
        playerNamePlaceHolder.text = "";
    }
}
