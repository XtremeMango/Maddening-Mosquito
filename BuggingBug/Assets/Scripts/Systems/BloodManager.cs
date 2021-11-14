using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodManager : MonoBehaviour
{
    float currentBlood;
    [SerializeField]
    float maxBlood;
    [SerializeField]
    float bloodConsumptionRate;
    [SerializeField]
    float startingBlood;

    private bool consume;

    private void Awake()
    {
        currentBlood = startingBlood;
    }

    private void Start()
    {
        Events.events.OnBloodRemovedFromSection += AddBlood;      
        Events.events.OnGameStateChangedToActive += StartConsumeBlood;
        Events.events.OnGameStateChangedToGameOver += StopConsumeBlood;
    }

    public IEnumerator ConsumeBlood()
    {
        while(consume)
        {
            currentBlood = Mathf.Clamp(currentBlood - bloodConsumptionRate * Time.deltaTime, 0, maxBlood);
            UIEvents.uiEvents.UpdateBloodUI(GetBloodRatio());
            if (currentBlood == 0)
            {
                Events.events.PlayerOutOfBlood();
            }
            yield return null;
        }
    }

    public void StartConsumeBlood()
    {
        consume = true;
        StartCoroutine("ConsumeBlood");
    }

    public void StopConsumeBlood()
    {
        consume = false;
        StopCoroutine("ConsumeBlood");
    }


    public void AddBlood(float amt)
    {
        currentBlood = Mathf.Clamp(currentBlood + amt, 0, maxBlood);
        UIEvents.uiEvents.UpdateBloodUI(GetBloodRatio());
        if (currentBlood == maxBlood)
        {
            //dead burst;
        }
    }

    public float GetCurrentBlood()
    {
        return currentBlood;
    }

    public float GetBloodRatio()
    {
        return currentBlood / maxBlood;
    }
}
