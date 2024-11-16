using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RT_Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] Timer timer;
    [SerializeField] CountDown countdown;
    Rigidbody2D playerRb2;
    float horizontal;
    Animator playerAnimator;
    SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        playerRb2 = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if(!timer.GetTimerIsDone() & countdown.GetCountDownDone()){
            PlayerMove();
            ScreenChk();
        }
    }

    void PlayerMove(){
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));
        playerRb2.velocity = new Vector2(horizontal * moveSpeed, playerRb2.velocity.y);
        if(horizontal < 0){
            playerSpriteRenderer.flipX = true;
        }
        else{
            playerSpriteRenderer.flipX = false;
        }

        //모바일 이동
        if(Input.touchCount > 0){
            if(Input.GetTouch(0).position.x > 1080/2){
                playerRb2.velocity = new Vector2(moveSpeed, playerRb2.velocity.y);
            }
            else{
                playerRb2.velocity = new Vector2(-moveSpeed, playerRb2.velocity.y);
            }
        }
    }

    void ScreenChk(){ // 스크린 안에서 이동 제한
        Vector3 worldpos = Camera.main.WorldToViewportPoint(this.transform.position);
        if(worldpos.x < 0.05f) worldpos.x = 0.05f;
        if(worldpos.x > 0.95f) worldpos.x = 0.95f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worldpos);
    }
}
