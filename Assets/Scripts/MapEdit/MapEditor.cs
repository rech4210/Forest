using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    public GameObject map;
    public bool randomRotation;
    public bool randomWidth;
    public bool randomHeight;
    public float randomMinValue = 0.8f, randomMaxValue = 1.15f;
    [Space(10)] public GameObject[] trees;

    private int selectingIndex;
    private List<GameObject> history;
    private Transform createTransform;
    private string treeListString;

    private void Start()
    {
        if(map == null)
        {
            map = new GameObject("Map");
        }
        
        history = new List<GameObject>();
        createTransform = gameObject.transform.Find("Create Position").transform;

        for(int i = 0; i < trees.Length; i++)
        {
            if (trees[i] == null)
            {
                continue;
            }

            if(i <= 11)
            {
                treeListString += $"F{(i + 1)}";
            }
            else
            {
                treeListString += $"Shift + F{i - 11}";
            }

            treeListString += $": {trees[i].name}\n[{trees[i].tag}]\n\n";
        }
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
        for (int i = (int)KeyCode.F1; i <= (int)KeyCode.F12; i++)
        {
            KeyCode keyCode = (KeyCode)i;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(keyCode))
            {
                selectingIndex = i - (int)KeyCode.F1 + 12;
            }
            else if (Input.GetKeyDown(keyCode))
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
            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height - 60 , 300, 55),
                $"생성 위치 : {createTransform.position}\n{trees[selectingIndex].name}\n[{trees[selectingIndex].tag}]");

            GUI.Box(new Rect(Screen.width - 275, 5, 270, Screen.height - 10), treeListString);
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.Log("트리 인덱스 초과");
        }
    }

    public void Save()
    {
        string PrefabFolder = null;
        //PrefabFolder = UnityEditor.EditorUtility.SaveFilePanel("Save map prefab", PrefabFolder, "New map", "prefab");

        if (PrefabFolder.Length > 0)
        {
            //UnityEditor.PrefabUtility.SaveAsPrefabAsset(map, "Assets" + PrefabFolder.Replace(Application.dataPath, null));
        }
    }

    private void Create(Vector3 position)
    {
        GameObject treeObject = Instantiate(trees[selectingIndex]);
        treeObject.transform.position = position;
        history.Add(treeObject);
        treeObject.transform.parent = map.transform;

        if(randomRotation)
        {
            Vector3 rotation = treeObject.transform.rotation.eulerAngles;
            rotation.y = Random.Range(0, 360f);
            treeObject.transform.rotation = Quaternion.Euler(rotation);
        }

        Vector3 size = treeObject.transform.localScale;
        if (randomWidth)
        {
            float randomValue = Random.Range(randomMinValue, randomMaxValue);
            size.x *= randomValue;
            size.z *= randomValue;
        }
        if (randomHeight)
        {
            float randomValue = Random.Range(randomMinValue, randomMaxValue);
            size.y *= randomValue;
        }
        treeObject.transform.localScale = size;
    }
    
    private void Create()
    {
        Vector3 position = createTransform.position;
        position.y -= createTransform.localScale.y / 2;
        Create(position);
    }

    private void Ctrl_Z()
    {
        try
        {

            Destroy(history[history.Count - 1]);
            history.RemoveAt(history.Count - 1);
        }
        catch (System.ArgumentOutOfRangeException)
        {
            Debug.Log("되돌리기 인덱스 초과");
        }
    }
}
