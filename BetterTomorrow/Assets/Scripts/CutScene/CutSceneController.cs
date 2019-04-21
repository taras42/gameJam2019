using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public GameObject film1;
    public GameObject film2;

    public CharacterBehaviour character;

    private SpriteRenderer film1SpriteRenderer;
    private SpriteRenderer film2SpriteRenderer;

    private int filmSortingOrder = 5;

    private float cutSceneTriggerXPos = 18;
    private float cutSceneCharacterStopPosX = 28;
    // Start is called before the first frame update
    void Start()
    {
        film1.SetActive(false);
        film2.SetActive(false);

        film1SpriteRenderer = film1.GetComponent<SpriteRenderer>();
        film2SpriteRenderer = film2.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float characterPosX = character.transform.position.x;

        if (characterPosX >= cutSceneTriggerXPos)
        {
            character.Freeze();
            character.EnableAutoMove();
        }

        if (characterPosX >= cutSceneCharacterStopPosX)
        {
            character.SetAutoMoveDirection(0);
        }
    }
}
