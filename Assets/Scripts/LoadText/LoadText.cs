using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadText : MonoBehaviour
{
    [Range(0.01f, 1f)] public float textDelay;
    public Text treeText;
    private bool Checking_On_off;
    private Coroutine runningCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        Checking_On_off = false;
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    public void LoadText_toggle()
    {
        Checking_On_off = !Checking_On_off;
        if (Checking_On_off == true)
        {
            string TreeName = "Tree";
            string txtData_2 = null;
            string[] txtData = null;
            string path = $"{Application.dataPath}\\Resources\\TreeInfo\\{TreeName}.txt";
            try
            {
                txtData = File.ReadAllLines(path);
            }
            catch (FileNotFoundException)
            {
                txtData = new string[1];
                txtData[0] = path;
            }
            for (int i = 0; i < txtData.Length; i++)
            {
                txtData_2 = txtData_2 + txtData[i] + System.Environment.NewLine;
            }
            runningCoroutine = StartCoroutine(InputText(txtData_2));
        }
        else
        {
            StopCoroutine(runningCoroutine);
            treeText.text = null;
        }
    }
    public IEnumerator InputText(string txtData_2)
    {
        Debug.Log(txtData_2.Length);
        for (int i = 0; i < txtData_2.Length; i++)
        {
            treeText.text = txtData_2.Substring(0, i);
            yield return new WaitForSecondsRealtime(textDelay);

        }
    }
   /* public IEnumerator LoadImage()
    {
        string path = $"{Application.dataPath}\\Resources\\TreePicture\\Tree.jpg";
        //string path = "TreePicture/Tree.jpg";
        GameObject temp = GameObject.Find("RawImage");
        Texture2D texture = null;
        texture = Resources.Load(path, typeof(Texture2D)) as Texture2D;
        Debug.Log(path);

        return null;
    }*/
}
