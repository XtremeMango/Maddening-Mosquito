using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManAnimController : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    float blendTime = 0.5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        AnimEvents.events.OnUpdateAnimBlend += UpdateAnimBlend;
        AnimEvents.events.OnTriggerAnim += TriggerAnim;
        anim.SetFloat("Annoyance", 0.25f);
    }

    private void TriggerAnim(string name)
    {
        anim.SetTrigger(name);
    }

    private void UpdateAnimBlend(string name, float val, bool blend)
    {
        if(blend)
        {
            DOTween.To(() => anim.GetFloat(name), x => anim.SetFloat(name, x), val, blendTime).SetEase(Ease.InOutQuad);
        }
        else
        {
            anim.SetFloat(name, val);
        }
    }
}
