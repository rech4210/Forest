using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class TestMove2 : MonoBehaviour
{
    public SteamVR_Input_Sources moveType;
    public SteamVR_Action_Boolean noClipCheak;
    public SteamVR_Action_Boolean moveAtion;
    public SteamVR_Action_Boolean noClipActionUp;
    public SteamVR_Action_Boolean noClipActionDown;
    private CharacterController cc;
    public Transform camTr;
    public float speed = 1.5f;
    public bool noClip = false;

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
                speed = 3f;
                cc.SimpleMove(camTr.forward * speed);
            }
        }
        else if (noClip)
        {
            if(moveAtion.GetState(moveType))
            {
                speed = 0.2f;
                cc.Move(camTr.forward * speed);
            }
        }
    }
}
