using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationBehaviour : MonoBehaviour
{
    public float timeBeforePhrases = 1f;
    public float timeAfterPhrases = 3f;

    private bool conversationStarted = false;
    private bool conversationFinished = false;

    private List<string> conversation;

    private Text textComponent;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<Text>();

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetConversation(List<string> newConversation)
    {
        conversationStarted = false;
        conversationFinished = false;

        conversation = newConversation;
    }

    public bool IsConversationStarted()
    {
        return conversationStarted;
    }

    public bool IsConversationFinished()
    {
        return conversationFinished;
    }

    public IEnumerator StartConversation()
    {
        gameObject.SetActive(true);

        conversationStarted = true;

        foreach (string phrase in conversation)
        {
            yield return new WaitForSeconds(timeBeforePhrases);

            textComponent.text = phrase;

            yield return new WaitForSeconds(timeAfterPhrases);
        }

        textComponent.text = "";

        conversationFinished = true;

        gameObject.SetActive(false);
    }
}
