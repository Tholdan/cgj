using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //PUBLIC
    [Header("Walk")]
    public float verticalWalkSpeed;
    public float horizontalWalkSpeed;

    [Space]

    [Header("Rotate")]
    [Range(0, 360)]
    public float degreesPerSecond;

    [Space]

    [Header("Jump")]
    public float firstJumpVelocity;
    public float secondJumpVelocity;
    [Range(0, 1)] public float fallingJumpMultiplier;

    //PRIVATE
    private Rigidbody rb;

    private bool jumping;
    private bool firstJump;
    private bool secondJump;

    private float verticalMovement;
    private float horizontalMovement;
    private float turn;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        Walk();
        Rotate();
        Jump();
    }

    void Walk()
    {
        verticalMovement = Input.GetAxis("Vertical") * verticalWalkSpeed * Time.fixedDeltaTime;
        horizontalMovement = Input.GetAxis("Horizontal") * horizontalWalkSpeed * Time.fixedDeltaTime;

        transform.position += transform.forward * verticalMovement;
        transform.position += transform.right * horizontalMovement;
    }

    void Rotate()
    {
        turn = Input.GetAxis("Mouse X") * degreesPerSecond * Time.fixedDeltaTime;
        transform.Rotate(Vector3.up * turn);
    }

    void Jump()
    {
        if (!jumping && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, firstJumpVelocity, rb.velocity.z);
            jumping = true;
            firstJump = true;
        }
        else
        {
            if (firstJump && Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector3(rb.velocity.x, secondJumpVelocity, rb.velocity.z);
                firstJump = false;
                secondJump = true;
            }
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 1 - fallingJumpMultiplier, rb.velocity.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        RaycastHit rayHitInfo;
        Ray downRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(downRay, out rayHitInfo, 0.5f))
        {
            jumping = false;
            firstJump = false;
            secondJump = false;
        }
    }
}
