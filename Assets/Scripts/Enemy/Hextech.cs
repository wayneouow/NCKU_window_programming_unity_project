using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEditor.PackageManager;

public class Hextech : MonoBehaviour//, IPointerClickHandler
{
    public List<string> skillList = new List<string>(); // 技能清單
    public List<string> selectedSkills = new List<string>(); // 選擇的技能清單
    List<int> selectedIndexes = new List<int>();

    public bool haschoose = false;
    public int choose = 0;
    GameObject select_image;
    public Canvas canvas;

    public bool isShow=false;

    //skill reset
    float originwalkSpeed;
    float originjumpForce;
    // Start is called before the first frame update
    void Start()
    {
        GameObject canvasObject = GameObject.FindWithTag("rewardtag");
        Canvas canvas = canvasObject.GetComponent<Canvas>();
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

            //按下Enter選擇
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("你成功選擇海克斯");
                string selectedSkill = selectedSkills[choose];
                Debug.Log("玩家選擇了技能：" + selectedSkill);
                canvas.enabled = false;
                isShow = false;
                Debug.Log("這是第"+ selectedIndexes[choose] + "+1招技能");
                SelectSkill(selectedIndexes[choose]);
            }

        }
        

    }
    void InitializeSkillList()
    {
        // 在這裡添加初始技能到技能清單
        skillList.Add("skill1:player full hp");
        skillList.Add("skill2:speed up");
        skillList.Add("skill3:Invincible for 5 seconds");
        skillList.Add("skill4:destroy all little enemies");
        skillList.Add("skill5:jump higher for 5 seconds");
        skillList.Add("skill6:+25% attack");
        skillList.Add("skill7:+25% armor");
        skillList.Add("skill8:+25% lucky");
        // 添加更多技能...

        // 你也可以從其他地方（例如資料庫、檔案等）讀取技能清單
    }
    void GenerateRandomSkills()
    {
        // 清空先前的選擇
        selectedSkills.Clear();
        
        //List<int> selectedIndexes = new List<int>();
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
        GameObject canvasObject = GameObject.FindWithTag("hex1");
       
       
        if (canvasObject != null)
        {

            TextMeshProUGUI textComponent = canvasObject.GetComponentInChildren<TextMeshProUGUI>();


            if (textComponent != null)
            {
                textComponent.text = "";
                textComponent.fontSize = 12;
                textComponent.text += selectedSkills[0];
                Debug.Log("顯示圖片");
            }
            else
            {
                Debug.Log("not found on Canvas");
            }
        }
        GameObject canvasObject2 = GameObject.FindWithTag("hex2");

        if (canvasObject2 != null)
        {

            TextMeshProUGUI textComponent2 = canvasObject2.GetComponentInChildren<TextMeshProUGUI>();


            if (textComponent2 != null)
            {
                textComponent2.text = "";
                textComponent2.fontSize = 12;
                textComponent2.text += selectedSkills[1];
                Debug.Log("顯示圖片");
            }
            else
            {
                Debug.Log("not found on Canvas");
            }
        }
        GameObject canvasObject3 = GameObject.FindWithTag("hex3");
        if (canvasObject3 != null)
        {

            TextMeshProUGUI textComponent3 = canvasObject3.GetComponentInChildren<TextMeshProUGUI>();


            if (canvasObject3 != null)
            {
                textComponent3.text = "";
                textComponent3.fontSize = 12;
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
        //string selectedSkill = selectedSkills[index];
        Debug.Log("玩家選擇了技能：" + skillList[index]);

        // 在這裡執行你想要實現的技能效果
        if (index == 0)
        {
            GameObject player = GameObject.Find("Player");
            player.GetComponent<playerscontrol>().health = 100;
            Debug.Log("成功使用第" + skillList[index] + "招技能");

        }
        else if(index == 1)
        {

            GameObject player = GameObject.Find("Player");
            originwalkSpeed = player.GetComponent<PlayerMovementAdvanced>().walkSpeed;
            player.GetComponent<PlayerMovementAdvanced>().walkSpeed = 20;
            Invoke("ResetWalkSpeed", 5f);
            Debug.Log("成功使用第" + skillList[index] + "招技能");
        }
        else if(index == 2)
        {
            GameObject player = GameObject.Find("Player");
            player.GetComponent<playerscontrol>().immune = true;
            Debug.Log("成功使用第" + skillList[index] + "招技能");
        }
        else if (index == 3)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in objectsWithTag)
            {
                EnemyControl enemyControl = obj.GetComponent<EnemyControl>();
                if(enemyControl != null)
                {
                    enemyControl.Die();
                }
               
                //Destroy(obj);
            }
            Debug.Log("成功使用第" + skillList[index] + "招技能");
        }
        else if (index == 4)
        {
            GameObject player = GameObject.Find("Player");
            originjumpForce = player.GetComponent<PlayerMovementAdvanced>().jumpForce;
            player.GetComponent<PlayerMovementAdvanced>().jumpForce = 50;
            Invoke("ResetJumpForce", 5f);
            Debug.Log("成功使用第" + skillList[index] + "招技能");
        }
        else if (index == 5)
        {
            GameObject player = GameObject.Find("Player");
            float damage = player.GetComponent<playerscontrol>().damage;
            player.GetComponent<playerscontrol>().damage = damage * 1.25f;
            Debug.Log("成功使用第" + skillList[index] + "招技能");
        }
        else if (index == 6)
        {
            GameObject player = GameObject.Find("Player");
            float armor = player.GetComponent<playerscontrol>().armor;
            player.GetComponent<playerscontrol>().armor = armor * 0.75f;
            Debug.Log("成功使用第" + skillList[index] + "招技能");
        }
        else if (index == 7)
        {
            float rate = 0.75f;
            GameObject player = GameObject.Find("Player");
            float lucky = player.GetComponent<playerscontrol>().lucky;
            player.GetComponent<playerscontrol>().lucky = lucky * rate;

            Debug.Log("成功使用第" + skillList[index] + "招技能");
        }

        // 清空選擇的技能，重新生成新的三個技能
        choose = 0;
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
        GameObject select_image = GameObject.FindWithTag("select_image");
        GameObject canvasObject = GameObject.FindWithTag("hex1");
        GameObject canvasObject2 = GameObject.FindWithTag("hex2");
        GameObject canvasObject3 = GameObject.FindWithTag("hex3");
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
    //重製技能的冷卻
    public void ResetWalkSpeed()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovementAdvanced>().walkSpeed = originwalkSpeed;
        Debug.Log("恢復走路速度");
    }

    public void ResetJumpForce()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovementAdvanced>().jumpForce = originjumpForce;
        Debug.Log("恢復跳躍力");
    }
}
