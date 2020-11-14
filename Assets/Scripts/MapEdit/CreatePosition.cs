using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePosition : MonoBehaviour
{
    public float x = 0, z = 7;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Editor");
    }

    private void Update()
    {
        ResetPosition();
        Vector3 startPosition = transform.position;
        startPosition.y = player.transform.position.y;

        int layerMask = 1 << LayerMask.NameToLayer("Terrain");
        bool isHit = Physics.Raycast(startPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask);
        Debug.DrawRay(startPosition, Vector3.down * 500, Color.red, 0.1f);

        if (isHit && hit.point.y >= 0)
        {
            Vector3 position = transform.position;
            position.y = hit.point.y + transform.localScale.x / 2;
            transform.position = position;
        }
    }

    void ResetPosition()
    {
        ResetPosition(gameObject);
    }

    void ResetPosition(GameObject go)
    {
        go.transform.localPosition = new Vector3(x, transform.lossyScale.x / 2, z);
    }
}
