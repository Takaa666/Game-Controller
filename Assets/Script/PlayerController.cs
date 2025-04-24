using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Properties")]
    [SerializeField] private SpriteRenderer graphic;

    [Header("Status")]
    public float health = 100;
    public float attack = 5;

    public bool canBeMoved = true;

    public float healthMax { private set; get; }

    private bool isGrounded = true;
    private bool isShooting = true;

    [Header("Configuration")]
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float jumpForce = 5.0f;
    // [SerializeField] private float shootingTimeMax = 1.0f;
    // [SerializeField] private GameObject bulletPrefab;
    // [SerializeField] private float bulletSpeed = 10;

    // [SerializeField] private Vector2 gunOffset;

    // private float shootingTime = 0;

    [SerializeField] private float minGroundDistance = 1.5f;

    private Rigidbody2D rb2;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();

        // shootingTime = shootingTimeMax;
        healthMax = health;
    }

    void Update()
    {
        if (canBeMoved)
        {
            MovementController();
            JumpController();
            // ShootController();
        }
    }

    void MovementController()
    {
        float x = Input.GetAxisRaw("Horizontal");

        Vector2 direction = rb2.velocity;
        direction.x = x * moveSpeed;

        rb2.velocity = direction;

        //sprite dibalik ketika arahnya ke kiri
        if (direction.x < 0)
        {
            graphic.flipX = true;
        }
        else if (direction.x > 0)
        {
            graphic.flipX = false;
        }
    }

    void JumpController()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 10, LayerMask.GetMask("Obstacle"));

        if (ray && ray.distance < minGroundDistance)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb2.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // void ShootController()
    // {
    //     isShooting = Input.GetKey(KeyCode.Z);

    //     if (isShooting)
    //     {
    //         shootingTime -= Time.deltaTime;
    //     }
    //     else
    //     {
    //         shootingTime = shootingTimeMax;
    //     }

    //     if (isShooting && shootingTime < 0)
    //     {
    //         shootingTime = shootingTimeMax;
    //         Shoot();
    //     }
    // }

    // void Shoot()
    // {
    //     int direction = (graphic.flipX == false ? 1 : -1);

    //     Vector2 gunPos = new Vector2(gunOffset.x * direction + transform.position.x, gunOffset.y + transform.position.y);

    //     GameObject bulletObj = Instantiate(bulletPrefab, gunPos, Quaternion.identity);
    //     Bullet bullet = bulletObj.GetComponent<Bullet>();

    //     if (bullet)
    //     {
    //         bullet.Launch(new Vector2(direction, 0), "Enemy", bulletSpeed, attack);
    //     }
    // }
}