using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public enum WAND_SIDE
{
    LEFT = 0,
    RIGHT,
    MAX
}
public class TestMove : MonoBehaviour
{
    SteamVR_Con
    public SteamVR_TrackedObject mTrackedObj;
    private SteamVR_Controller.Device mWand { get { return SteamVR_Controller.Input((int)mTrackedObj.index); } }
    public WAND_SIDE mSide;

    private void Update()
    {
        Debug.Log(string.Format("{0} {1}", mSide, mWand, GetAxis()));
    }
}
