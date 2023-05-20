using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;
    public float startTimeBtwShot;
    private float timeBtwShot;

   

    void Update()
    {
        Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

        if (timeBtwShot <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShot = startTimeBtwShot;
            }

        }
       
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }
}

