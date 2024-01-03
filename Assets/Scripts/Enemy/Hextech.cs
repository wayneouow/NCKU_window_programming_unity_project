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
    public List<string> skillList = new List<string>(); // �ޯ�M��
    public List<string> selectedSkills = new List<string>(); // ��ܪ��ޯ�M��
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
        // �ͦ��H���ޯ�����
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

            //���UEnter���
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("�A���\��ܮ��J��");
                string selectedSkill = selectedSkills[choose];
                Debug.Log("���a��ܤF�ޯ�G" + selectedSkill);
                canvas.enabled = false;
                isShow = false;
                Debug.Log("�o�O��"+ selectedIndexes[choose] + "+1�ۧޯ�");
                SelectSkill(selectedIndexes[choose]);
            }

        }
        

    }
    void InitializeSkillList()
    {
        // �b�o�̲K�[��l�ޯ��ޯ�M��
        skillList.Add("skill1:player full hp");
        skillList.Add("skill2:speed up");
        skillList.Add("skill3:Invincible for 5 seconds");
        skillList.Add("skill4:destroy all little enemies");
        skillList.Add("skill5:jump higher for 5 seconds");
        skillList.Add("skill6:+25% attack");
        skillList.Add("skill7:+25% armor");
        skillList.Add("skill8:+25% lucky");
        // �K�[��h�ޯ�...

        // �A�]�i�H�q��L�a��]�Ҧp��Ʈw�B�ɮ׵��^Ū���ޯ�M��
    }
    void GenerateRandomSkills()
    {
        // �M�ť��e�����
        selectedSkills.Clear();
        
        //List<int> selectedIndexes = new List<int>();
        selectedIndexes.Clear();

        // �H����ܤT�ӧޯ�
        for (int i = 0; i < 3; i++)
        {
            //int randomIndex = Random.Range(0, skillList.Count);
            //selectedSkills.Add(skillList[randomIndex]);
            //skillList.RemoveAt(randomIndex);
            int randomIndex;
            do
            {
                // ���ͤ@���٨S���Q��ܹL������
                randomIndex = UnityEngine.Random.Range(0, skillList.Count);
            } while (selectedIndexes.Contains(randomIndex));
            // �N��ܪ����ޥ[�J�l�ܲM��
            selectedIndexes.Add(randomIndex);

            // �N�ޯ�[�J��ܲM��
            selectedSkills.Add(skillList[randomIndex]);
        }

    }
    void DisplaySkills()
    {
        //���J��1
        GameObject canvasObject = GameObject.FindWithTag("hex1");
       
       
        if (canvasObject != null)
        {

            TextMeshProUGUI textComponent = canvasObject.GetComponentInChildren<TextMeshProUGUI>();


            if (textComponent != null)
            {
                textComponent.text = "";
                textComponent.fontSize = 12;
                textComponent.text += selectedSkills[0];
                Debug.Log("��ܹϤ�");
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
                Debug.Log("��ܹϤ�");
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
                Debug.Log("��ܹϤ�");
            }
            else
            {
                Debug.Log("not found on Canvas");
            }
        }

    }

    public void SelectSkill(int index)
    {
        // ���a��ܤF�䤤�@�ӧޯ�
        //string selectedSkill = selectedSkills[index];
        Debug.Log("���a��ܤF�ޯ�G" + skillList[index]);

        // �b�o�̰���A�Q�n��{���ޯ�ĪG
        if (index == 0)
        {
            GameObject player = GameObject.Find("Player");
            player.GetComponent<playerscontrol>().health = 100;
            Debug.Log("���\�ϥβ�" + skillList[index] + "�ۧޯ�");

        }
        else if(index == 1)
        {

            GameObject player = GameObject.Find("Player");
            originwalkSpeed = player.GetComponent<PlayerMovementAdvanced>().walkSpeed;
            player.GetComponent<PlayerMovementAdvanced>().walkSpeed = 20;
            Invoke("ResetWalkSpeed", 5f);
            Debug.Log("���\�ϥβ�" + skillList[index] + "�ۧޯ�");
        }
        else if(index == 2)
        {
            GameObject player = GameObject.Find("Player");
            player.GetComponent<playerscontrol>().immune = true;
            Debug.Log("���\�ϥβ�" + skillList[index] + "�ۧޯ�");
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
            Debug.Log("���\�ϥβ�" + skillList[index] + "�ۧޯ�");
        }
        else if (index == 4)
        {
            GameObject player = GameObject.Find("Player");
            originjumpForce = player.GetComponent<PlayerMovementAdvanced>().jumpForce;
            player.GetComponent<PlayerMovementAdvanced>().jumpForce = 50;
            Invoke("ResetJumpForce", 5f);
            Debug.Log("���\�ϥβ�" + skillList[index] + "�ۧޯ�");
        }
        else if (index == 5)
        {
            GameObject player = GameObject.Find("Player");
            float damage = player.GetComponent<playerscontrol>().damage;
            player.GetComponent<playerscontrol>().damage = damage * 1.25f;
            Debug.Log("���\�ϥβ�" + skillList[index] + "�ۧޯ�");
        }
        else if (index == 6)
        {
            GameObject player = GameObject.Find("Player");
            float armor = player.GetComponent<playerscontrol>().armor;
            player.GetComponent<playerscontrol>().armor = armor * 0.75f;
            Debug.Log("���\�ϥβ�" + skillList[index] + "�ۧޯ�");
        }
        else if (index == 7)
        {
            float rate = 0.75f;
            GameObject player = GameObject.Find("Player");
            float lucky = player.GetComponent<playerscontrol>().lucky;
            player.GetComponent<playerscontrol>().lucky = lucky * rate;

            Debug.Log("���\�ϥβ�" + skillList[index] + "�ۧޯ�");
        }

        // �M�ſ�ܪ��ޯ�A���s�ͦ��s���T�ӧޯ�
        choose = 0;
        GenerateRandomSkills();
        DisplaySkills();
    }

    public void OnImageClick(int index)
    {
        // ���a�I���F�Ϥ��AĲ�o��ܧޯ઺�\��
        SelectSkill(index);
        Debug.Log("�w�g��ܮ��J��");
    }
    private void HandleMouseScroll(float scrollValue)
    {
        GameObject select_image = GameObject.FindWithTag("select_image");
        GameObject canvasObject = GameObject.FindWithTag("hex1");
        GameObject canvasObject2 = GameObject.FindWithTag("hex2");
        GameObject canvasObject3 = GameObject.FindWithTag("hex3");
        if (scrollValue > 0f)
        {
            Debug.Log("�V�W�u��");
            if (choose < 2)
            {
                choose += 1;
            }
            // �b�o�̰���W�u���\��
            // �Ҧp�W�[�۾��������B�Y�p���鵥
        }
        else if (scrollValue < 0f)
        {
            Debug.Log("�V�U�u��");
            if (choose>0)
            {
                choose -= 1;
            }
         
            // �b�o�̰���U�u���\��
            // �Ҧp��p�۾��������B��j���鵥
        }
        else
        {
            //Debug.Log("�u������u��");

            // �b�o�̰��氱��u�ʪ��\��
            // �Ҧp���������S�w�ާ@
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
    //���s�ޯ઺�N�o
    public void ResetWalkSpeed()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovementAdvanced>().walkSpeed = originwalkSpeed;
        Debug.Log("��_�����t��");
    }

    public void ResetJumpForce()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovementAdvanced>().jumpForce = originjumpForce;
        Debug.Log("��_���D�O");
    }
}
