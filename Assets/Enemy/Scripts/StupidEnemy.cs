using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidEnemy : MonoBehaviour
{
    public float health;

    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public float chance = 25;
    float random;

    void Start()
    {
        
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);

            random = Random.Range(0, 100);
            if (random <= chance && random <= 6)
            {
                GameObject Item = Instantiate(item1, transform.position, Quaternion.identity);
            }

            if (random <= chance && random >= 7 && random <= 12)
            {
                GameObject Item = Instantiate(item2, transform.position, Quaternion.identity);
            }

            if (random <= chance && random >= 13 && random <= 18)
            {
                GameObject Item = Instantiate(item3, transform.position, Quaternion.identity);
            }

            if (random <= chance && random >= 19 && random <= 25)
            {
                GameObject Item = Instantiate(item4, transform.position, Quaternion.identity);
            }
        }
    }
    public void TakeDamege(int damage)
    {
        health -= damage;
    }

}
        
    

