using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderPosition : MonoBehaviour
{
    [SerializeField]
    private int sorterOrderBase = 0;
    private Renderer rend;

    private void Awake()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        rend.sortingOrder = (int)(sorterOrderBase - transform.position.y);
    }
}
