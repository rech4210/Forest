using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class LoadText : MonoBehaviour
{
    private TestLayser TL;

    public GameObject obj;
    private string TreeName; //나무 들어갈 이름

    [Range(0.01f, 0.1f)] public float textDelay;//텍스트 표기 속도

    public Text treeText;//표시할 텍스트

    private bool Checking_On_off; //버튼 온오프

    private Coroutine runningCoroutine;

    public RaycastHit hit;//Raycast로 피격한 오브젝트 정보

    float MaxDistance = 300f;//Raycast 사정거리

    public SteamVR_Action_Boolean teleportAction;

    public SteamVR_Input_Sources handType;

    //private int layerMask;//Raycast가 식별할 레이어

    //private bool RaycastCheck;//Raycast가 물체를 충돌했는가 체크

    // Start is called before the first frame update
    void Start()
    {
        TL = GameObject.Find("Controller (right)").GetComponent<TestLayser>();
        Checking_On_off = false;
        //layerMask = 1 << 8
        obj.gameObject.SetActive(false);
        TreeName = null;
        StartCoroutine(ViewInfo());

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Physics.Raycast(transform.position, transform.forward, out hit, MaxDistance,layerMask))
        {
            Debug.Log(hit.transform.name);
            RaycastCheck = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 1000f, Color.blue);
            RaycastCheck = false;
        }*/
    }
    public void LoadText_toggle()
    {
        Checking_On_off = !Checking_On_off;

            //TreeName = hit.transform.name;
        if (Checking_On_off == true && TL.RaycastCheck ==true)
        {
            InfoUI_toggle();
            TreeName = TL.hit.transform.name;
            GameObject.Find("Canvas").transform.Find(TreeName).gameObject.SetActive(true);
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
            if(TreeName !=null)
            {
                InfoUI_toggle();
                GameObject.Find("Canvas").transform.Find(TreeName).gameObject.SetActive(false);
                StopCoroutine(runningCoroutine);
                TreeName = null;
                treeText.text = null;
            }
            else if(TL.RaycastCheck==false)
            {
                Checking_On_off = !Checking_On_off;
            }
               
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
    public IEnumerator ViewInfo()
    {
        do
        {
            if (teleportAction.GetState(handType))
            {
                Debug.Log("ViewInfo");
                LoadText_toggle();
                yield return new WaitForSecondsRealtime(1f);
            }
            else
            {
                yield return null;
            }
        } while (true);
    }

    public void InfoUI_toggle()
    {
        if (obj.gameObject.activeSelf == true)
        {
            obj.gameObject.SetActive(false);
        }
        else
        {
            obj.gameObject.SetActive(true);
        }
    }
}
