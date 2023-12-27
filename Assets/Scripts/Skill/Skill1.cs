using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    public Collider myCollider;
    GameObject myObject;

    private void Start()
    {
        myObject = GameObject.Find("ForceField");
        myCollider = myObject.GetComponent<CapsuleCollider>();
    }
    SkillCD1 skill1;
    private void Update()
    {
        myCollider.enabled = false;
        skill1 = FindObjectOfType<SkillCD1>();
        if (skill1.isUsingSkill)
        {
            ToggleCollider();
        }
    }

    private void ToggleCollider()
    {
        myCollider.enabled = true;

        if (myCollider.enabled)
        {
            Debug.Log("Collider is enabled.");
        }
        else
        {
            Debug.Log("Collider is disabled.");
        }
    }
}
