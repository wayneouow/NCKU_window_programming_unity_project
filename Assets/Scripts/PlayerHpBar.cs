using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpBar : MonoBehaviour
{
    public HealthBar healthbar;
    public playerscontrol pc;
    // Start is called before the first frame update
    void Start()
    {
        healthbar.SetMaxHealth(pc.health);
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.SetHealth(pc.health);
    }
}
