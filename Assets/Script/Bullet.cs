using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 startPosition;
    private float maxDistance;
    private float speed;
    private Vector2 direction;
    public static int damage = 100;
    public void Initialize(Vector2 direction, float speed, float maxDistance)
    {
        this.direction = direction;
        this.speed = speed;
        this.maxDistance = maxDistance;
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        if (Vector2.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
