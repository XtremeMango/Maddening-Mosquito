using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManManager : MonoBehaviour
{
    [SerializeField]
    float annoyanceDecayRate;
    [SerializeField]
    Vector2[] annoyanceLevelData;
    float annoyanceLevel = 0f;
    float currentAnnoyanceSeverity = 1;
    ManSection currentSection;
    bool decay;
    bool attack;
    bool attacking;

    private void Start()
    {
        Events.events.OnMounted += InitialIncrease;
        Events.events.OnDismount += StartDecayAnnoyanceAmount;
        Events.events.OnSuckAttempted += AddAnnoyance;
        Events.events.OnSuckAttempted += SuckAttack;
        Events.events.OnGameStateChangedToActive += ManCanAttack;
        Events.events.OnAttackComplete += resetAttack;
        Events.events.OnGameStateChangedToGameOver += GameOver;
    }

    private void SuckAttack()
    {
        if (!attacking)
        {
            RandomChanceAttack(10f);
        }
    }

    private float GetAnnoyRatio()
    {
        return annoyanceLevel / annoyanceLevelData[annoyanceLevelData.Length - 1].x;
    }

    private void GameOver()
    {
        attack = false;
        StopCoroutine("DecayAnnoyanceAmount");
        AnimEvents.events.UpdateAnimBlend("Annoyance", 0, true);
    }

    private void resetAttack()
    {
        attacking = false;
    }

    private void ManCanAttack()
    {
        attack = true;
        StartCoroutine("ManChanceAttack");
    }

    public IEnumerator ManChanceAttack()
    {
        while(attack && !attacking)
        {
            if(annoyanceLevel == 0)
            {

            }
            else
            {
                RandomChanceAttack(100f);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void RandomChanceAttack(float maxChanceRange)
    {
        float chance = UnityEngine.Random.Range(0f, maxChanceRange);
        if (currentAnnoyanceSeverity > chance)
        {
            Events.events.ManAttack();
            attacking = true;
        }
    }
    private void AddAnnoyance()
    {
        IncreaseAnnoyanceLevel(currentSection.data.annoyanceValue);
    }

    private void InitialIncrease(ManSection section)
    {
        currentSection = section;
        IncreaseAnnoyanceLevel(currentSection.data.annoyanceValue / 2f);
    }

    public Vector2 GetAnnoyanceData()
    {
        return new Vector2(annoyanceLevel,currentAnnoyanceSeverity);
    }

    public void IncreaseAnnoyanceLevel(float amount)
    {
        annoyanceLevel += amount;
        UIEvents.uiEvents.UpdateAnnoyUI(GetAnnoyRatio());
        AnimEvents.events.UpdateAnimBlend("Annoyance", 0.25f + 0.75f *(annoyanceLevel / 100f),true);
        for (int i = 1; i < annoyanceLevelData.Length; i++)
        {
            if (annoyanceLevel < annoyanceLevelData[i].x)
            {
                currentAnnoyanceSeverity = annoyanceLevelData[i - 1].y;
                break;
            }
        }
        //Annoyance over max, instant death
    }

    public void StartDecayAnnoyanceAmount()
    {
        decay = true;
        StartCoroutine("DecayAnnoyanceAmount");
    }

    public IEnumerator DecayAnnoyanceAmount()
    {
        while (decay)
        {
            annoyanceLevel = Mathf.Clamp(annoyanceLevel - annoyanceDecayRate * Time.deltaTime, 0, float.MaxValue);
            UIEvents.uiEvents.UpdateAnnoyUI(GetAnnoyRatio());
            yield return null;
        }
    }
}
