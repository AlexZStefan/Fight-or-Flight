using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject languageUI;
    void Start()
    {
        StartCoroutine(HideScreen());   
    }
    IEnumerator HideScreen()
    {
       
         yield return new WaitForSeconds(2);

         gameObject.SetActive(false);
        languageUI.transform.localScale = new Vector3(1, 1, 1);
    }
}
