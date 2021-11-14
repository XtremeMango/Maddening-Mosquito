using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class FlyAnimationController : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    [SerializeField]
    SkinnedMeshRenderer rend;
    [SerializeField]
    GameObject explode;
    [SerializeField]
    bool titleMenu;

    private void Start()
    {
        if (!titleMenu)
        {
            Events.events.OnPlayerHit += Explode;
        }
    }

    private void Explode()
    {
        rend.enabled = false;
        Instantiate(explode, transform.position, transform.rotation);
    }

    public void SetFlyBlend(float b,float dur)
    {
        float a = anim.GetFloat("FlyAnimBlend");
        DOTween.To(() => a, x => anim.SetFloat("FlyAnimBlend", x),b,dur);
    }

    public void TriggerSuck()
    {
        anim.SetTrigger("PerformSuck");
    }


}
