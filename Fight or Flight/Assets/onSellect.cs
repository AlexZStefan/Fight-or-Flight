using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class onSellect : MonoBehaviour, ISelectHandler
{

    // Start is called before the first frame update
    public Image mapFrame;
    public Sprite mapSprite;
   
    public void OnSelect(BaseEventData eventData)
    {
        mapFrame.sprite = mapSprite;
    }
}
