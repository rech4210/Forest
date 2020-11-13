using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove2 : MonoBehaviour
{
    private CharacterController cc;
    public Transform camTr;
    public float speed = 1.5f;

    void Start()
    {
        //camTr = Camera.main.GetComponent<Transform>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        cc.SimpleMove(camTr.forward * speed);
    }
}
