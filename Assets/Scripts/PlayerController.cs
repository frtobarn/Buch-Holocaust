using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MaxSpeed;
    private Rigidbody rb;
    public float Playervelocity;
    public bool gravity ;
    [SerializeField] float Gvalue;
    public float JumpforceUp;
    public float JumpforceDown;
    public bool IsGrounded;
    public float jumpvelocity = 5f;

    public int jumCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Gravity
        if (gravity == true)
        {
             Gvalue = -7;
        }
        if (gravity == false)
        {
            Gvalue = 7;
        }
        float horizontalInput = Input.GetAxis("Horizontal") * Playervelocity;
        // Movement
        if (horizontalInput != 0 && IsGrounded == true)
        {
            Move();

        }
        else if (IsGrounded == true)
        {
            rb.velocity = Vector3.zero;
        }
        else if (IsGrounded == false)
        {
            //Movimineto en el aire
            rb.AddForce(new Vector3(0, Gvalue, 0), ForceMode.Acceleration);
            float horizontalInputJump = Input.GetAxis("Horizontal") * jumpvelocity;
            rb.AddForce(new Vector3(horizontalInputJump, 0, 0), ForceMode.Impulse);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 10f);
        }
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded == true)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && IsGrounded == false && jumCount != 0)
        {
            jumCount = jumCount - 1;
            if (gravity == true)
            {
                // Movement in mid air
                rb.AddForce(new Vector3(0, JumpforceUp * 1.5f, 0), ForceMode.VelocityChange);
            }
            if (gravity == false)
            {
                rb.AddForce(new Vector3(0, JumpforceDown * 1.5f, 0), ForceMode.VelocityChange);
            }
        }
    }
    void Jump()
    {
        IsGrounded = false;
        if (gravity == true)
        {
            // Movement in mid air
            rb.AddForce(new Vector3(0, JumpforceUp, 0), ForceMode.VelocityChange);
        }
        if (gravity == false)
        {
            rb.AddForce(new Vector3(0, JumpforceDown, 0), ForceMode.VelocityChange);
        }

    }
    void Move()
    {
        rb.AddForce(new Vector3(0, Gvalue, 0), ForceMode.Acceleration);
        float horizontalInput = Input.GetAxis("Horizontal") * Playervelocity;
        rb.AddForce(new Vector3(horizontalInput, 0, 0), ForceMode.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumCount = 1;
            IsGrounded = true;
        }
    }

    //consider when character is jumping .. it will exit collision.
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = false;
        }
    }
}

