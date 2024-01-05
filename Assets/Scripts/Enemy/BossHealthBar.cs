using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public HealthBar healthbar;
    public TornatoEnemy te;
    // Start is called before the first frame update
    void Start()
    {
        healthbar.SetMaxHealth(5000);
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.SetHealth(te.HP);
    }
}
