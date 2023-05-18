using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoodiesForFollowing;

public class FollowingAndThrowCompanion : MonoBehaviour
{
<<<<<<< Updated upstream

    public Rigidbody2D rb;
    private Transform player;

    public float speedOfFoll = 5f;      //—корость преследовани€

    private bool isCompanionThrown = false;
    public float CooldownTime = 5f;
    public float speedOfThrow = 1f;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float targetX = 0f;
=======
    //private bool isCompanionThrown = false;
    //public float CooldownTime = 5f;
    public float speedOfFoll = 5f;                   //—корость преследовани€
    //public float speedOfThrow = 10f;
    public Rigidbody2D rb;
    Transform target;
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
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
=======
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    isCompanionThrown = true;
        //    Vector2 startPosition = transform.position;
        //    Vector2 endPosition = new Vector2(startPosition.x + 7, startPosition.y);
        //   float distance = Vector2.Distance(startPosition, endPosition);
        //    float timeOfThrow = distance / speedOfThrow;
        //
        //    Vector2 direction = (endPosition - startPosition).normalized;
        //   rb.velocity = new Vector2(distance, direction.y);

       // }
        //¬от тут надо бы проверку isThrown забацать, чтобы он не бегал за игроком, пока ультит
        //ћожно сделать все в одном файле, типа эту строку перенести на когда isThrown == false
        //ј можно сделать публичный статический класс, в котором будут только переменные хранитьс€
        //“ипа статов, хп, урон вс€ шл€па, и туда же впихнуть isThrown
        //ј так как этот класс будет статичным, то к его переменным можно будет обращатьс€ отовсюду,
        //» смотреть их и измен€ть
        //¬от так как-то
        if (Ulti.isThrown == false)
        //{
            transform.position = Vector2.MoveTowards(transform.position, target.position, speedOfFoll * Time.deltaTime);
        //}
>>>>>>> Stashed changes
    }
}
