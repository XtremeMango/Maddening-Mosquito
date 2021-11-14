using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManLookController : MonoBehaviour
{
    [SerializeField]
    GameObject AnimTarget;
    [SerializeField]
    GameObject player;
    [SerializeField]
    float projectionPlaneDist = -10f;

    bool followPlayer;

    private void Start()
    {
        followPlayer = true;
        Events.events.OnGameStateChangedToGameOver += WatchAndLookAway;
    }

    private void WatchAndLookAway()
    {
        StartCoroutine("WatchThenLookForward");
    }

    public IEnumerator WatchThenLookForward()
    {
        float t = 0;
        while (t <= 5f)
        {
            t += Time.deltaTime;
            yield return null;
        }
        followPlayer = false;
        AnimTarget.transform.DOMove(new Vector3(0f, 27f, -10f), 1f).SetEase(Ease.OutBack);
        yield return null;
    }

    private void Update()
    {
        if (followPlayer)
        {
            AnimTarget.transform.position = OffsetPlayerPositionZ(projectionPlaneDist);
        }
    }

    private Vector3 OffsetPlayerPositionZ(float offset)
    {
        Vector3 playerPos = player.transform.position;
        playerPos.z += offset;
        return playerPos;
    }
}
