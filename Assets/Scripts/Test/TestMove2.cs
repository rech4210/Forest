using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class TestMove2 : MonoBehaviour
{
    public SteamVR_Input_Sources moveType;
    public SteamVR_Action_Boolean noClipCheak;
    public SteamVR_Action_Boolean moveAtion;
    private CharacterController cc;
    public Transform camTr;
    public float speed = 1.5f;
    public bool noClip = false;

    public SteamVR_Action_Boolean moveLeft;
    public SteamVR_Action_Boolean moveRight;
    public SteamVR_Action_Boolean moveForward;
    public SteamVR_Action_Boolean moveBack;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (noClipCheak.GetStateDown(moveType))
        {
            noClip = !noClip;
        }

        if (noClip == false)
        {
            if (moveAtion.GetState(moveType))
            {
                cc.SimpleMove(camTr.forward * speed * 1.5f);
            }
            speed = 5f;
            if (moveForward.GetState(moveType))
            {
                cc.SimpleMove(camTr.forward * speed);
            }
            if (moveRight.GetState(moveType))
            {
                cc.SimpleMove(camTr.right * speed);
            }
            if (moveLeft.GetState(moveType))
            {
                cc.SimpleMove((camTr.right * -1) * speed);
            }
            if (moveBack.GetState(moveType))
            {
                cc.SimpleMove((camTr.forward * -1) * speed);
            }
        }
        else if (noClip)
        {
            if(moveAtion.GetState(moveType))
            {
                cc.Move(camTr.forward * speed);
            }
            speed = 0.2f;
            if (moveForward.GetState(moveType))
            {
                cc.Move(camTr.forward * speed);
            }
            if (moveRight.GetState(moveType))
            {
                cc.Move(camTr.right * speed);
            }
            if (moveLeft.GetState(moveType))
            {
                cc.Move((camTr.right * -1) * speed);
            }
            if (moveBack.GetState(moveType))
            {
                cc.Move((camTr.forward * -1) * speed);
            }

        }
        

    }
}