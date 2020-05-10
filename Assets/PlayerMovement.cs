using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator anim;

    public float runSpeed = 35f;

    float horizontalMove = 0f;
    bool jump = false;
    public bool able = true;
    
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            if(able == true)
            {
                jump = true;
                anim.SetBool("IsJumping", true);
            }
            
        }
    }

    public void OnLanding()
    {
        anim.SetBool("IsJumping", false);
    }
    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
