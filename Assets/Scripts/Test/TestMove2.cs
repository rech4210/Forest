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
    public static bool walk = false;

    public SteamVR_Action_Boolean moveLeft;
    public SteamVR_Action_Boolean moveRight;
    public SteamVR_Action_Boolean moveForward;
    public SteamVR_Action_Boolean moveBack;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        StartCoroutine(moveCast());
    }

    void Update()
    {
        if (noClipCheak.GetStateDown(moveType))
        {
            noClip = !noClip;
        }
    }


    public IEnumerator moveCast()
    {
        do
        {
            if (noClip == false)
            {
                if (moveAtion.GetState(moveType))
                {
                    cc.SimpleMove(camTr.forward * speed * 1.5f);
                    walk = true;
                }
                speed = 5f;
                if (moveForward.GetState(moveType))
                {
                    cc.SimpleMove(camTr.forward * speed);
                    walk = true;
                }
                if (moveRight.GetState(moveType))
                {
                    cc.SimpleMove(camTr.right * speed);
                    walk = true;
                }
                if (moveLeft.GetState(moveType))
                {
                    cc.SimpleMove((camTr.right * -1) * speed);
                    walk = true;
                }
                if (moveBack.GetState(moveType))
                {
                    cc.SimpleMove((camTr.forward * -1) * speed);
                    walk = true;
                }
            }
            else if (noClip)
            {
                if (moveAtion.GetState(moveType))
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
            walk = false;
            yield return new WaitForSecondsRealtime(0.016f);
        } while (true);
    }
}