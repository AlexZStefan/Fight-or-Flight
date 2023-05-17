using System.Collections.Generic;
using UnityEngine;

public class MapSelector : MonoBehaviour
{
    public static MapSelector instance;

    public List<string> keys = new List<string>();
    public List<GameObject> values = new List<GameObject>();
    public Dictionary<string, GameObject> map = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (instance) return;
        instance = this;

        CreateMap();
    }
    private void CreateMap()
    {
        map.Clear();
        if (keys.Count != values.Count) return;

        for (int i = 0; i < keys.Count; i++)
        {
            map.Add(keys[i], values[i]);
        }
    }
}
