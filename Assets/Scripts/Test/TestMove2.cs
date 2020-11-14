using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class TestMove2 : MonoBehaviour
{
    public SteamVR_Input_Sources moveType;
    public SteamVR_Action_Boolean moveAtion;
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
        if (moveAtion.GetState(moveType))
        {
            cc.SimpleMove(camTr.forward * speed);
        }
    }
}
