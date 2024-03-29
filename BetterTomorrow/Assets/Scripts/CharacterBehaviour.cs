﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public CharacterController2D controller;

    public Animator animator;
    public float runSpeed = 15f;
    public int waitTime = 3;

    private float horizontalMove = 0f;
    private bool isCharacterVisible = true;
    private bool frozen = false;
    private Rigidbody2D m_Rigidbody2D;

    private bool autoMove = false;
    private float autoMoveDirection = 1f;

    private bool isDead = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = GetHorizontalMove() * runSpeed;

        if (!frozen || autoMove)
        {
            animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        }
    }

    void FixedUpdate()
    {
        if(!frozen || autoMove)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
        }
    }

    public float GetHorizontalMove()
    {
        if (autoMove)
        {
            return autoMoveDirection;
        } 
        else
        {
            return Input.GetAxisRaw("Horizontal");
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void EnableAutoMove()
    {
        autoMove = true;
    }

    public void DisableAutoMove()
    {
        autoMove = false;
    }

    public void SetAutoMoveDirection(float direction)
    {
        autoMoveDirection = direction;
    }

    public void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void RessurectAt(float x, float y)
    {
        isDead = false;
        gameObject.SetActive(true);

        gameObject.transform.position = new Vector2(x, y);
    }

    public void SetVisibility(bool isVisible)
    {
        isCharacterVisible = isVisible;
    }

    public bool GetVisibility()
    {
        return isCharacterVisible;
    }

    public void interactionWithElectrycityNode()
    {
        StartCoroutine(Wait());
    }

    public bool IsFrozen()
    {
        return frozen;
    }

    public void Freeze()
    {
        frozen = true;
    }

    public void UnFreeze()
    {
        frozen = false;
    }

    public void PullDown(float forceY)
    {
        m_Rigidbody2D.AddForce(new Vector2(0f, forceY));
    }

    private IEnumerator Wait()
    {
        Freeze();
        animator.SetBool("interactionWithElectrycityNode", true);

        yield return new WaitForSeconds(waitTime);

        animator.SetBool("interactionWithElectrycityNode", false);
        UnFreeze();
    }
}
