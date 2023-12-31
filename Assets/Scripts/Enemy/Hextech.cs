using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Hextech : MonoBehaviour//, IPointerClickHandler
{
    public List<string> skillList = new List<string>(); // 技能清單
    public List<string> selectedSkills = new List<string>(); // 選擇的技能清單

    public bool haschoose = false;
    public int choose = 0;
    GameObject select_image;
    public Canvas canvas;

    public bool isShow=false;
    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        
        //this.transform.localScale = canvasScale;
        InitializeSkillList();
        // 生成隨機技能並顯示
        GenerateRandomSkills();
        DisplaySkills();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.enabled == true)
        {
            isShow = true;
        }
        if (isShow == true)
        {
            canvas.enabled = true;
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0f)
            {

                HandleMouseScroll(scroll);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("你成功選擇海克斯");
                string selectedSkill = selectedSkills[choose];
                Debug.Log("玩家選擇了技能：" + selectedSkill);
                canvas.enabled = false;
                isShow = false;
            }

        }
        

    }
    void InitializeSkillList()
    {
        // 在這裡添加初始技能到技能清單
        skillList.Add("skill1");
        skillList.Add("skill2");
        skillList.Add("skill3");
        skillList.Add("skill4");
        skillList.Add("skill5");
        skillList.Add("skill6");
        skillList.Add("skill7");
        // 添加更多技能...

        // 你也可以從其他地方（例如資料庫、檔案等）讀取技能清單
    }
    void GenerateRandomSkills()
    {
        // 清空先前的選擇
        selectedSkills.Clear();
        
        List<int> selectedIndexes = new List<int>();
        selectedIndexes.Clear();

        // 隨機選擇三個技能
        for (int i = 0; i < 3; i++)
        {
            //int randomIndex = Random.Range(0, skillList.Count);
            //selectedSkills.Add(skillList[randomIndex]);
            //skillList.RemoveAt(randomIndex);
            int randomIndex;
            do
            {
                // 產生一個還沒有被選擇過的索引
                randomIndex = UnityEngine.Random.Range(0, skillList.Count);
            } while (selectedIndexes.Contains(randomIndex));
            // 將選擇的索引加入追蹤清單
            selectedIndexes.Add(randomIndex);

            // 將技能加入選擇清單
            selectedSkills.Add(skillList[randomIndex]);
        }

    }
    void DisplaySkills()
    {
        //海克斯1
        GameObject canvasObject = GameObject.Find("hex1");
       
       
        if (canvasObject != null)
        {

            TextMeshProUGUI textComponent = canvasObject.GetComponentInChildren<TextMeshProUGUI>();


            if (textComponent != null)
            {

                textComponent.text += selectedSkills[0];
                Debug.Log("顯示圖片");
            }
            else
            {
                Debug.Log("not found on Canvas");
            }
        }
        GameObject canvasObject2 = GameObject.Find("hex2");

        if (canvasObject2 != null)
        {

            TextMeshProUGUI textComponent2 = canvasObject2.GetComponentInChildren<TextMeshProUGUI>();


            if (textComponent2 != null)
            {

                textComponent2.text += selectedSkills[1];
                Debug.Log("顯示圖片");
            }
            else
            {
                Debug.Log("not found on Canvas");
            }
        }
        GameObject canvasObject3 = GameObject.Find("hex3");
        if (canvasObject3 != null)
        {

            TextMeshProUGUI textComponent3 = canvasObject3.GetComponentInChildren<TextMeshProUGUI>();


            if (canvasObject3 != null)
            {

                textComponent3.text += selectedSkills[2];
                Debug.Log("顯示圖片");
            }
            else
            {
                Debug.Log("not found on Canvas");
            }
        }

    }

    public void SelectSkill(int index)
    {
        // 玩家選擇了其中一個技能
        string selectedSkill = selectedSkills[index];
        Debug.Log("玩家選擇了技能：" + selectedSkill);

        // 在這裡執行你想要實現的技能效果

        // 清空選擇的技能，重新生成新的三個技能
        GenerateRandomSkills();
        DisplaySkills();
    }

    public void OnImageClick(int index)
    {
        // 玩家點擊了圖片，觸發選擇技能的功能
        SelectSkill(index);
        Debug.Log("已經選擇海克斯");
    }
    private void HandleMouseScroll(float scrollValue)
    {
        GameObject select_image = GameObject.Find("select_image");
        GameObject canvasObject = GameObject.Find("hex1");
        GameObject canvasObject2 = GameObject.Find("hex2");
        GameObject canvasObject3 = GameObject.Find("hex3");
        if (scrollValue > 0f)
        {
            Debug.Log("向上滾動");
            if (choose < 2)
            {
                choose += 1;
            }
            // 在這裡執行上滾的功能
            // 例如增加相機的視野、縮小物體等
        }
        else if (scrollValue < 0f)
        {
            Debug.Log("向下滾動");
            if (choose>0)
            {
                choose -= 1;
            }
         
            // 在這裡執行下滾的功能
            // 例如減小相機的視野、放大物體等
        }
        else
        {
            //Debug.Log("滾輪停止滾動");

            // 在這裡執行停止滾動的功能
            // 例如不執行任何特定操作
        }
        Debug.Log(choose);
        if (choose == 0)
        {
            select_image.transform.position=canvasObject.transform.position;
        }
        else if(choose == 1)
        {
            select_image.transform.position = canvasObject2.transform.position;
        }
        else if(choose == 2)
        {
            select_image.transform.position = canvasObject3.transform.position;
        }
    }

}
