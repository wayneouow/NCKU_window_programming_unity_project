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
    public List<string> skillList = new List<string>(); // �ޯ�M��
    public List<string> selectedSkills = new List<string>(); // ��ܪ��ޯ�M��

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

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("�A���\��ܮ��J��");
                string selectedSkill = selectedSkills[choose];
                Debug.Log("���a��ܤF�ޯ�G" + selectedSkill);
                canvas.enabled = false;
                isShow = false;
            }

        }
        

    }
    void InitializeSkillList()
    {
        // �b�o�̲K�[��l�ޯ��ޯ�M��
        skillList.Add("skill1");
        skillList.Add("skill2");
        skillList.Add("skill3");
        skillList.Add("skill4");
        skillList.Add("skill5");
        skillList.Add("skill6");
        skillList.Add("skill7");
        // �K�[��h�ޯ�...

        // �A�]�i�H�q��L�a��]�Ҧp��Ʈw�B�ɮ׵��^Ū���ޯ�M��
    }
    void GenerateRandomSkills()
    {
        // �M�ť��e�����
        selectedSkills.Clear();
        
        List<int> selectedIndexes = new List<int>();
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
        GameObject canvasObject = GameObject.Find("hex1");
       
       
        if (canvasObject != null)
        {

            TextMeshProUGUI textComponent = canvasObject.GetComponentInChildren<TextMeshProUGUI>();


            if (textComponent != null)
            {

                textComponent.text += selectedSkills[0];
                Debug.Log("��ܹϤ�");
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
                Debug.Log("��ܹϤ�");
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
        string selectedSkill = selectedSkills[index];
        Debug.Log("���a��ܤF�ޯ�G" + selectedSkill);

        // �b�o�̰���A�Q�n��{���ޯ�ĪG

        // �M�ſ�ܪ��ޯ�A���s�ͦ��s���T�ӧޯ�
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
        GameObject select_image = GameObject.Find("select_image");
        GameObject canvasObject = GameObject.Find("hex1");
        GameObject canvasObject2 = GameObject.Find("hex2");
        GameObject canvasObject3 = GameObject.Find("hex3");
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

}
