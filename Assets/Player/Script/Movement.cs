using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class Movement : MonoBehaviour
{

    public float speed;
    private Vector2 direction;
    private Rigidbody2D rb;
    Animator animator;

    public float HP = 6;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        animator.SetBool("Running", Math.Abs(direction.x)+Math.Abs(direction.y)>0);
    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealPotion"))
        {
            HP = 6;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("DmgIncrease"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("DmgDecrease"))
        {
            Destroy(collision.gameObject);
        }
    }
}
