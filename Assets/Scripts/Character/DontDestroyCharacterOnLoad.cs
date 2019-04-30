using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCharacterOnLoad : MonoBehaviour {

	void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
