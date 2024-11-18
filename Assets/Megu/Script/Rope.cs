using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Animator animator;
    [SerializeField] float ropeSpeed = 0.5f;
    private bool IsReverse = false;
    public GameManager manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            animator.enabled = false;
        }
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", ropeSpeed);
    }

    private void LateUpdate()
    {

    }

    

    //------------------//
    // Custom Functions //
    //------------------//

    // 줄넘기 줄이 플레이어에 닿을 수 있는 순간
    public void HitGround()
    {
        boxCollider.enabled = true;
    }

    // 줄넘기 줄이 플레이어 너머로 넘어간 순간
    public void OverGround()
    {
        boxCollider.enabled = false;
        manager.SendMessage("ScoreUp");
    }

    // 줄넘기 정방향인지 역방향인지
    public void ChangeState()
    {
        IsReverse = !IsReverse;
        animator.SetBool("IsReverse", IsReverse);
    }

    private void SetRopeSpeed(float speed)
    {
        ropeSpeed = speed;
    }
}
