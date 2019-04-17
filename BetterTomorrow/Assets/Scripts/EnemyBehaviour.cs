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
    public GameObject lightSwitch;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform lightSwitchPosition;

    private float horizontalMove = 1f;
    private float direction = 0f;
    private float destination = 1f;

    Collider2D characterCollider;
    Collider2D enemyCollider;

    bool isCharacterCollisionDisabled = false;

    Vector3 size;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == character.name && !character.GetVisibility())
        {
            DisableCollisionsWithCharacter();
        }
    }

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
        destination = lightSwitch.transform.position.x;
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
            destination = walkingRangeLeftBoundary;
        } 
        else if (xCoordinate <= destination && horizontalMove < 0)
        {
            horizontalMove = 1;
            destination = walkingRangeRightBoundary;
        }

        direction = horizontalMove * maxSpeed;
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
