using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor_Move : MonoBehaviour
{
    public float speed = 10;
    public float upDownPower = 10;

    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        if(xMove != 0 || zMove != 0)
        {
            Vector3 position = transform.position;
            position.x += xMove * speed * Time.deltaTime;
            position.z += zMove * speed * Time.deltaTime;
            transform.position = position;
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            Vector3 position = transform.position;
            position.y += upDownPower * Time.deltaTime;
            transform.position = position;
        }
        else if(Input.mouseScrollDelta.y < 0)
        {
            Vector3 position = transform.position;
            position.y -= upDownPower * Time.deltaTime;
            transform.position = position;
        }
    }
}
