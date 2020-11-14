using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y < 1)
        {
            //Debug.Log("Destroy");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Destroy");
        Destroy(gameObject);
    }
}
