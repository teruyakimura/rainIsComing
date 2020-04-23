using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D controller;

    public Rigidbody2D rb;
    public Animator anim;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
 
    private Vector3 m_Velocity = Vector3.zero;
    private float m_MovementSmoothing = .05f;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKeyDown("right"))

        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            anim.SetFloat("speed", Mathf.Abs(horizontalMove));
            //horizontalMove * Time.fixedDeltaTim

            Vector3 targetVelocity = new Vector2(horizontalMove * Time.fixedDeltaTime * 10f, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


        }
        // else
        //{
        //  anim.SetFloat("speed", .0f);

        //}
    }
}
