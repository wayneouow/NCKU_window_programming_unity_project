using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscontrol : MonoBehaviour
{
    /*
    //
    public Vector3 MovingDirection;
    public float JumpingForce;
    */
    //Rigidbody rb;
    // [SerializeField] float movingSpeed = 10f;
    
    public float health=100f;
    public bool immune = false;
    //heal
    public bool isHealed = false;
    public bool healtimer = false;
    public float healStartTime = 0f;
    private float healDuration = 4f;
    private int healcount = 0;
    //attack
    public float damage = 50f;
    public float armor = 1f;
    public float lucky = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //WASD
        Move();
        //Space
        //
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(JumpingForce * Vector3.up);
        }
        */
        if (isHealed)
        {
            PlayerHealing(10);
        }

    }
    /*
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * movingSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);
    }*/
    public void TakeDagme(float damage)
    {
        damage = damage * armor;
        if(immune==false)
        {
            health -= damage;
        }
        else
        {
            Invoke("ResetImmune",5f);
        }
    }

    public void ResetImmune()
    {
        immune = false;
    }
    void PlayerHealing(float hp)
    {
        healcount++;
       
        isHealed = false;
        if (healtimer)
        {
            healStartTime += Time.deltaTime;
            health += hp;

            if (healStartTime >= healDuration)
            {             
                healtimer = false;
                healStartTime = 0f;
                healDuration = 4f;
                healcount = 0;
            }
        }
    }
}
