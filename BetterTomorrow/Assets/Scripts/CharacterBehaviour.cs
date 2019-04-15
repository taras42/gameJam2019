using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 15f;

    float horizontalMove = 0f;

    bool isCharacterVisible = true;

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
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }

    public void TakeDamage()
    {
        Die();
    }

    public void SetVisibility(bool isVisible)
    {
        isCharacterVisible = isVisible;
    }

    public bool GetVisibility()
    {
        return isCharacterVisible;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
