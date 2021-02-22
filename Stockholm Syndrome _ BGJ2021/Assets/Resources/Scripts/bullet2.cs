using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2 : MonoBehaviour
{
    public float speed;

    private Transform enemy;
    private Vector2 target;
    private Rigidbody2D rb;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        rb = GetComponent<Rigidbody2D>();
        target = new Vector2(enemy.position.x, enemy.position.y);
    }

    void Update()
    {
        rb.velocity = transform.right * speed * Time.deltaTime;
        Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

}
