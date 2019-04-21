using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalNodeController : MonoBehaviour
{

    public List<GameObject> lights;
    public EnemyBehaviour enemy;
    public CharacterBehaviour character;

    public Sprite openSprite;
    public Sprite closeOnSprite;
    public Sprite closeOffSprite;

    public float nearestSwitchAnchorPoint = 0f;

    public List<Sprite> offSpritesList;

    public int timer;

    private bool theNodeCanBeOperated = true;
    private bool lightOffCountDownStarted = false;

    private bool characterNearTheNode;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    public Transform firstLightSwitch;
    public Transform secondLightSwitch;

    private float initialEnemySpeed;
    private float initialEnemyShootTargetWithinRange;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        initialEnemySpeed = enemy.maxSpeed;
        initialEnemyShootTargetWithinRange = enemy.shootTargetWithinRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == character.name) {
            characterNearTheNode = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == character.name)
        {
            characterNearTheNode = false;
        }
    }

    private void Update()
    {
        if (character != null)
        {
            bool iterationKeyPressed = Input.GetAxisRaw("Iteract") > 0;

            if (characterNearTheNode && iterationKeyPressed && theNodeCanBeOperated)
            {
                theNodeCanBeOperated = false;

                character.interactionWithElectrycityNode();

                spriteRenderer.sprite = openSprite;
            }

            if (!theNodeCanBeOperated && !character.IsFrozen() && !lightOffCountDownStarted)
            {
                lightOffCountDownStarted = true;

                spriteRenderer.sprite = closeOffSprite;

                StartCoroutine(LightOff());
            }
        }
    }

    private IEnumerator LightOff()
    {
        audioSource.Play();
        for (int i = 0; i < offSpritesList.Count; i++)
        {
            spriteRenderer.sprite = offSpritesList[i];
            yield return new WaitForSeconds(timer / offSpritesList.Count);
        }
        audioSource.Pause();
        TriggerLights();
        MakeEnemyTurnTheLightsOn(); 
    }
   
    public void ResetNode()
    {
        theNodeCanBeOperated = true;
        lightOffCountDownStarted = false;

        enemy.shootTargetWithinRange = initialEnemyShootTargetWithinRange;
        enemy.maxSpeed = initialEnemySpeed;

        spriteRenderer.sprite = closeOnSprite;

        TriggerLights();
    }

    public void TriggerLights()
    {
        foreach (GameObject light in lights)
        {
            Transform lightCone = light.transform.Find("lightCone");
            lightCone.gameObject.SetActive(!lightCone.gameObject.activeSelf);
        }
    }

    private void MakeEnemyTurnTheLightsOn()
    {
        enemy.shootTargetWithinRange = 5f;
        enemy.maxSpeed = enemy.maxSpeed * 2;

        GoToTheNearestLightSwitcher();
    }

    private void GoToTheNearestLightSwitcher()
    {
        float firstX = firstLightSwitch.transform.position.x;
        float secondX = secondLightSwitch.transform.position.x;

        float enemyX = enemy.transform.position.x;

        if (enemyX <= nearestSwitchAnchorPoint)
        {
            enemy.GoTo(firstX);
        } else
        {
            enemy.GoTo(secondX);
        }
    }
}
