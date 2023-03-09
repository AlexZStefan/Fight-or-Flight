using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InGameCharacters : MonoBehaviour
{      
    public List<GameObject> characters = new List<GameObject>();
    public static InGameCharacters instance;
    private void Awake()
    {
        if (!instance)
            instance = this;
    }
}
