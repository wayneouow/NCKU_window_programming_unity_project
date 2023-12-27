using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCD1 : MonoBehaviour
{
    public float coolTime = 5.0f;
    private float cooltimer = 0;
    private float usingtimer = 0;
    private Image skillImg;
    public Image using_skillImg;
    public bool isUsingSkill = false;
    private bool isStartCool = false;


    // Start is called before the first frame update
    void Start()
    {
        skillImg = GetComponent<Image>();
        using_skillImg.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            using_skillImg.gameObject.SetActive(true);
            isUsingSkill = true;
        }

        if (isUsingSkill)
        {
            usingtimer += Time.deltaTime;
            if (usingtimer >= 3)
            {
                using_skillImg.gameObject.SetActive(false);
                usingtimer = 0;
                isUsingSkill = false;
                isStartCool = true;
            }
        }

        if (isStartCool)
        {
            cooltimer += Time.deltaTime;
            skillImg.fillAmount = 1 - ((coolTime - cooltimer) / coolTime);
            if (cooltimer >= coolTime)
            {
                skillImg.fillAmount = 1;
                cooltimer = 0;
                isStartCool = false;
            }
        }
    }
}
