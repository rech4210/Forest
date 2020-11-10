using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Valve.VR;

public class Test : MonoBehaviour
{
    public SteamVR_Action_Boolean TriggerClick;
    private SteamVR_Input_Sources inputSource;

    private void Start() { }

    private void OnEnable()
    {
        TriggerClick.AddOnStateDownListener(Press, inputSource);
    }
    private void OnDisable()
    {
        TriggerClick.RemoveOnStateDownListener(Press, inputSource);
    }

    private void Press(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //put your stuff here
        Debug.Log("Success!!");
    }
}
