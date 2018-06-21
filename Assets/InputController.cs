using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    #region variables
    float inputH;
    float inputV;

    public float gravityMultiplier = 2f;
    public float jumpPower = 6;

    public bool isCrouching = false;
    public bool isGrounded = true;
    public bool hasJumped = false;

    float groundDistance = 0.2f;
    float originalGroundDistance;
    float forwardAmount;
    Vector3 moveVector;

    Animator anim;
    Rigidbody rb;
    #endregion


    // Use this for initialization
    void Start()
    {
        // get component from Animator
        anim = GetComponent<Animator>();
        // get component from rigidbody
        rb = GetComponent<Rigidbody>();

        // set originalGroundDistance = groundDistance
        originalGroundDistance = groundDistance;
    }

    // Update is called once per frame
    void Update()
    {
        // call CheckGroundStatus fucntion
        CheckGroundStatus();

        // get Vertical axis and set it into inputV
        inputV = Input.GetAxis("Vertical");
        // get Horizontal axis and set it into inputH
        inputH = Input.GetAxis("Horizontal");

        // if shift key is pressed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // set anim bool Running to true
            anim.SetBool("Running", true);
            // set anim float Vertical to inputV
            anim.SetFloat("Vertical", inputV);
            // set anim float Horizontal to inputH
            anim.SetFloat("Horizontal", inputH);
            // set moveVector to inputV * Vector3 forward plus inputH * Vector3 right
            moveVector = inputV * Vector3.forward + inputH * Vector3.right;
            // set forwardAmount to moveVector.z
            forwardAmount = moveVector.z;
        }
        // if shift key is NOT pressed
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            // set anim bool Running to false
            anim.SetBool("Running", false);
            // set anim float Vertical to inputV times half
            anim.SetFloat("Vertical", inputV * 0.5f);
            // set anim float Horizontal to inputH times half
            anim.SetFloat("Horizontal", inputH * 0.5f);
            // set moveVector to inputV * Vector3 forward plus inputH * Vector3 right
            moveVector = inputV * Vector3.forward + inputH * Vector3.right;
            // set forwardAmount to moveVector.z
            forwardAmount = moveVector.z;
        }
        // if left control key is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // if is not crouching
            if (!isCrouching)
            {
                // set anim bool Crouch to true
                anim.SetBool("Crouch", true);
                // flip isCrouching bool
                isCrouching = !isCrouching;
                // if shift key is pressed
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    // set anim bool Running to true
                    anim.SetBool("Running", true);
                    // set anim bool Crouch to true
                    anim.SetBool("Crouch", true);
                }
                // if shift key is NOT pressed
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    // set anim bool Running to false
                    anim.SetBool("Running", false);
                    // set anim bool Crouch to false
                    anim.SetBool("Crouch", false);
                }
            }
            else
            {
                // set anim bool Crouch to false
                anim.SetBool("Crouch", false);
                // flip isCrouching bool
                isCrouching = !isCrouching;
            }
        }
        // if space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isCrouching)
        {
            anim.SetBool("Crouch", false);
            // flip isCrouching bool
            isCrouching = !isCrouching;
            // set isGrounded to false
            isGrounded = false;
            // set rigidbody velocity to new Velocity, change the rb.y to jumpPower
            rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
            // set anim.applyRootMotion to false
            anim.applyRootMotion = false;
            // flip hasJumped bool
            hasJumped = !hasJumped;

        }
        // if hasJumped is false
        if (!hasJumped)
        {
            // if space bar is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // set isGrounded to false
                isGrounded = false;
                // set rigidbody velocity to new Velocity, change the rb.y to jumpPower
                rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
                // set anim.applyRootMotion to false
                anim.applyRootMotion = false;
                // flip hasJumped bool
                hasJumped = !hasJumped;
            }
        }
        // isGrounded is fals
        if (!isGrounded)
        {
            // set anim bool isGrounded to false
            anim.SetBool("IsGrounded", false);
            // set anim float Jump to rb.velocity.y
            anim.SetFloat("Jump", rb.velocity.y);
        }
        // hasJumped is true
        if (hasJumped)
        {
            // call OnJumped function
            OnJumped();
        }
        // set float runCycle = Mathf.Repeat(anim.GetCurrentAnimatorStateInfo(0).normalizedTime + groundDistance, 1);
        float runCycle = Mathf.Repeat(anim.GetCurrentAnimatorStateInfo(0).normalizedTime + groundDistance, 1);
        // set float jumpLeg to (runCycle < 0.5f ? 1 : -1) * forwardAmount
        float jumpLeg = (runCycle < 0.5f ? 1 : -1) * forwardAmount;
        // if isGrounded true
        if (isGrounded)
        {
            // set anim float JumpLeg to jumpLeg
            anim.SetFloat("JumpLeg", jumpLeg);
        }

    }
    void CheckGroundStatus()
    {
        // new RaycastHit hitInfo
        RaycastHit hitInfo;

        // if raycast from transform position to down Vector3 out hitInfo, range is groundDistance
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundDistance))
        {
            // set isGrounded to true
            isGrounded = true;
            // set anim applyRootMotion to true
            anim.applyRootMotion = true;
            // set hasJumped to false
            hasJumped = false;
            // set anim bool IsGrounded to true
            anim.SetBool("IsGrounded", true);
        }
        else
        {
            // set hasJumped to true
            hasJumped = true;
            // set isGrounded to false
            isGrounded = false;
            // set anim applyRootMotion to false
            anim.applyRootMotion = false;
        }
    }
    void OnJumped()
    {
        // apply extra gravity from multiplier:
        Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
        // AddForce to rb taking passing extraGravityForce
        rb.AddForce(extraGravityForce);
        //set groundDistance = rb.velocity.y < 0 ? originalGroundDistance : 0.02f
        groundDistance = rb.velocity.y < 0 ? originalGroundDistance : 0.02f;
    }
}
