using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicutreTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string PATH = "TreePIcture/Tree.jpg";    //이미지 위치를 저장하는 변수
        GameObject obj= GameObject.Find("Copy");
        gameObject.GetComponent<Image>().sprite = obj.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
