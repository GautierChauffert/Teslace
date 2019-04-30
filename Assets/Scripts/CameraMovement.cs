using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraMovement : MonoBehaviour {
    public Transform[] players;

    private Transform cameraPosition;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        cameraPosition = GetComponent<Transform>();
        offset = cameraPosition.position - players[0].position;
    }
	
	// Update is called once per frame
	void Update () {
        cameraPosition.position = new Vector3()
        {
            x = players.Select(t => t.position.x).Max() + offset.x,
            y = (players.Select(t => t.position.y).Aggregate((y1, y2) => y1 + y2) / players.Length),
            z = cameraPosition.position.z
        };
	}
}
