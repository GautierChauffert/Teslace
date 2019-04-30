using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevertControllersOnCollision : MonoBehaviour {
    AlphaLevelController controller;

    void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("LevelController").GetComponent<AlphaLevelController>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        controller.AssignPlayersDefault();
    }
}
