using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D collideCheck;
    public float ropeSpeed = 0.5f;
    private bool isReversing = false;
    private bool playerHit = false;
    private bool dummyHit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 더미가 먼저 맞는 현상 있음 어캐 수정하지
        //if (collision.gameObject.name == "Player")
        //{
        //    // 플레이어가 줄에 걸렸을 때 로직 구현하기
        //    Debug.Log("Player Hit! Game End");
        //    return;
        //}
        //else
        //{
        //    // 더미가 맞았을 때 (줄넘기를 넘었으니 점수 획득 로직)
        //    Debug.Log(collision.gameObject);
        //}
        // 플레이어가 먼저 충돌하는지 확인
        if (collision.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject);
            playerHit = true;
            return; // 플레이어가 감지되면 종료
        }

        // 플레이어가 감지되지 않은 경우 더미 처리
        if (collision.CompareTag("Dummy"))
        {
            // 더미가 맞았을 경우 처리
            Debug.Log(collision.gameObject);
            dummyHit = true;
        }
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collideCheck = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 현재 애니메이션 상태 정보 가져오기
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 애니메이션이 종료되었고, 루프가 꺼져 있을 때
        if (stateInfo.normalizedTime >= 1.0f && !isReversing)
        {
            // speed를 -1로 설정하여 반대로 재생
            animator.SetFloat("Speed", ropeSpeed * - 1);
            isReversing = true;
            spriteRenderer.sortingOrder++;
        }

        // 애니메이션이 반대로 재생되는 동안
        if (isReversing)
        {
            // 애니메이션이 끝나면 다시 원래 속도로 설정
            if (stateInfo.normalizedTime <= 0.0f)
            {
                animator.SetFloat("Speed", ropeSpeed);
                isReversing = false;
                spriteRenderer.sortingOrder--;
            }
        }

       
        
    }
    private void LateUpdate()
    {
        if (dummyHit && !playerHit)
        {
            // 점수 증가 로직
            IncreaseScore();
            dummyHit = false; // 플래그 초기화
        }

        // 플레이어가 히트된 경우 게임 오버 처리
        if (playerHit)
        {
            // 게임 오버 처리 로직
            //HandleGameOver();
            Debug.Log("GameOver");
            playerHit = false; // 플래그 초기화
        }

    }
    private void FixedUpdate()
    {
        if (spriteRenderer.sprite.name == "rope_2" && isReversing)
        {
            collideCheck.enabled = true;
        }
        else
        {
            collideCheck.enabled = false;
        }
    }

    private void IncreaseScore()
    {
        Debug.Log("점수 +1");
    }
}
