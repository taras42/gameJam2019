using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneController : MonoBehaviour
{
    public GameObject film1;
    public GameObject film2;

    public CharacterBehaviour character;

    public Text conversationTextComponent;
    public Text toBeContinuedTextComponent;

    private SpriteRenderer film1SpriteRenderer;
    private SpriteRenderer film2SpriteRenderer;

    private int filmSortingOrder = 5;

    private float cutSceneTriggerXPos = 18;
    private float cutSceneTriggerYPos = 24;
    private float cutSceneCharacterStopPosX = 28;

    private bool startMoveFilms = false;
    private bool filmsFinishedMoving = false;
    private bool conversationStarted = false;
    private bool conversationFinished = false;
    private bool showToBeContinuedText = false;

    private float filmSpeed = 3f;
    private float timeBeforePhrases = 1f;
    private float timeAfterPhrases = 3f;

    private float film1PosY;
    private float film2PosY;

    private float film1StopPosY = 10f;
    private float film2StopPosY = -10f;

    private List<string> conversation = new List<string>() {
        "You: Hello, Nikola Tesla.",
        "Nikola Tesla: Who are you? What are you doing in my laboratory?",
        "You: I was sent here from the future. We need your help.",
        "Nikola Tesla: Fascinating... How exactly I can help?",
        "You: No time to explain. We are not alone, it's too dangerous here.",
        "You: There are people who don't want our future to be saved.",
        "Nikola Tesla: Okay, just a minute. I need to feed my pigeon.",
        "You: OK, hurry. We still need to collect Hanry Ford and Albert Einstein."
    };

    private string toBeContinuedText = "To be continued... Press 'Esc' to exit";
    // Start is called before the first frame update
    void Start()
    {
        conversationTextComponent.gameObject.SetActive(false);
        toBeContinuedTextComponent.gameObject.SetActive(false);

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

            startMoveFilms = true;

            ActivateFilms();
        }

        if (characterPosX >= cutSceneCharacterStopPosX && characterPosY >= cutSceneTriggerYPos && !filmsFinishedMoving)
        {
            character.SetAutoMoveDirection(0);

            conversationTextComponent.gameObject.SetActive(true);

            startMoveFilms = false;
            filmsFinishedMoving = true;
        }

        if (conversationFinished)
        {
            startMoveFilms = true;

            if (film1.transform.localPosition.y <= film1StopPosY && film2.transform.localPosition.y >= film2StopPosY)
            {
                startMoveFilms = false;
                showToBeContinuedText = true;
            } 
        }
    }

    private void FixedUpdate()
    {
        if (startMoveFilms)
        {
            film1.transform.position = new Vector2(film1.transform.position.x, film1.transform.position.y - (filmSpeed * Time.fixedDeltaTime));
            film2.transform.position = new Vector2(film2.transform.position.x, film2.transform.position.y + (filmSpeed * Time.fixedDeltaTime));
        }

        if (filmsFinishedMoving && !conversationStarted)
        {
            conversationStarted = true;

            StartCoroutine(StartConversation());
        }

        if (showToBeContinuedText)
        {
            toBeContinuedTextComponent.gameObject.SetActive(true);
            toBeContinuedTextComponent.text = toBeContinuedText;
        }
    }

    private void ActivateFilms()
    {
        film1SpriteRenderer.sortingOrder = filmSortingOrder;
        film2SpriteRenderer.sortingOrder = filmSortingOrder;

        film1.gameObject.SetActive(true);
        film2.gameObject.SetActive(true);
    }

    private IEnumerator StartConversation()
    {
        foreach(string phrase in conversation)
        {
            yield return new WaitForSeconds(timeBeforePhrases);

            conversationTextComponent.text = phrase;

            yield return new WaitForSeconds(timeAfterPhrases);
        }

        conversationTextComponent.text = "";

        conversationFinished = true;
    }
}
