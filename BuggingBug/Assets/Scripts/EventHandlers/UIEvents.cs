using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIEvents : MonoBehaviour
{
    public static UIEvents uiEvents;

    private void Awake()
    {
        uiEvents = this;
    }

    public event Action<string,string> OnUpdateDebugUIElement_String;
    public void UpdateDebugUIElement_String(string id, string val)
    {
        OnUpdateDebugUIElement_String?.Invoke(id, val);
    }

    public event Action<string, float> OnUpdateDebugUIElement_Float;
    public void UpdateDebugUIElement_Float(string id, float val)
    {
        OnUpdateDebugUIElement_Float?.Invoke(id, val);
    }

    public event Action<string, float> OnUpdateDebugUIElement_PerFrame;
    public void UpdateDebugUIElement_PerFrame(string id, float val)
    {
        OnUpdateDebugUIElement_PerFrame?.Invoke(id, val);
    }

    public event Action<float> OnUpdateSuckUIReticulePos;
    public void UpdateSuckUIReticulePos( float val)
    {
        OnUpdateSuckUIReticulePos?.Invoke(val);
    }

    public event Action<float> OnUpdateScoreUI;
    public void UpdateScoreUI(float val)
    {
        OnUpdateScoreUI?.Invoke(val);
    }

    public event Action<float> OnUpdatePointsUI;
    public void UpdatePointsUI(float val)
    {
        OnUpdatePointsUI?.Invoke(val);
    }

    public event Action<float> OnUpdateMultUI;
    public void UpdateMultUI(float val)
    {
        OnUpdateMultUI?.Invoke(val);
    }

    public event Action<float> OnUpdateBloodUI;
    public void UpdateBloodUI(float val)
    {
        OnUpdateBloodUI?.Invoke(val);
    }

    public event Action<float> OnUpdateSectionBloodUI;
    public void UpdateSectionBloodUI(float val)
    {
        OnUpdateSectionBloodUI?.Invoke(val);
    }

    public event Action<float> OnUpdateAnnoyUI;
    public void UpdateAnnoyUI(float val)
    {
        OnUpdateAnnoyUI?.Invoke(val);
    }

    public event Action OnButtonHover;
    public void ButtonHover()
    {
        OnButtonHover?.Invoke();
    }

    public event Action OnButtonClick;
    public void ButtonClick()
    {
        OnButtonClick?.Invoke();
    }

    public event Action<string> OnPlayGameUISound;
    public void PlayGameUISound(string name)
    {
        OnPlayGameUISound?.Invoke(name);
    }

    public event Action<int> OnSuckGameAttempted;
    public void SuckGameAttempted(int result)
    {
        OnSuckGameAttempted?.Invoke(result);
    }
    


}
