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
    private float cutSceneTriggerYPos = 24;
    private float cutSceneCharacterStopPosX = 28;

    private bool startMoveFilms = false;
    private bool filmsFinishedMoving = false;
    private float filmSpeed = 3f;

    private float film1PosY;
    private float film2PosY;
    // Start is called before the first frame update
    void Start()
    {
        film1.gameObject.SetActive(false);
        film2.gameObject.SetActive(false);

        film1SpriteRenderer = film1.GetComponent<SpriteRenderer>();
        film2SpriteRenderer = film2.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float characterPosX = character.transform.position.x;
        float characterPosY = character.transform.position.y;

        if (characterPosX >= cutSceneTriggerXPos && characterPosY >= cutSceneTriggerYPos && !filmsFinishedMoving)
        {
            character.Freeze();
            character.EnableAutoMove();

            ActivateFilms();
        }

        if (characterPosX >= cutSceneCharacterStopPosX && characterPosY >= cutSceneTriggerYPos && !filmsFinishedMoving)
        {
            character.SetAutoMoveDirection(0);
            startMoveFilms = false;
            filmsFinishedMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if (startMoveFilms)
        {
            film1.transform.position = new Vector2(film1.transform.position.x, film1.transform.position.y - (filmSpeed * Time.fixedDeltaTime));
            film2.transform.position = new Vector2(film2.transform.position.x, film2.transform.position.y + (filmSpeed * Time.fixedDeltaTime));
        }
    }

    private void ActivateFilms()
    {
        startMoveFilms = true;

        film1SpriteRenderer.sortingOrder = filmSortingOrder;
        film2SpriteRenderer.sortingOrder = filmSortingOrder;

        film1.gameObject.SetActive(true);
        film2.gameObject.SetActive(true);
    }
}
