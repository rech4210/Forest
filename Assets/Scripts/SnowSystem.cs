using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSystem : MonoBehaviour
{
    public GameObject snowPrefab;
    public GameObject player;
    public float randomMinX, randomMaxX;
    public float randomMinZ, randomMaxZ;
    public float y;
    public float dropInterval;
    public float snowSpeed;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Editor");
        }

        StartCoroutine(SnowProcess());
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
        float x = Random.Range(randomMinX, randomMaxX);
        float z = Random.Range(randomMinZ, randomMaxZ);
        snow.transform.parent = transform;
        snow.transform.position = new Vector3(x, y, z) + player.transform.position;
        snow.GetComponent<Rigidbody>().velocity = new Vector3(0, -snowSpeed, 0);
    }
}