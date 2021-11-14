using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoExplodeController : MonoBehaviour
{
    Transform[] parts;
    List<Rigidbody> rbs;
    bool exploded;
    private void OnEnable()
    {
        exploded = false;
        parts = GetComponentsInChildren<Transform>();
        rbs = new List<Rigidbody>();
        foreach (Transform t in parts)
        {
            rbs.Add( t.gameObject.AddComponent<Rigidbody>());    
        }
    }

    private void FixedUpdate()
    {
        if(!exploded)
        {
            foreach (Rigidbody rigidbody in rbs)
            {
                rigidbody.AddForce(Random.insideUnitSphere.normalized * 100f);
                rigidbody.AddTorque(Random.insideUnitSphere.normalized * 100f);
            }
            StartCoroutine("DestroyAfter");
            exploded = true;
        }
    }

    public IEnumerator DestroyAfter()
    {
        float t = 0;
        while (t <=10f)
        {
            t += Time.deltaTime;
            yield return null;
        }
        for (int i = 0; i < parts.Length; i++)
        {
            Destroy(parts[i].gameObject);
        }
    }
}
