using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Valve.VR;

public class TestLayser : MonoBehaviour
{
    private LoadText LT;
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;

    public GameObject layserPrefab;
    private GameObject layser;
    private Transform layserTransform;
    private Vector3 hitPoint;

    private int layerMask;//Raycast가 식별할 레이어

    public bool RaycastCheck;//Raycast가 물체를 충돌했는가 체크

    public RaycastHit hit;

    private void Start()
    {
        //레이져 프리팹 생성
        layser = Instantiate(layserPrefab);
        layserTransform = layser.transform;
        LT = GetComponent<LoadText>();
        layerMask = 1 << 8;
    }

    private void Update()
    {
        if (teleportAction.GetState(handType))
        {
            if (Physics.Raycast(controllerPose.transform.position, transform.forward,
                out hit, 100f,layerMask))
            {
                hitPoint = hit.point;
                RaycastCheck = true;
            }
            else
            {
                RaycastCheck = false;
            }
            ShowLayser(hit);
            Debug.Log("On");
            
        }
        else
        {
            RaycastCheck = false;
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
