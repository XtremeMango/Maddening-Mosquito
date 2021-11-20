using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMusicSlider : MonoBehaviour
{
    Slider slider;
    BGMusicController bgmusic;
    private void Awake()
    {
        
    }

    private void Start()
    {
        slider = GetComponent<Slider>();
        bgmusic = GameObject.FindGameObjectWithTag("PersistAudio").GetComponent<BGMusicController>();
        slider.value = bgmusic.main.volume;
        slider.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    public void ValueChanged()
    {
        bgmusic.SetVol(slider.value);
    }

    
}
