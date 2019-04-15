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

    public Transform firePoint;
    public GameObject bulletPrefab;

    float horizontalMove = -1f;
    float direction = 0f;
    float halfWidth;

    Vector3 size;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        size = renderer.bounds.size;
        halfWidth = size[0] / 2;

        FireAnimationController exampleSmb = animator.GetBehaviour<FireAnimationController>();
        exampleSmb.enemy = this;
    }

    void Update()
    {
        Vector3 pos = transform.position;

        float xCoordinate = pos[0];

        if (horizontalMove < 0 && xCoordinate - halfWidth <= walkingRangeLeftBoundary)
        {
            horizontalMove = horizontalMove * -1f;
        } else if (horizontalMove > 0 && xCoordinate + halfWidth >= walkingRangeRightBoundary)
        {
            horizontalMove = horizontalMove * -1f;
        }

        direction = horizontalMove * maxSpeed;
    }

    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime, false, false);

        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        CharacterBehaviour character = hitInfo.transform.GetComponent<CharacterBehaviour>();

        bool shouldFireAtCharacter = hitInfo.distance > 0 && hitInfo.distance <= shootTargetWithinRange;

        if (character != null && character && shouldFireAtCharacter && character.GetVisibility())
        {
            animator.SetTrigger("Fire");
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
