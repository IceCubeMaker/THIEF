using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Variables
    public Rigidbody2D rb;

    public PlayerControls pControl;
    private InputAction moveAxis;

    public float Speed = 1f;

    public bool canJump = false;
    public float jumpHeight = 10f;

    // Start is called before the first frame update
    private void Awake()
    {
        pControl = new PlayerControls();
    }
    void Start()
    {
        moveAxis = pControl.PlayerMovement.Movement;
        pControl.PlayerMovement.Jump.performed += ctx => OnJump(); 

        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        rb.velocity = new Vector2(moveAxis.ReadValue<Vector2>().x*Speed*Time.deltaTime, rb.velocity.y);
    }
    private void OnJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }
    private void OnEnable()
    {
        pControl.PlayerMovement.Enable();
    }
    private void OnDisable()
    {
        pControl.PlayerMovement.Disable();
    }
}
