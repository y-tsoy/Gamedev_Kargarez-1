using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ulti : MonoBehaviour
{

    public Rigidbody2D rb;

    public float speedOfThrow = 10f;
    //Переменная для фиксации состояния "Кастую ульту"
    private bool isThrown = false;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float targetX = 0f;
    private float targetY = 0f;


    private void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position;
    }

    
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            //Начинает кастовать
            isThrown = true;
            targetX = 3f;
            endPosition = new Vector2(startPosition.x + targetX, startPosition.y);
        }
        //Пока кастует..
        if (isThrown)
        {
            //Делает все вычисления, передвигается, всё чики пуки
            startPosition = transform.position;
            transform.position = Vector2.MoveTowards(startPosition, endPosition, speedOfThrow * Time.deltaTime);

            //И когда дистанция сократилась достаточно...
            if (Vector2.Distance(transform.position, endPosition) < 0.5f)
            {
                //То состояние "Кастую ульту" выключается
                isThrown = false;
            }
        }
          

    }

}
