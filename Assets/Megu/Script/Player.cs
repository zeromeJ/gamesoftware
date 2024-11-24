using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpPower = 12.0f;
    [SerializeField] CountDown countdown;
    [SerializeField] bool canJump = true;
    [SerializeField] GameManager manager;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown.GetCountDownDone() && canJump)
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
        // PC 스페이스바
        if (Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("IsJump"))
        {
            rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("IsJump", true);
            audioSource.Play();
        }
        // mobile 터치
        if (Input.touchCount > 0 && !animator.GetBool("IsJump"))
        {
            rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("IsJump", true);
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Rope")
        {
            manager.StuckRope();
            canJump = false;
        }
    }
}
