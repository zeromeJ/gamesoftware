using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCollider : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void HitGround()
    {
        Debug.Log("HitGround");
        boxCollider.enabled = true;
    }
    public void OverGround()
    {
        boxCollider.enabled = false;
    }
}
