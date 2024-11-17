using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    [SerializeField] int score;
    Rigidbody2D toyRigidbody2D;
    CapsuleCollider2D toyCollider2D;

    void Start()
    {
        toyRigidbody2D = GetComponent<Rigidbody2D>();
        toyCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            //점수 획득 미구현
        }
        Destroy(this.gameObject);
        //toyCollider2D.isTrigger = true; // 획득 후에 캐릭터가 밀려나지 않게 하기 위함
        //toyRigidbody2D.bodyType = RigidbodyType2D.Static; // 먹은 후에 애니메이션이 멈춰서 작동하게 하기 위함
    }
}
