using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Variables
    public Rigidbody2D rb;

    public PlayerControls pControl; //Player Input Script [ Auto generated from input asset file ]
    private InputAction moveAxis;

    public float Speed = 1f;

    public bool canJump = false;
    public bool jumped = false;
    public float jumpHeight = 10f;
    public float groundsCheckDist = 1f;
    public LayerMask groundLayer;
    private float lastYPos;

    [Header("Responsiveness")]
    public float fallJumpTimeOut = 0.1f;
    public float fallJumpTime = 0; // To be made private

    // Start is called before the first frame update
    private void Awake()
    {
        pControl = new PlayerControls(); //Instantiating or Creating an Object for Input Class to detect Input
    }
    void Start()
    {
        moveAxis = pControl.PlayerMovement.Movement;
        pControl.PlayerMovement.Jump.performed += ctx => OnJump(); //Binding Jump Action to Jump Function
        GameManager.Instance.player = this;
        rb = this.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        GroundedJumpCheck();
    }
    void FixedUpdate() //For Physics related stuff
    {
        MovePlayer();
    }

    private void OnDrawGizmos() //Debug drawings
    {
        Gizmos.DrawLine(transform.position, transform.position - transform.up * groundsCheckDist);
    }


    private void MovePlayer()
    {
        rb.velocity = new Vector2(moveAxis.ReadValue<Vector2>().x*Speed*Time.deltaTime, rb.velocity.y);
    }
    private void OnJump() //Called when Jump Button pressed
    {
        if(!canJump || fallJumpTime <= 0 || jumped) { return; }
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        jumped = true;
    }
    private bool GroundedJumpCheck()
    {
        if (lastYPos != rb.position.y)//If not stationary
        {
            fallJumpTime -= Time.deltaTime;
        }
        if(Physics2D.Raycast(transform.position, -transform.up, groundsCheckDist, groundLayer)) //If hits ground
        { 
            if(fallJumpTime != fallJumpTimeOut) { jumped = false; }
            fallJumpTime = fallJumpTimeOut;
        }

        lastYPos = rb.position.y;
        return fallJumpTime>0;
    }

    private void OnEnable()
    {
        //Enables player controls
        pControl.PlayerMovement.Enable();
    }
    private void OnDisable()
    {
        //Disables Player Controls
        pControl.PlayerMovement.Disable();
    }
}
