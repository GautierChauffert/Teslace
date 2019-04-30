using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {
    private CheckpointManager checkpointManager;

    void Awake()
    {
        checkpointManager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("PlayerPhysic"))
        {
            checkpointManager.PassCheckpoint(gameObject, collider.gameObject);
        }
    }
}
