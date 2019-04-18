using System;
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

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
    }

    void FixedUpdate()
    {
        if(frozen) { return; }
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }

    public void Die()
    {
        // to avoild putting null checks everywhere
        gameObject.SetActive(false);
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

    private IEnumerator Wait()
    {
        frozen = true;
        animator.SetBool("interactionWithElectrycityNode", true);

        yield return new WaitForSeconds(waitTime);

        animator.SetBool("interactionWithElectrycityNode", false);
        frozen = false;
    }
}
