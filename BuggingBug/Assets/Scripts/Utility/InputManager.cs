using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Events.events.LeftMBClicked();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Events.events.RightMBClicked();
        }
    }

   
}
