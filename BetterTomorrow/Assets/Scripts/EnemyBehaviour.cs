using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float maxSpeed = 10;
    public float walkingRangeLeftBoundary = 48f;
    public float walkingRangeRightBoundary = 82f;
    public float shootTargetWithinRange = 15f;
    public CharacterController2D controller;
    public Animator animator;
    public CharacterBehaviour character;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private float horizontalMove = 1f;
    private float direction = 0f;
    private float destination = 1f;

    Collider2D characterCollider;
    Collider2D enemyCollider;

    bool isCharacterCollisionDisabled = false;

    bool gunLoaded = false;

    Vector3 size;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        size = renderer.bounds.size;
        destination = walkingRangeRightBoundary;

        FireAnimationController exampleSmb = animator.GetBehaviour<FireAnimationController>();
        exampleSmb.enemy = this;

        characterCollider = character.GetComponent<Collider2D>();
        enemyCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        CalculateDirection();
        EnableCollisionsWithCharacterIfItsVisible();

        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        if (character != null)
        {
            bool targetWithinRange = hitInfo.distance > 0f && hitInfo.distance <= shootTargetWithinRange;
            bool targetIsCharacter = hitInfo.transform.position.x == character.transform.position.x;

            if (targetWithinRange && targetIsCharacter && character.GetVisibility())
            {
                gunLoaded = true;
                animator.SetTrigger("Fire");
            }
        }
    }

    public void GoTo(float dest)
    {
        float enemyX = transform.position.x;
        horizontalMove = enemyX > dest ? -1 : 1;

        destination = dest;
    }

    void FixedUpdate()
    {
        if (!gunLoaded)
        {
            controller.Move(direction * Time.fixedDeltaTime, false, false);
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void Reload()
    {
        gunLoaded = false;
    }

    private void CalculateDirection()
    {
        float xCoordinate = transform.position.x;

        if (xCoordinate >= destination && horizontalMove > 0)
        {
            horizontalMove = -1;
            GoTo(walkingRangeLeftBoundary);
        } 
        else if (xCoordinate <= destination && horizontalMove < 0)
        {
            horizontalMove = 1;
            GoTo(walkingRangeRightBoundary);
        }

        direction = horizontalMove * maxSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == character.name && !character.GetVisibility())
        {
            DisableCollisionsWithCharacter();
        }
    }

    private void EnableCollisionsWithCharacterIfItsVisible()
    {
        if (character.GetVisibility())
        {
            EnableCollisionsWithCharacter();
        }
    }

    private void DisableCollisionsWithCharacter()
    {
        isCharacterCollisionDisabled = true;
        Physics2D.IgnoreCollision(characterCollider, enemyCollider);
    }

    private void EnableCollisionsWithCharacter()
    {
        if (isCharacterCollisionDisabled)
        {
            isCharacterCollisionDisabled = false;
            Physics2D.IgnoreCollision(characterCollider, enemyCollider, false);
        }
    }
}
