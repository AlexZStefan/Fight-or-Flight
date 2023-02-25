using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{

    Light light;

    float timer = 2; 
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.5)
        {
            light.intensity = 0;

            if (timer <= 0 )

                timer = Random.Range(2, 4);
        }
        else
            light.intensity = 3.5f;

       
    }
}
