using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Rope rope;
    public Player player;
    public float ropeSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RopeEnter()
    {
        Debug.Log("RopeEnter");
    }

    public void RopeExit()
    {
        Debug.Log("RopeExit");
    }

    public void Failed()
    {
        Debug.Log("Player Failed!");
        //rope.StopSwinging();
    }
}
