using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField]
    LineRenderer line;
    private void Update()
    {
        Ray r = new Ray(transform.position, Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(r,out hit,20f,1<<7))
        {
            line.positionCount = 2;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hit.point);
        }
        else
        {
            line.positionCount = 0;
        }

    }
}
