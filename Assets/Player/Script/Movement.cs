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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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


}
