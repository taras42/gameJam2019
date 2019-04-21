using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecticalNodeTeslaCoilControllBehaviour : MonoBehaviour
{
    public CharacterBehaviour character;
    public DelayControlledTeslaCoilControllerByElectricalNodeBehaviour delayControlledCoil;
    public ToggleArcModeTeslaCoilBehaviour toggleArcModeCoil;
    public float coilActivationDelay = 1f;
    public float coilDeactivationDelay = 1f;

    public Sprite openSprite;

    public Sprite closeOffSprite;

    public List<Sprite> onSpritesList;
    public List<Sprite> offSpritesList;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool characterNearTheNode = false;
    private bool theNodeCanBeOperated = true;

    private bool activationDeactivationCycleStarted = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == character.name)
        {
            characterNearTheNode = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == character.name)
        {
            characterNearTheNode = false;
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        bool iterationKeyPressed = Input.GetAxisRaw("Iteract") > 0;
        
        if (character != null)
        {
            if (iterationKeyPressed && characterNearTheNode && theNodeCanBeOperated)
            {
                theNodeCanBeOperated = false;

                character.interactionWithElectrycityNode();

                spriteRenderer.sprite = openSprite;
            }

            if (!theNodeCanBeOperated && !character.IsFrozen() && !activationDeactivationCycleStarted)
            {
                activationDeactivationCycleStarted = true;

                spriteRenderer.sprite = closeOffSprite;

                StartCoroutine(ActivateCoil());
            }
        }
    }

    private IEnumerator ActivateCoil()
    {
        audioSource.Play();

        for (int i = 0; i < onSpritesList.Count; i++)
        {
            spriteRenderer.sprite = onSpritesList[i];
            yield return new WaitForSeconds(coilActivationDelay / onSpritesList.Count);
        }

        delayControlledCoil.Activate();
        toggleArcModeCoil.EnablePeriodicArcMode();

        StartCoroutine(DeactivateCoil());
    }

    private IEnumerator DeactivateCoil()
    {
        for (int i = 0; i < offSpritesList.Count; i++)
        {
            spriteRenderer.sprite = offSpritesList[i];
            yield return new WaitForSeconds(coilDeactivationDelay / offSpritesList.Count);
        }

        audioSource.Pause();

        delayControlledCoil.Deactivate();
        toggleArcModeCoil.EnableContiniousArcMode();

        theNodeCanBeOperated = true;
        activationDeactivationCycleStarted = false;
    }
}
