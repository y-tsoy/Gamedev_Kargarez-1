using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAK : MonoBehaviour
{
    public float speed;
 
    public int damage;
    public LayerMask whatIsSolid;
    public float distance;
    public float destroyTime;

    void Start()
    {
        Invoke("DestroyBullet", destroyTime);
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<StupidEnemy>().TakeDamege(damage);
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}

