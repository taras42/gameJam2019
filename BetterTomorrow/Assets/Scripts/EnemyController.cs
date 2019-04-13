using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsObject
{
    public float maxSpeed = 1;
    public float walkingRange = 200;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool facingLeft = true;
    private float flipCounter = 0;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        flipCounter = flipCounter + 1;
        if (flipCounter > walkingRange)
        {
            flipCounter = 0;
            facingLeft = !facingLeft;
            Flip();
        }

        move.x = facingLeft ? -1 : 1;
        targetVelocity = move * maxSpeed;
    }

    private void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
