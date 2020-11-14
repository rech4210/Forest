using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public enum MoveType
    {
        WAY_POINT,
        LOOK_AT,
        DAYDREAM
    }

    public MoveType moveType = MoveType.WAY_POINT; // 이동 방식
    public float speed = 1.0f;                     // 이동 속도
    public float damping = 3.0f;                   // 회전 속도 조절 계수

    private Transform tr;
    private Transform camTr;
    private CharacterController cc;
    private Transform[] points;                    // 웨이포인트 저장 배열
    private int nextIdx = 1;                       // 다음 이동 위치 변수

    void Start()
    {
        tr = GetComponent<Transform>();
        camTr = Camera.main.GetComponent<Transform>();
        cc = GetComponent<CharacterController>();
        points = GameObject.Find("WayPointGroup").GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        switch(moveType)
        {
            case MoveType.WAY_POINT:
                MoveWayPoint();
                break;

            case MoveType.LOOK_AT:
                MoveLookAt();
                break;

            case MoveType.DAYDREAM:
                break;
        }
    }

    void MoveWayPoint()
    {
        Vector3 direction = points[nextIdx].position - tr.position;
        Quaternion rot = Quaternion.LookRotation(direction);
        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
        tr.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void MoveLookAt()
    {
        Vector3 dir = camTr.TransformDirection(Vector3.forward);
        cc.SimpleMove(dir * speed);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("WAY_POINT"))
        {
            nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
        }
    }
}