using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManSection : MonoBehaviour
{
    public ManSectionData data;

    float currentBloodAvailable;

    bool regen;
    bool active;
    bool mounted;

    private void Awake()
    {
        currentBloodAvailable = data.maxBlood;
        regen = false;
        mounted = false;
        active = false;
    }

    private void Start()
    {
        Events.events.OnPointsCaptured += RemoveBlood;
        Events.events.OnDismount += ToggleMounted;
    }

    public void SetActive()
    {
        active = true;
    }

    public void ToggleMounted()
    {
        if(mounted && active)
        {
            mounted = false;
            active = false;
            StartRegen();
        }
        else
        {
            if(active)
            {
                mounted = true;
                StopRegen();
            }
        }
    }

    public void StartRegen()
    {
        regen = true;
        StartCoroutine("Regen");
    }

    public void StopRegen()
    {
        regen = false;
        StopCoroutine("Regen");
    }

    public IEnumerator Regen()
    {
        while(regen)
        {
            currentBloodAvailable = Mathf.Clamp(currentBloodAvailable + data.regenRate * Time.deltaTime, 0, data.pointValue);
            if (currentBloodAvailable == data.pointValue)
            {
                StopRegen();
            }
            yield return null;
        }
    }

    public bool HasBlood()
    {
        bool result = false;
        if (currentBloodAvailable > 0)
        {
            result = true;
        }
        else
        {
            //Event for no blood!
        }
        return result;
    }

    public void RemoveBlood(float points, float lvl)
    {
        if (active)
        {
            float bloodRemoved = Mathf.Clamp(lvl, 0, currentBloodAvailable);
            currentBloodAvailable = currentBloodAvailable - bloodRemoved;
            UIEvents.uiEvents.UpdateSectionBloodUI(GetBloodAmountRatio());
            Events.events.BloodRemovedFromSection(bloodRemoved);
        }
    }

    public float GetBloodAmountRatio()
    {
        return currentBloodAvailable / data.maxBlood;
    }
}
