using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayCast : MonoBehaviour
{
    RayCast RC;
    RaycastHit hit;
    float MaxDistance = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.Log("hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 1000f, Color.blue);
        }
    }
}
