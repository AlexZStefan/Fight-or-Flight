using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public GameObject languageUI;
    void Start()
    {
        StartCoroutine(HideScreen());   
    }
    IEnumerator HideScreen()
    {       
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
