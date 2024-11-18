using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    [SerializeField] int score;
    Rigidbody2D toyRigidbody2D;
    Animator toyAnimator;
    BoxCollider2D toyCollider2D;

    void Start()
    {
        toyAnimator = GetComponent<Animator>();
        toyRigidbody2D = GetComponent<Rigidbody2D>();
        toyCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            ToyManager.Instance.curScore += score;
            toyAnimator.SetTrigger("Get");
            toyCollider2D.isTrigger = true; // 획득 후에 캐릭터가 밀려나지 않게 하기 위함
            toyRigidbody2D.bodyType = RigidbodyType2D.Static; // 먹은 후에 애니메이션이 멈춰서 작동하게 하기 위함

        }
        else{
            Destroy(this.gameObject);
        }
    }
}
