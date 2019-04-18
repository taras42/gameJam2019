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
    public Transform firstLightSwitch;
    public Transform secondLightSwitch;

    private float horizontalMove = 1f;
    private float direction = 0f;
    private float destination = 1f;

    Collider2D characterCollider;
    Collider2D enemyCollider;

    bool isCharacterCollisionDisabled = false;

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

        bool shouldFireAtCharacter = hitInfo.distance > 0 && hitInfo.distance <= shootTargetWithinRange;

        if (character != null && character && shouldFireAtCharacter && character.GetVisibility())
        {
            animator.SetTrigger("Fire");
        }
    }

    public void TurnOnLightQuicly()
    {
        shootTargetWithinRange = 5f;
        maxSpeed = maxSpeed * 2;

        GoToTheNearestLightSwitcher();
    }

    public void GoTo(float dest)
    {
        float enemyX = transform.position.x;
        horizontalMove = enemyX > dest ? -1 : 1;

        destination = dest;
    }

    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime, false, false);
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
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

    private void GoToTheNearestLightSwitcher()
    {
        float firstX = firstLightSwitch.transform.position.x;
        float secondX = secondLightSwitch.transform.position.x;

        float enemyX = transform.position.x;

        if (Math.Abs(secondX - enemyX) > Math.Abs(firstX - enemyX))
        {
            GoTo(firstX);
        }
        else
        {
            GoTo(secondX);
        }
    }
}
