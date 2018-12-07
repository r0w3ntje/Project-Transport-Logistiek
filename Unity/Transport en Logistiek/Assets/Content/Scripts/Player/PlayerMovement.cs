using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb; //Reference to rigidbody (used to apply forces)

    [Header("Variables")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float MaxSlopeAngle;
    [SerializeField] private float Smoothing;
    private float Distance = 0f;
    [SerializeField] private AudioClip Footstep;

    [Header("States")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool groundHit;

    private Vector3 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
    }

    //private void Update()
    //{
    //    if (Cursor.lockState != CursorLockMode.Locked) return;

    //    Jump();
    //}

    private void FixedUpdate()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;

        Sprint();
        Move();
        StickToGround();
    }

    //private void Jump()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    //    {
    //        rb.AddForce(new Vector3(0f, jumpForce, 0f));
    //    }
    //}

    private void StickToGround()
    {
        RaycastHit hitInfo;
        if (Physics.BoxCast(transform.position - new Vector3(0, .2f, 0), new Vector3(.5f, .1f, .5f), Vector3.down, out hitInfo, transform.rotation, 1f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < MaxSlopeAngle && groundHit)
            {
                isGrounded = true;
                rb.velocity = Vector3.ProjectOnPlane(rb.velocity, hitInfo.normal);
            }
            else
            {
                isGrounded = false;
                rb.velocity = Vector3.ProjectOnPlane(rb.velocity + new Vector3(0, -2, 0), Vector3.zero);
            }
        }
        else
        {
            rb.velocity = Vector3.ProjectOnPlane(rb.velocity + new Vector3(0, -4, 0), Vector3.zero);
            isGrounded = false;
        }
        /*
        Renderer name = hitInfo.collider.GetComponent<Renderer>();
        if (name != null)
        {
            Debug.Log(name.sharedMaterial.name);
        }
        */
    }

    private void Move()
    {
        velocity = Vector3.zero;
        //Move forward or backwards
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.W)) velocity += transform.forward * 1;
            else velocity -= transform.forward * 1;
        }
        //Move left or right
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A)) velocity -= transform.right * 1;
            else velocity += transform.right * 1;
        }
        //Jump force apply
        //if (isJumping)
        //{
        //    velocity = transform.up * 4;
        //    rb.velocity += velocity;
        //    isJumping = false;
        //}
        //Check idle state
        if (velocity == new Vector3(0, 0, 0))
        {
            isIdle = true;
        }
        else isIdle = false;
        //Normalize and apply the force
        velocity.Normalize();
        rb.velocity = new Vector3(rb.velocity.x * Smoothing, rb.velocity.y * Smoothing, rb.velocity.z * Smoothing) + velocity * currentSpeed;

        if (groundHit)
        {
            Distance = Distance + new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        }

        if (Distance > 125 && groundHit)
        {
            if (isRunning)
            {
                GetComponent<AudioSource>().PlayOneShot(Footstep, Random.Range(0.6F, 0.8F));
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(Footstep, Random.Range(0.4F, 0.6F));
            }
            Distance = 0;
        }
    }

    private void Sprint()
    {
        //Hold key to sprint, release key to walk
        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
        {
            currentSpeed = runSpeed;
            isRunning = true;
        }
        else
        {
            currentSpeed = walkSpeed;
            isRunning = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            Vector3 normal = collision.contacts[0].normal;
            if (normal.y > 0.7f)
            {
                groundHit = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            groundHit = false;
        }
    }
}