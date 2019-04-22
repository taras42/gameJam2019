using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public CharacterBehaviour character;
    public ElevatorController elevatorToTheSecondFloor;
    public ElevatorController elevatorToTheThirdFloor;

    public Text gameOverText;
    public Text gameOverInfo;

    private float firstFloorPositionX = -7.5f;
    private float firstFloorPositionY = 0.33f;
     
    private float secondFloorPositionX = 85.5f;
    private float secondFloorPositionY = 13.18f;

    private float floorHeight = 13f;

    private bool showGameOverScreen = false;
    private bool gameScreenShown = false;

    private bool restart = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (character.IsDead() && !showGameOverScreen)
        {
            showGameOverScreen = true;
        }

        if (character.IsDead() && Input.GetAxisRaw("Submit") > 0)
        {
            restart = true;
        }

        if (Input.GetAxisRaw("Cancel") > 0)
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        if (showGameOverScreen)
        {
            if (!gameScreenShown)
            {
                gameOverText.gameObject.SetActive(true);
                gameOverInfo.gameObject.SetActive(true);
            }

            gameScreenShown = true;
        }

        if (restart)
        {
            restart = false;

            showGameOverScreen = false;
            gameScreenShown = false;
            gameOverText.gameObject.SetActive(false);
            gameOverInfo.gameObject.SetActive(false);

            elevatorToTheSecondFloor.ResetElevator();
            elevatorToTheThirdFloor.ResetElevator();

            Vector3 characterPos = character.GetPosition();

            if (characterPos.y >= floorHeight)
            {
                character.RessurectAt(secondFloorPositionX, secondFloorPositionY);
            } else
            {
                character.RessurectAt(firstFloorPositionX, firstFloorPositionY);
            }
        }
    }
}
