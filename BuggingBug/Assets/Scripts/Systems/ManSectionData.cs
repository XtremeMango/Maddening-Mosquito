using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ManSectionData : ScriptableObject
{
    public string location;
    public float pointValue;
    public float annoyanceValue;
    public float regenRate;
    public float maxBlood;
}
