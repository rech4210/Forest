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

    public GameObject obj2;

    public GameObject TreePicture;
    private string TreeTag; //나무 들어갈 이름

    [Range(0.01f, 0.1f)] public float textDelay;//텍스트 표기 속도

    public Text[] treeText;//표시할 텍스트


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
        //TreePicture = GameObject.Find("TreePicture");
        Checking_On_off = false;
        obj.gameObject.SetActive(false);
        obj2.gameObject.SetActive(false);
        TreeTag = null;
        StartCoroutine(ViewInfo());

    }

    // Update is called once per frame
    void Update()
    {
 
    }
    public void LoadText_toggle()
    {
        Checking_On_off = !Checking_On_off;

            //TreeName = hit.transform.name;
        if (Checking_On_off == true && TL.RaycastCheck ==true)
        {
            InfoUI_toggle();
            TreeTag = TryingGetTagAtParent(TL.hit.transform);

            //GameObject.Find("Canvas").transform.Find("TreePicture").gameObject.SetActive(true);


            try
            {
                TreePicture.GetComponent<Image>().sprite = GameObject.Find(TreeTag+ "_Image").GetComponent<Image>().sprite;
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("이미지 오류 : " + GameObject.Find(TreeTag).name);
            }

            string txtData_2 = null;
            string[] txtData = null;
            string path = $"{Application.dataPath}\\Resources\\TreeInfo\\{TreeTag}.txt";
            try
            {
            txtData = File.ReadAllLines(path);
            }
            catch (FileNotFoundException)
            {
            txtData = new string[1];
            txtData[0] = path;
            }
            /*for (int i = 0; i < txtData.Length; i++)
            {
            txtData_2 = txtData_2 + txtData[i] + System.Environment.NewLine;
            }*/
            //txtData_2 = txtData[i];
            runningCoroutine = StartCoroutine(InputText(txtData));
            
            //runningCoroutine = StartCoroutine(InputText(txtData_2));
        }
        else
        {
            if(TreeTag != null)
            {
                InfoUI_toggle();
                //GameObject.Find("Canvas").transform.Find("TreePicture").gameObject.SetActive(false);
                StopCoroutine(runningCoroutine);
                TreeTag = null;
                TreePicture.GetComponent<Image>().sprite = null;
                for(int i=0;i<treeText.Length;i++)
                {
                    treeText[i].text = null;
                }
            }
            else if(TL.RaycastCheck==false)
            {
                Checking_On_off = !Checking_On_off;
            }
               
        }
        
    }

    private string TryingGetTagAtParent(Transform transform)
    {   // 물체의 트랜스폼을 기준으로 태그를 찾고 없다면 부모의 태그를 얻는 함수
        do
        {
            string result = transform.tag.ToString();
            if (result == "Untagged")
            {   // 태그가 없을 경우
                transform = transform.parent;
            }
            else
            {
                return result;
            }
        } while (true);
    }

    public IEnumerator InputText(string[] txtData)
    {
        Debug.Log(txtData.Length);
        for(int j=0;j< txtData.Length;j++)
        {
            for (int i = 0; i < txtData[j].Length; i++)
            {
                treeText[j].text = txtData[j].Substring(0, i);
                yield return new WaitForSecondsRealtime(textDelay);

            }
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
        if (obj2.gameObject.activeSelf == true)
        {
            obj2.gameObject.SetActive(false);
        }
        else
        {
            obj2.gameObject.SetActive(true);
        }
    }
}
