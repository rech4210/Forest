using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    public GameObject[] trees;

    private int selectingIndex;
    private GameObject map;
    public List<GameObject> history;
    private void Start()
    {
        map = new GameObject("Map");
        history = new List<GameObject>();
    }

    void Update()
    {
        for(int i = (int)KeyCode.F1; i <= (int)KeyCode.F10; i++)
        {
            KeyCode keyCode = (KeyCode)i;
            if (Input.GetKeyDown(keyCode))
            {
                selectingIndex = i - (int)KeyCode.F1;
            }
        }

        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(1))
        {
            Create(transform.position);
        }
        else if(Input.GetKeyDown(KeyCode.Z))
        {
            Ctrl_Z();
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 90), selectingIndex + "\n" + trees[selectingIndex].name);
    }

    private void Create(Vector3 position)
    {
        GameObject treeObject = Instantiate(trees[selectingIndex]);
        treeObject.transform.position = transform.position;
        history.Add(treeObject);
        treeObject.transform.parent = map.transform;
    }

    private void Ctrl_Z()
    {
        Destroy(history[history.Count - 1]);
        history.RemoveAt(history.Count - 1);
    }
}
