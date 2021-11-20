using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandController : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject handL;
    [SerializeField]
    GameObject handR;
    [SerializeField]
    float readyTime = 1f;
    [SerializeField]
    float anticipationDelay = 1f;
    [SerializeField]
    float slamTime = 1f;
    [SerializeField]
    float recoveryTime = 1f;

    [SerializeField]
    Vector3 standbyLoc_L;
    [SerializeField]
    Vector3 standbyLoc_R;
    [SerializeField]
    Vector3 readyLoc_L;
    [SerializeField]
    Vector3 readyLoc_R;
    [SerializeField]
    Vector3 slamLoc_L;
    [SerializeField]
    Vector3 slamLoc_R;

    Sequence attackSequenceL;
    Sequence attackSequenceR;
    float[] angles;

    bool hit;

    public void Awake()
    {
        angles = new float[3] { 0, 45, 90};
        handL.transform.localPosition = standbyLoc_L;
        handR.transform.localPosition = standbyLoc_R;
    }

    private void Start()
    {
        Events.events.OnManAttack += Attack;
        Events.events.OnPlayerHit += Hit;
    }

    private void Hit()
    {
        hit = true;
    }

    public void Attack()
    {
        float angle = angles[Random.Range(0, 2)];
        switch (angle)
        {
            case 0f:
                transform.position = Vector3.up * player.transform.position.y;
                transform.rotation = Quaternion.Euler(0, 0, angle);
                break;
            case 45f:
                transform.position = new Vector3(player.transform.position.x,player.transform.position.y,0);
                transform.rotation = Quaternion.Euler(0, 0, angle);
                break;
            case 90f:
                transform.position = Vector3.right * player.transform.position.x;
                transform.rotation = Quaternion.Euler(0, 0, angle);
                break;
            default:
                break;
        }
        
        attackSequenceL = DOTween.Sequence();
        attackSequenceR = DOTween.Sequence();
        attackSequenceL.AppendCallback(() => Events.events.PlayManSound("ReadyAttack"))
           .Append(handL.transform.DOLocalMove(readyLoc_L, readyTime))
           .Append(handL.transform.DOShakePosition(anticipationDelay)).SetEase(Ease.Linear)
           .AppendCallback(() => AnimEvents.events.TriggerAnim("Attack"))
           .AppendCallback(() => Events.events.PlayManSound("AttackEffort"))
           .AppendCallback(() => Events.events.PlayManSound("AttackWoosh"))
           .Append(handL.transform.DOLocalMove(slamLoc_L, slamTime)).SetEase(Ease.InQuart)
           .AppendCallback(() => Events.events.StopManSound("AttackWoosh"))
           .AppendCallback(() => Events.events.PlayManSound("AttackClap"))
           .AppendCallback(() => Events.events.ManClap())
           .AppendCallback(() => Grunt())
           .Append(handL.transform.DOLocalMove(standbyLoc_L, recoveryTime)).SetEase(Ease.InOutQuart);
           

        attackSequenceR.Append(handR.transform.DOLocalMove(readyLoc_R, readyTime))
           .Append(handR.transform.DOShakePosition(anticipationDelay)).SetEase(Ease.Linear)
           .Append(handR.transform.DOLocalMove(slamLoc_R, slamTime)).SetEase(Ease.InQuart)
           .Append(handR.transform.DOLocalMove(standbyLoc_R, recoveryTime)).SetEase(Ease.InOutQuart)
           .OnComplete(()=>Events.events.AttackComplete());
    }

    public void Grunt()
    {
        if (!hit)
        {
            Events.events.PlayManSound("Grunt");
        }
    }

}
