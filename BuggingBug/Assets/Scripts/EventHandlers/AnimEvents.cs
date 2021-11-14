using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimEvents : MonoBehaviour
{
    public static AnimEvents events;

    private void Awake()
    {
        events = this;
    }

    public event Action<string,float,bool> OnUpdateAnimBlend;
    public void UpdateAnimBlend(string name, float val, bool blend)
    {
        OnUpdateAnimBlend?.Invoke(name, val, blend);
    }

    public event Action<string> OnTriggerAnim;
    public void TriggerAnim(string name)
    {
        OnTriggerAnim?.Invoke(name);
    }

    

}
