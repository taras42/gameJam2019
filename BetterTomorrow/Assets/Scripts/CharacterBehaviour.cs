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
    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

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

    public void MoveVerticaly()
    {
        m_Rigidbody2D.velocity = (new Vector2(m_Rigidbody2D.velocity.x, 10));
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

    public void ElevatorMove(Vector3 position)
    {
        m_Rigidbody2D.position = position;
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
