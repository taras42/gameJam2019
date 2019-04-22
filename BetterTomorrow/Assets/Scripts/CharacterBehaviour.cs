using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public CharacterController2D controller;
    public MonoBehaviour gameOverScene;

    public Animator animator;
    public float runSpeed = 15f;
    public int waitTime = 3;

    private float horizontalMove = 0f;
    private bool isCharacterVisible = true;
    private bool frozen = false;
    private Rigidbody2D m_Rigidbody2D;

    private bool autoMove = false;
    private float autoMoveDirection = 1f;
    private Vector3 firtsLevelStartPosition = new Vector2(-7.5f, 0.33f);

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
        gameObject.SetActive(false);
        gameOverScene.gameObject.SetActive(true);
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
