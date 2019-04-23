using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneController : MonoBehaviour
{
    public CharacterBehaviour character;

    public ConversationBehaviour conversationTextComponent;
    public Text toBeContinuedTextComponent;

    public FoldsBehaviour folds;

    private float cutSceneTriggerXPos = 18;
    private float cutSceneTriggerYPos = 24;
    private float cutSceneCharacterStopPosX = 28;

    private bool startMoveFilms = false;
    private bool filmsFinishedMoving = false;
    private bool showToBeContinuedText = false;

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
        toBeContinuedTextComponent.gameObject.SetActive(false);
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

            folds.ShowFilms();
        }

        if (characterPosX >= cutSceneCharacterStopPosX && characterPosY >= cutSceneTriggerYPos && !filmsFinishedMoving)
        {
            character.SetAutoMoveDirection(0);

            conversationTextComponent.SetConversation(conversation);

            startMoveFilms = false;
            filmsFinishedMoving = true;
        }

        if (filmsFinishedMoving && conversationTextComponent.IsConversationFinished())
        {
            startMoveFilms = true;

            Vector3 film1Position = folds.GetFilm1LocalPosition();
            Vector3 film2Position = folds.GetFilm2LocalPosition();

            if (film1Position.y <= film1StopPosY && film2Position.y >= film2StopPosY)
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
            folds.MoveFilms();
        }

        if (filmsFinishedMoving && !conversationTextComponent.IsConversationStarted())
        {
            StartCoroutine(conversationTextComponent.StartConversation());
        }

        if (showToBeContinuedText)
        {
            toBeContinuedTextComponent.gameObject.SetActive(true);
            toBeContinuedTextComponent.text = toBeContinuedText;
        }
    }
}
