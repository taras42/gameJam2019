using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float maxSpeed = 10;
    public float walkingRange = 200;
    public CharacterController2D controller;
    public Animator animator;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private float flipCounter = 0f;
    float horizontalMove = -1f;
    float direction = 0f;

    void Start()
    {
        FireAnimationController exampleSmb = animator.GetBehaviour<FireAnimationController>();
        exampleSmb.enemy = this;
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
    }

    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime, false, false);

        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        CharacterMovement character = hitInfo.transform.GetComponent<CharacterMovement>();
        if (character != null && character)
        {
            Debug.Log(hitInfo.distance);
            animator.SetTrigger("Fire");
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
