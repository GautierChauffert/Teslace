using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckpointManager : MonoBehaviour {
    private int resetCount = 0;

    public bool autoFind = true;

    public CheckPointPart[] CheckPointParts;
    public CheckPointPart activeCheckPoint;
    public Vector3[] playersInitialPosition;
    public GameObject[] playersPhysic;


    private bool resetOnGoing = false;
    public int ResetCount
    {
        get
        {
            return resetCount;
        }

        set
        {
            resetCount = value;
        }
    }

    public bool IsResseting
    {
        get
        {
            return resetOnGoing;
        }
    }
    // Use this for initialization
    void Start () {
        if(autoFind)
            FindChecksPoint();
        // On définit le point de démarrage.
        playersPhysic = GameObject.FindGameObjectsWithTag("PlayerPhysic");

        playersInitialPosition = new Vector3[playersPhysic.Length];
        for(int i = 0; i < playersPhysic.Length; i++)
        {
            playersInitialPosition[i] = playersPhysic[i].transform.position;
        }

	}

    public void GetPlayersBackToLastCheckpoint()
    {
        if (resetOnGoing)
            return;
        resetOnGoing = true;
        if (activeCheckPoint != null && activeCheckPoint.crossedBy.Length > 0 && !activeCheckPoint.crossedBy.Any(p => p == null))
        {
            ResetCount++;
            StartCoroutine(playDeath(activeCheckPoint.crossedBy));
        }
        else
        {
            for (int i = 0; i < playersPhysic.Length; i++)
            {
                
                ResetPlayer(playersPhysic[i], playersInitialPosition[i]);
            }
            resetOnGoing = false;
        }
    }
    public void PassCheckpoint(GameObject checkPointcrossed, GameObject player)
    {
        int checkPointPassedIndex = -1;
        CheckPointPart checkPointPart = CheckPointParts.FirstOrDefault(c => (checkPointPassedIndex = Array.IndexOf(c.checkPoints, checkPointcrossed)) > -1);
        checkPointPart.crossedBy[checkPointPassedIndex] = player;

        if (!checkPointPart.crossedBy.Any(x => x == null) && activeCheckPoint != checkPointPart)
        {
            activeCheckPoint = checkPointPart;
        }
    }
    private static void ResetPlayer(GameObject player, Vector3 respawnPoint)
    {
        if (player == null)
            return;
        player.transform.position = respawnPoint;
        ResetTransform(player.transform);
    }
    /// <summary>
    /// Permet de remettre la zone à reset à 0
    /// </summary>
    private void ResetMap()
    {
        FindAllChidsAndExecuteReset(activeCheckPoint.partToReset);
    }
    private void FindChecksPoint()
    {
        GameObject[] resettableParts = GameObject.FindGameObjectsWithTag("ChallengeReset");
        CheckPointParts = new CheckPointPart[resettableParts.Length];
        int i = 0;
        foreach (GameObject resetPart in resettableParts)
        {
            List<GameObject> checkPoints = FindAllCheckpointsInGameObject(resetPart);
            CheckPointParts[i] = new CheckPointPart
            {
                partToReset = resetPart,
                checkPoints = checkPoints.ToArray(),
                crossedBy = new GameObject[checkPoints.Count]
            };
            i++;
        }
    }
    private void FindAllChidsAndExecuteReset(GameObject gameObject)
    {
        if(gameObject != null)
        {
            foreach(Transform childTransform in gameObject.transform)
            {
                if(childTransform.childCount > 0)
                {
                    FindAllChidsAndExecuteReset(childTransform.gameObject);
                }

                ResetTransform(childTransform);
            }
        }
    }
    private List<GameObject> FindAllCheckpointsInGameObject(GameObject gameObject)
    {
        List<GameObject> checkPoints = new List<GameObject>();
        if (gameObject != null)
        {
            foreach(Transform childTransform in gameObject.transform)
            {
                if(childTransform.childCount > 0)
                {
                    checkPoints.AddRange(FindAllCheckpointsInGameObject(childTransform.gameObject));
                }

                if (childTransform.tag == "Checkpoint")
                    checkPoints.Add(childTransform.gameObject);
            }
        }
        return checkPoints;
    }

    private static void ResetTransform(Transform transform)
    {
        foreach (Resettable ressetableObject in transform.GetComponents<Resettable>())
        {
            ressetableObject.Reset();
        }
    }

    private IEnumerator playDeath(GameObject[] players)
    {
        foreach(GameObject player in players)
        {
            if (player == null)
                yield break;
            Animator localAnim = player.GetComponent<Animator>();
            if(localAnim != null)
                localAnim.Play("Death");
        }

        yield return new WaitForSeconds(0.4f);
        int i = 0;
        ResetMap();
        foreach (GameObject player in activeCheckPoint.crossedBy)
        {

            ResetPlayer(player, activeCheckPoint.checkPoints[i].transform.position);
            i++;
        }
        Debug.Log("Fin du reset");
        yield return new WaitForSeconds(0.2f);
        resetOnGoing = false;
    }
}
[System.Serializable]
public class CheckPointPart
{
    public GameObject partToReset;
    public GameObject[] checkPoints;
    public GameObject[] crossedBy;
}