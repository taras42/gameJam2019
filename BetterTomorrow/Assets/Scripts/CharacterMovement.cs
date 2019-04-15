using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController2D controller;
    public Animator animator;

    float horizontalMove = 0f;

    public float runSpeed = 15f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
    }

    void FixedUpdate()
    {
        Debug.Log(horizontalMove);
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }

    public void TakeDamage()
    {
        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
