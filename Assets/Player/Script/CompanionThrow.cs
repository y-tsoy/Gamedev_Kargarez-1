using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionThrow : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D rb;
    public float speed = 10f;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }


}
