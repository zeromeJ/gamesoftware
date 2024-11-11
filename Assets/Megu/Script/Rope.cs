using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public float RopeSpeed = 0.5f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        Debug.Log("Rope Setup");
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        // 애니메이션이 종료되었는지 확인 (NormalizedTime이 1이면 종료)
        if (animStateInfo.normalizedTime >= 1.0f && !animStateInfo.loop)
        {
            Debug.Log("애니메이션이 종료되었습니다.");
            animator.SetFloat("Speed", RopeSpeed * -1);
            animator.Play(0);
        }
    }

    
}
