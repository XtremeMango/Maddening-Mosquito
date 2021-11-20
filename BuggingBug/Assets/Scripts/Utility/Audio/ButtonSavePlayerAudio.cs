using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSavePlayerAudio : MonoBehaviour, IPointerClickHandler
{
    BGMusicController BGAudio;

    public void OnPointerClick(PointerEventData eventData)
    {
        BGAudio.SaveVolume();
    }

    private void Awake()
    {
        BGAudio = GameObject.Find("PersistAudioManager").GetComponent<BGMusicController>();
    }
    
}
