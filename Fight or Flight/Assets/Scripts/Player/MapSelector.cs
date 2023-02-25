using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelector : MonoBehaviour
{
    public List<GameObject> maps = new List<GameObject>();
    public static MapSelector instance;
    private void Awake()
    {
        if (!instance)
            instance = this;
    }

}
