using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    public GameObject[] trees;

    private int selectingIndex;
    private GameObject map;
    private List<GameObject> history;
    private Transform createTransform;

    private void Start()
    {
        map = new GameObject("Map");
        history = new List<GameObject>();
        createTransform = gameObject.transform.Find("Create Position").transform;
    }

    void Update()
    {
        try
        {
            UpdateProcess();
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.Log("트리 인덱스 초과");
        }
    }

    private void UpdateProcess()
    {
        for (int i = (int)KeyCode.F1; i <= (int)KeyCode.F10; i++)
        {
            KeyCode keyCode = (KeyCode)i;
            if (Input.GetKeyDown(keyCode))
            {
                selectingIndex = i - (int)KeyCode.F1;
            }
        }

        bool createTrigger = Input.GetMouseButtonDown(0) || Input.GetMouseButton(1);
        bool ctrlz_Trigger = Input.GetKeyDown(KeyCode.Z) || (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Z));
        bool saveTrigger = Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S);

        if (createTrigger)
        {
            Create();
        }
        else if (ctrlz_Trigger)
        {
            Ctrl_Z();
        }
        else if(saveTrigger)
        {
            Save();
        }
    }

    private void OnGUI()
    {
        try
        {
            GUI.Box(new Rect(10, 10, 100, 90), selectingIndex + "\n" + trees[selectingIndex].name);
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.Log("트리 인덱스 초과");
        }
    }

    public void Save()
    {
        string PrefabFolder = null;
        PrefabFolder = UnityEditor.EditorUtility.SaveFilePanel("Save map prefab", PrefabFolder, "New map", "prefab");

        if (PrefabFolder.Length > 0)
        {
            UnityEditor.PrefabUtility.SaveAsPrefabAsset(map, "Assets" + PrefabFolder.Replace(Application.dataPath, null));
        }
    }

    private void Create(Vector3 position)
    {
        GameObject treeObject = Instantiate(trees[selectingIndex]);
        treeObject.transform.position = position;
        history.Add(treeObject);
        treeObject.transform.parent = map.transform;
    }
    
    private void Create()
    {
        Vector3 position = createTransform.position;
        position.y = 0;
        Create(position);
    }

    private void Ctrl_Z()
    {
        Destroy(history[history.Count - 1]);
        history.RemoveAt(history.Count - 1);
    }
}
