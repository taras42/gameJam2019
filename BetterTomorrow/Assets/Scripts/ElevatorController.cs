using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    public CharacterBehaviour character;
    public float floor = 12;
    public float elevatorSpeed = 1;
    public float characterPullDownForce = -200f;

    private bool characterNearTheElevator = false;
    private Rigidbody2D m_Rigidbody2D;
    private AudioSource audioSource;
    private Vector3 position;
    private bool elevatorMove = false;

    private float startPosition;
    private bool upDirection = true;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.y;
    }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool iterationKeyPressed = Input.GetAxisRaw("Iteract") > 0;

        if (characterNearTheElevator && iterationKeyPressed && !elevatorMove)
        {
            elevatorMove = true;
            audioSource.Play();
        }
    }

    private void FixedUpdate()
    {
        if (elevatorMove)
        {
            ElevatorMove();
        }
    }

    private void ElevatorMove()
    {
        character.PullDown(characterPullDownForce);

        position = transform.position;

        float directionModifier = upDirection ? 1 : -1;

        transform.position = new Vector2(position.x, (position.y + (elevatorSpeed * Time.fixedDeltaTime * directionModifier)));
       
        if (upDirection)
        {
            if (transform.position.y >= floor)
            {
                elevatorMove = false;
                upDirection = false;

                audioSource.Pause();
            }
        } else
        {
            if (transform.position.y <= startPosition)
            {
                elevatorMove = false;
                upDirection = true;

                audioSource.Pause();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == character.name)
        {
            characterNearTheElevator = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == character.name)
        {
            characterNearTheElevator = false;
        }
    }
}
