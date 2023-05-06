using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FModEvents : MonoBehaviour
{
    [field: Header("UI SFX")]
    [field: SerializeField] public EventReference ok { get; private set; }
    [field: SerializeField] public EventReference back { get; private set; }
    [field: SerializeField] public EventReference charSelect { get; private set; }
    [field: SerializeField] public EventReference mapSelect { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference footStep { get; private set; }
    [field: SerializeField] public EventReference jumpSound { get; private set; }
    [field: SerializeField] public EventReference punch { get; private set; }
    [field: SerializeField] public EventReference kick { get; private set; }
    [field: SerializeField] public EventReference block { get; private set; }
    [field: SerializeField] public EventReference readyFight { get; private set; }

    [field: Header("Ambient SFX")]
    [field: SerializeField] public EventReference openWindZone { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference MenuMusic { get; private set; }
    [field: SerializeField] public EventReference CharSelectMusic { get; private set; }
    [field: SerializeField] public EventReference InGameMusic { get; private set; }
    
    public static FModEvents instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
    
    }   
}
