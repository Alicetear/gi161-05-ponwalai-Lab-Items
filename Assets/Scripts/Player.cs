using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [field:SerializeField] public int Coin {  get; set; } = 0;
    [field:SerializeField] public int Health {  get; set; } = 50;
    public void AddCoin(int value)
    {
        Coin += value;
        Debug.Log("Picked up acoin! Total coins: " + Coin);
    }

    public void Heal(int value) 
    {
        Health += value;
        Debug.Log("Eat a Heart! Health: " + Health);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Items item = other.GetComponent<Items>();
        if (item)
        {
            item.Pickup(this);
        }
    }

    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Jump")]
    public float jumpForce = 12f;
    public float gravity = 30f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;

    private float moveInput;
    private float verticalVelocity;
    private bool isGrounded;
    private bool jumpQueued;

    void Update()
    {
        CheckGround();

        // Reset ??????????????
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = 0f;
        }

        // ??????
        if (jumpQueued && isGrounded)
        {
            verticalVelocity = jumpForce;
        }
        jumpQueued = false;

        // ???????????
        verticalVelocity += gravity * Time.deltaTime;

        // Movement ??? manual
        Vector3 movement = new Vector3(moveInput * moveSpeed, verticalVelocity, 0f) * Time.deltaTime;

        // ?????????? manual physics
        transform.position += movement;

        // Flip direction
        if (moveInput != 0)
            transform.localScale = new Vector3(moveInput > 0 ? 1 : -1, 1, 1);
    }

    void CheckGround()
    {
        // Raycast ??????????
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        isGrounded = hit.collider != null;

        // ???????????
        if (isGrounded)
        {
            // ????????????????
            float newY = hit.point.y + GetComponent<Collider2D>().bounds.extents.y;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    // Input System ????
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            jumpQueued = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}