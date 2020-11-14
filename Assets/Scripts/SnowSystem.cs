using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSystem : MonoBehaviour
{
    public GameObject snowPrefab;
    public GameObject player;

    [Space(10)]
    public float maxPoint;
    public float y;

    [Space(10)]
    public float dropInterval;
    public float snowSpeed;
    public float minSizeValue, maxSizeValue;
    public float coroutineCount;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Editor");
        }

        for(int i = 0; i < coroutineCount; i++)
        {
            StartCoroutine(SnowProcess());
        }
    }

    IEnumerator SnowProcess()
    {
        do
        {
            CreateSnow();
            yield return new WaitForSecondsRealtime(dropInterval);
        } while (true);
    }

    private void CreateSnow()
    {
        GameObject snow = Instantiate(snowPrefab);
        float xValue = Random.Range(-maxPoint, maxPoint);
        float zValue = Random.Range(-maxPoint, maxPoint);
        snow.transform.parent = transform;
        snow.transform.position = new Vector3(xValue, y, zValue) + player.transform.position;
        snow.transform.localScale *= Random.Range(minSizeValue, maxSizeValue);
        snow.GetComponent<Rigidbody>().velocity = new Vector3(0, -snowSpeed, 0);
    }
}