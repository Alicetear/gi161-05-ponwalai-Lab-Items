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

    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    [System.Obsolete]
    void Update()
    {
        float move = 0f;

        // ????????
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            move = -1f;

        // ???????
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            move = 1f;

        // ?????????? velocity
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // ?????? (????????????????? Sun ??)
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Flip ????
        if (move != 0)
            transform.localScale = new Vector3(move > 0 ? 1 : -1, 1, 1);
    }
}