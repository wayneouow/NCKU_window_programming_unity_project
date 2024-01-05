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
    public List<string> skillList = new List<string>(); 
    public List<string> selectedSkills = new List<string>(); 
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
            GameObject camera = GameObject.Find("ItemHoldingR");
            camera.GetComponent<WeaponSwitching>().enabled = false;
            Time.timeScale = 0f;
            
            canvas.enabled = true;
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0f)
            {

                HandleMouseScroll(scroll);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                camera.GetComponent<WeaponSwitching>().enabled = true;
                Time.timeScale = 1f;
                string selectedSkill = selectedSkills[choose];
                canvas.enabled = false;
                isShow = false;
                SelectSkill(selectedIndexes[choose]);
            }

        }
        

    }
    void InitializeSkillList()
    {
        skillList.Add("player full hp");
        skillList.Add("speed up");
        skillList.Add("Invincible for 5 seconds");
        skillList.Add("destroy all little enemies");
        skillList.Add("jump higher for 5 seconds");
        skillList.Add("+25% attack");
        skillList.Add("+25% armor");
        skillList.Add("+25% lucky");
    }
    void GenerateRandomSkills()
    {
        selectedSkills.Clear();
        
        //List<int> selectedIndexes = new List<int>();
        selectedIndexes.Clear();

        for (int i = 0; i < 3; i++)
        {
            //int randomIndex = Random.Range(0, skillList.Count);
            //selectedSkills.Add(skillList[randomIndex]);
            //skillList.RemoveAt(randomIndex);
            int randomIndex;
            do
            {
                randomIndex = UnityEngine.Random.Range(0, skillList.Count);
            } while (selectedIndexes.Contains(randomIndex));
            selectedIndexes.Add(randomIndex);

            selectedSkills.Add(skillList[randomIndex]);
        }

    }
    void DisplaySkills()
    {
        GameObject canvasObject = GameObject.FindWithTag("hex1");
       
       
        if (canvasObject != null)
        {

            TextMeshProUGUI textComponent = canvasObject.GetComponentInChildren<TextMeshProUGUI>();


            if (textComponent != null)
            {
                textComponent.text = "";
                textComponent.fontSize = 12;
                textComponent.text += selectedSkills[0];
            }
            else
            {
                //Debug.Log("not found on Canvas");
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
            }
            else
            {
               // Debug.Log("not found on Canvas");
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
            }
            else
            {
                //Debug.Log("not found on Canvas");
            }
        }

    }

    public void SelectSkill(int index)
    {
        //string selectedSkill = selectedSkills[index];
        if (index == 0)
        {
            GameObject player = GameObject.Find("Player");
            player.GetComponent<playerscontrol>().health = 100;

        }
        else if(index == 1)
        {

            GameObject player = GameObject.Find("Player");
            originwalkSpeed = player.GetComponent<PlayerMovementAdvanced>().walkSpeed;
            player.GetComponent<PlayerMovementAdvanced>().walkSpeed = 20;
            Invoke("ResetWalkSpeed", 5f);
        }
        else if(index == 2)
        {
            GameObject player = GameObject.Find("Player");
            player.GetComponent<playerscontrol>().immune = true;
        }
        else if (index == 3)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in objectsWithTag)
            {
                EnemyGhost enemyControl = obj.GetComponent<EnemyGhost>();
                if(enemyControl != null)
                {
                    enemyControl.HP = 0;
                }
               
                //Destroy(obj);
            }
        }
        else if (index == 4)
        {
            GameObject player = GameObject.Find("Player");
            originjumpForce = player.GetComponent<PlayerMovementAdvanced>().jumpForce;
            player.GetComponent<PlayerMovementAdvanced>().jumpForce = 30;
            Invoke("ResetJumpForce", 5f);
        }
        else if (index == 5)
        {
            GameObject player = GameObject.Find("Player");
            float rate = player.GetComponent<playerscontrol>().attackrate;
            player.GetComponent<playerscontrol>().attackrate = rate * 1.25f;
        }
        else if (index == 6)
        {
            GameObject player = GameObject.Find("Player");
            float armor = player.GetComponent<playerscontrol>().armor;
            player.GetComponent<playerscontrol>().armor = armor * 0.75f;
        }
        else if (index == 7)
        {
            float rate = 0.75f;
            GameObject player = GameObject.Find("Player");
            float lucky = player.GetComponent<playerscontrol>().lucky;
            player.GetComponent<playerscontrol>().lucky = lucky * rate;

        }

        choose = 0;
        GenerateRandomSkills();
        DisplaySkills();
    }

    public void OnImageClick(int index)
    {
        SelectSkill(index);
    }
    private void HandleMouseScroll(float scrollValue)
    {
        GameObject select_image = GameObject.FindWithTag("select_image");
        GameObject canvasObject = GameObject.FindWithTag("hex1");
        GameObject canvasObject2 = GameObject.FindWithTag("hex2");
        GameObject canvasObject3 = GameObject.FindWithTag("hex3");
        if (scrollValue > 0f)
        {
            if (choose < 2)
            {
                choose += 1;
            }
        }
        else if (scrollValue < 0f)
        {
            if (choose>0)
            {
                choose -= 1;
            }
         
        }
        else
        {

        }
        //Debug.Log(choose);
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
    public void ResetWalkSpeed()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovementAdvanced>().walkSpeed = originwalkSpeed;
    }

    public void ResetJumpForce()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovementAdvanced>().jumpForce = originjumpForce;
    }
}
