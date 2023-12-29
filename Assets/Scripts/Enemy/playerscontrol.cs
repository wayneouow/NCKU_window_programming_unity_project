using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscontrol : MonoBehaviour
{
    //�Ѽ�
    public Vector3 MovingDirection;
    public float JumpingForce;
    Rigidbody rb;
    [SerializeField] float movingSpeed = 10f;
    public float health=100f;

    //heal
    public bool isHealed = false;
    public bool healtimer = false;
    public float healStartTime = 0f;
    private float healDuration = 4f;
    private int healcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //WASD
        Move();
        //Space
        //���D
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(JumpingForce * Vector3.up);
        }

        if (isHealed)
        {
            PlayerHealing(10);
        }

    }
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * movingSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);
    }

    void PlayerHealing(float hp)
    {
        healcount++;
       
        isHealed = false;
        if (healtimer)
        {
            healStartTime += Time.deltaTime;
            // �~��w�t��
            health += hp;
            //Debug.Log("�ĤH����");

            Debug.Log("���a���b�^��A��q=" + health);
            if (healStartTime >= healDuration)
            {
                // �ĪG����
               
                healtimer = false;
                healStartTime = 0f;
                healDuration = 4f;
                healcount = 0;
            }
        }
    }
}
