using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    public CharacterBehaviour character;
    public float floor = 12;
    public float elevatorSpeed = 1;

    private bool characterNearTheElevator = false;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 position;
    private bool elevatorMove = false;
    private bool elevatorActive = true;
    private float elevatorCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        //elevatorCounter = floor;
    }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool iterationKeyPressed = Input.GetAxisRaw("Iteract") > 0;
        if (characterNearTheElevator && iterationKeyPressed && !elevatorMove && elevatorActive)
        {
            elevatorMove = true;
        }

        if (elevatorMove)
        {
            ElevatorMove();
        }
    }

    private void ElevatorMove()
    {
        position = transform.position;
        transform.position = new Vector2(position.x, position.y + elevatorSpeed);

        character.ElevatorMove(transform.position);

        elevatorCounter = elevatorCounter + elevatorSpeed;
       
        if (elevatorCounter >= floor)
        {
            Debug.Log("el " + elevatorCounter);
            elevatorMove = false;
            elevatorActive = false;
            elevatorCounter = 0;
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
