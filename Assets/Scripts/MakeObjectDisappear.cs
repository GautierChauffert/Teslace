using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MakeObjectDisappear : MonoBehaviour {
    public GameObject[] objectsToDisappearJoueurUp;
    public GameObject[] objectsToDisappearJoueurDown;

    public float timeToChange = 2f;
    public float timeToDisappear = 0.5f;
    public int pourcentToShow = 10;
    public GameObject[] showedObjectsUp;
    public GameObject[] showedObjectsDown;

    float lastChange = 0;
    System.Random random = new System.Random();
    void Start()
    {
        UpdatePositions();
    }
	// Update is called once per frame
	void Update () {
		if((Time.time - lastChange) > timeToChange)
        {
            UpdatePositions();
        }
	}

    void UpdatePositions()
    {
        DeactivateRandom(objectsToDisappearJoueurUp);
        DeactivateRandom(objectsToDisappearJoueurDown);
        lastChange = Time.time;
    }

    void DeactivateRandom(GameObject[] gameObjects)
    {
        int[] rnds = GetSeveralRandomNumbers(0, gameObjects.Length, (int)((float)gameObjects.Length * (float)pourcentToShow/(float)100));
        StartCoroutine(Disappear(gameObjects.Where(g => rnds.Contains(Array.IndexOf(gameObjects, g)))));

        Appear(gameObjects.Where(g => !rnds.Contains(Array.IndexOf(gameObjects, g))));

    }

    IEnumerator Disappear(IEnumerable<GameObject> gameObjectToDisappear)
    {
        
        float timeToDisappearLeft = timeToDisappear;
        while (timeToDisappearLeft > 0 )
        {
            timeToDisappearLeft -= Time.deltaTime;
            Color color = new Color(1f, 1f, 1f, (timeToDisappearLeft / timeToDisappear));
            foreach(GameObject game in gameObjectToDisappear)
                game.GetComponent<SpriteRenderer>().color = color;

            yield return null;
        }
        foreach (GameObject game in gameObjectToDisappear)
            game.SetActive(false);
    }

    void Appear(IEnumerable<GameObject> gameObjectsToAppear)
    {
        foreach(GameObject gameObjectToAppear in gameObjectsToAppear)
        {
            gameObjectToAppear.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            gameObjectToAppear.SetActive(true);
        }
    }

    int[] GetSeveralRandomNumbers(int min, int max, int numberWanted)
    {
        int[] randoms = new int[numberWanted];
        int randomToAdd;
        randoms = randoms.Select(r => -1).ToArray();
        for(int i = 0; i < numberWanted; i++)
        {
            do
            {
                randomToAdd = random.Next(min, max);
            } while (randoms.Contains(randomToAdd));

            randoms[i] = randomToAdd;
        }

        return randoms;
    }
}
