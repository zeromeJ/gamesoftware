using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCharacter : MonoBehaviour
{
    public float jumpPower = 12.0f;
    private Rigidbody2D rigidBody;
    private Animator animator;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 일단은 스페이스바로 점프
        if(Input.GetKeyDown(KeyCode.Space) /*&& !animator.GetBool("IsJump")*/)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Landing Platform
        if(rigidBody.velocity.y < 0)
        {
            Debug.DrawRay(rigidBody.position, Vector3.down, Color.red);
            RaycastHit2D raycastHit = Physics2D.Raycast(rigidBody.position, Vector2.down, 2.5f, LayerMask.GetMask("Platform"));
            if(raycastHit.collider != null )
            {
                if (raycastHit.distance < 2.0f)
                {   
                    animator.SetBool("IsJump", false);
                }
            }
        }
    }

    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        animator.SetBool("IsJump", true);
    }
}
