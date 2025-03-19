using NodeCanvas.Tasks.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;

    const float groundCheckRadius = 0.2f;
    float horizontalValue;
    [SerializeField] float speed = 1;
    [SerializeField] float jumpPower = 300;
    public Animator anim;
    public int damage = 100;
    public SubscribeManager coinManager;
    public int playButtonCount;


    [SerializeField] bool isGrounded;
    bool jump;
    bool facingRight = true; // To track which direction the player is facing

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Ensure the Animator is assigned
    }

    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal"); // Horizontal Input
        if (Input.GetButtonDown("Jump")) // Input for Jump
            jump = true;
        else if (Input.GetButtonUp("Jump")) // Cancel Jump
            jump = false;

        // Handle animations based on input
        if (isGrounded)
        {
            if (horizontalValue != 0)
            {
                anim.SetTrigger("Walk"); // Walking animation
            }
            else
            {
                anim.SetTrigger("Idle"); // Idle animation
            }
        }

        FlipSprite(horizontalValue); // Check for flipping the sprite
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue, jump);
    }

    void Move(float dir, bool jumpFlag)
    {
        if (isGrounded && jumpFlag)
        {
            isGrounded = false;
            jumpFlag = false;
            rb.AddForce(new Vector2(0f, jumpPower)); // Jump force
            anim.SetTrigger("Jump"); // Jump animation
        }

        #region Movement
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        Vector2 targetvelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetvelocity;
        #endregion
    }

    void GroundCheck()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
    }

    void FlipSprite(float horizontal)
    {
        // Flip the sprite based on the movement direction
        if (horizontal > 0 && !facingRight)
        {
            Flip(); // Face right
        }
        else if (horizontal < 0 && facingRight)
        {
            Flip(); // Face left
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // Flip the x-axis
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek apakah objek yang ditabrak adalah enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {   
            // Dapatkan posisi player dan enemy
            float playerY = transform.position.y;
            float enemyY = collision.transform.position.y;

            // Jika player berada di atas enemy
            if (playerY > enemyY)
            {
                // Panggil metode untuk mengurangi kesehatan enemy
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);

                // Di sini kita tidak menambahkan efek melambungkan player
                // Namun, kamu bisa menambahkan efek visual atau suara jika perlu
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Silver Play Button")
        {
            Destroy(other.gameObject);
            coinManager.coinCount ++;
            
        }
    }
}