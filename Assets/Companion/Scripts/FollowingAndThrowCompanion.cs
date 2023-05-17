using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingAndThrowCompanion : MonoBehaviour
{

    public Rigidbody2D rb;
    private Transform player;

    public float speedOfFoll = 5f;      //Скорость преследования

    private bool isCompanionThrown = false;
    public float CooldownTime = 5f;
    public float speedOfThrow = 1f;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float targetX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        startPosition = transform.position;
        endPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            isCompanionThrown = true;
            targetX = 3f;
            endPosition = new Vector2(startPosition.x + targetX, startPosition.y);
            /* Vector2 startPosition = transform.position;
             Vector2 endPosition = new Vector2(startPosition.x + 7, startPosition.y);
             float distance = Vector2.Distance(startPosition, endPosition);
             float timeOfThrow = distance / speedOfThrow;

             Vector2 direction = (endPosition - startPosition).normalized;
             rb.velocity = new Vector2(distance, direction.y);*/

        }

        if (isCompanionThrown)
        {   
            startPosition = transform.position;
            transform.position = Vector2.MoveTowards(startPosition, endPosition, speedOfThrow * Time.deltaTime);

            if (Vector2.Distance(transform.position, endPosition) < 0.2f)
            {
                isCompanionThrown = false;
            }
        }

        if (isCompanionThrown == false)
        {

        }
    }
}
