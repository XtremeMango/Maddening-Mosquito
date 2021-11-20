using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Hand")
        {
            Events.events.PlayerHit();
            Events.events.PlayManSound("Splat");
        }
    }
}
