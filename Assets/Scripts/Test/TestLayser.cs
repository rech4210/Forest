using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Valve.VR;

public class TestLayser : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;

    public GameObject layserPrefab;
    private GameObject layser;
    private Transform layserTransform;
    private Vector3 hitPoint;

    private void Start()
    {
        //레이져 프리팹 생성
        layser = Instantiate(layserPrefab);
        layserTransform = layser.transform;
    }

    private void Update()
    {
        if (teleportAction.GetState(handType))
        {
            RaycastHit hit;
            
            if (Physics.Raycast(controllerPose.transform.position, transform.forward,
                out hit, 100))
            {
                hitPoint = hit.point;
            }

            ShowLayser(hit);
            
        }
        else
        {
            layser.SetActive(false);
        }
    }
    //레이져 보여주기
    private void ShowLayser(RaycastHit hit)
    {
        layser.SetActive(true);
        layser.transform.position = Vector3.Lerp(controllerPose.transform.position,
            hitPoint, 0.5f);
        layserTransform.LookAt(hitPoint);
        layserTransform.localScale = new Vector3(layserTransform.localScale.x,
            layserTransform.localScale.y, hit.distance);
    }
}
