using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAnimation : MonoBehaviour
{
    Animator animator;
    public float ropeSpeed = 0.5f;
    private bool IsReverse = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", ropeSpeed);
    }

    public void ChangeState()
    {
        IsReverse = !IsReverse;
        animator.SetBool("IsReverse", IsReverse);
    }
}
