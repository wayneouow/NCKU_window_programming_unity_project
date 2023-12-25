using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscontrol : MonoBehaviour
{
    //∞—º∆
    public Vector3 MovingDirection;
    public float JumpingForce;
    Rigidbody rb;
    [SerializeField] float movingSpeed = 10f;
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
        //∏ı≈D
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(JumpingForce * Vector3.up);
        }
    }
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * movingSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);
    }
}
