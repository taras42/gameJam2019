using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public FoldsBehaviour folds;
    public GameObject background;
    public Text gameName;
    public float gameNameFadeTime = 5f;

    public ConversationBehaviour conversationComponent;

    public CharacterBehaviour character;

    private bool gameNameFading = false;

    private float film1PosYToStartMonologue = -2.5f;
    private float film2PosYToStartMonologue = -28.5f;

    private float film1PosYToStartPostMonologue = -2.5f;
    private float film2PosYToStartPostMonologue = -30.5f;

    private float film1PosYToHideBackground = -6.5f;
    private float film2PosYToHideBackground = -22f; 

    private float film1PosYToStartGame;
    private float film2PosYToStartGame;

    private bool moveFoldsUntilMonologueStarted = false;
    private bool startIntroMonologue = false;
    private bool startPostIntroMonologue = false;

    private bool introFinished = false;

    private List<string> introMonologue = new List<string>()
    {
        "**Recording device activated**",
        "You: We thought we could mindlessly use resources of our planet",
        "You: without any consequences...",
        "You: We were wrong.",
        "You: The year is 2107 and the whole planet is flooded. ",
        "You: The last of the floating cities will soon cease to exist.",
        "You: I need to fix this.",
        "You: The device I invented should send me back in time.",
        "You: I need to find the most significant scientists and entrepreneurs of the past",
        "You: to unite them and save the future.",
        "**Recording device deactivated**",
        "You: Phew, gotta keep recording those.",
        "You: Should be one hell of a story, when I'll come back.",
        "You: Someone must be already bored of that one lengthy monologue.",
        "You: I'll just activate the device.",
        "**Temporal field activated**",
        "**Year: 1902, Location: Nikola's Tesla Laboratory**"
    };

    private List<string> postIntroMonologue = new List<string>()
    {
        "You: Wow! It worked!",
        "You: Tesla must be somewhere here...",
        "You: I should be careful though, The Company may have sent their agents."
    };
    // Start is called before the first frame update

    void Start()
    {
        character.Freeze();

        Vector3 film1Position = folds.GetFilm1LocalPosition();
        Vector3 film2Position = folds.GetFilm2LocalPosition();

        film1PosYToStartGame = film1Position.y;
        film2PosYToStartGame = film2Position.y;

        conversationComponent.SetConversation(introMonologue);
        background.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!introFinished)
        {
            if (!gameNameFading)
            {
                gameNameFading = true;
                gameName.CrossFadeAlpha(0, gameNameFadeTime, false);
                StartCoroutine(ActivateFolds());
            }
        }
    }

    private void FixedUpdate()
    {
        if (!introFinished)
        {
            Vector3 film1Position = folds.GetFilm1LocalPosition();
            Vector3 film2Position = folds.GetFilm2LocalPosition();

            if (moveFoldsUntilMonologueStarted)
            {
                if (film1Position.y >= film1PosYToStartMonologue && film2Position.y <= film2PosYToStartMonologue)
                {
                    folds.MoveFilms();
                }
                else
                {
                    startIntroMonologue = true;
                    moveFoldsUntilMonologueStarted = false;
                }
            }


            if (startIntroMonologue)
            {
                if (!conversationComponent.IsConversationStarted())
                {
                    StartCoroutine(conversationComponent.StartConversation());
                }

                if (conversationComponent.IsConversationFinished())
                {
                    if (film1Position.y >= film1PosYToHideBackground && film2Position.y <= film2PosYToHideBackground)
                    {
                        folds.MoveFilms();
                    }
                    else
                    {
                        startIntroMonologue = false;
                        startPostIntroMonologue = true;

                        conversationComponent.SetConversation(postIntroMonologue);
                        background.SetActive(false);
                        folds.InvertDirection();
                    }
                }
            }

            if (startPostIntroMonologue)
            {

                if (film1Position.y <= film1PosYToStartPostMonologue && film2Position.y >= film2PosYToStartPostMonologue)
                {
                    folds.MoveFilms();
                }
                else
                {
                    if (!conversationComponent.IsConversationStarted())
                    {
                        StartCoroutine(conversationComponent.StartConversation());
                    }

                    if (conversationComponent.IsConversationFinished())
                    {
                        if (film1Position.y <= film1PosYToStartGame && film2Position.y >= film2PosYToStartGame)
                        {
                            folds.MoveFilms();
                        } else
                        {
                            folds.HideFilms();
                            character.UnFreeze();

                            startPostIntroMonologue = false;
                            introFinished = true;
                        }
                    }
                }
            }
        }
    }

    private IEnumerator ActivateFolds()
    {
        yield return new WaitForSeconds(gameNameFadeTime);

        folds.ShowFilms();

        moveFoldsUntilMonologueStarted = true;
    }
}
