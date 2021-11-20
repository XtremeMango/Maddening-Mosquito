using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    UIAudioManager uiAudio;
    public bool confirmOnClick;

    private void Awake()
    {
        uiAudio = GameObject.Find("PersistAudioManager").GetComponent<UIAudioManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(confirmOnClick)
        {
            uiAudio.PlayClip("Confirm");
        }
        else
        {
            uiAudio.PlayClip("Deny");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiAudio.PlayClip("Hover");
    }  
    
}
