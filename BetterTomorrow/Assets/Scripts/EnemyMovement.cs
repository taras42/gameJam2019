using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float maxSpeed = 10;
    public float walkingRange = 200;
    public CharacterController2D controller;
    public Animator animator;

    private float flipCounter = 0f;
    float horizontalMove = -1f;
    float direction = 0f;

    void Start()
    {

    }

    void Update()
    {
        flipCounter = flipCounter + 1;

        if (flipCounter > walkingRange)
        {
            flipCounter = 0;
            horizontalMove = horizontalMove * -1f;
        }

        direction = horizontalMove * maxSpeed;

        //animator.SetFloat("speed", Mathf.Abs(horizontalMove));
    }

    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime, false, false);
    }
}
