using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalNodeController : MonoBehaviour
{

    public List<GameObject> ligts;
    public EnemyBehaviour enemy;
    public CharacterBehaviour characterBehaviour;

    public Sprite openSprite;
    public Sprite closeOnSprite;
    public Sprite closeOffSprite;

    public List<Sprite> offSpritesList;

    public int timer;

    private bool active;
    private SpriteRenderer spriteRenderer;

    public Transform firstLightSwitch;
    public Transform secondLightSwitch;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Character") {
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Character")
        {
            active = false;
        }
    }

    private void Update()
    {
        if (active && Input.GetKeyDown(KeyCode.E) && characterBehaviour != null)
        {
            characterBehaviour.interactionWithElectrycityNode();

            spriteRenderer.sprite = openSprite;
            StartCoroutine(LightOff());
        }
    }

    private IEnumerator LightOff()
    {
        yield return new WaitForSeconds(characterBehaviour.waitTime);

        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.sprite = offSpritesList[i];
            yield return new WaitForSeconds(timer / 3);
        }
        spriteRenderer.sprite = closeOffSprite;

        TriggerLights();

        TurnOnLightQuicly(); 
    }
   
    public void ResetNode()
    {
        spriteRenderer.sprite = closeOnSprite;
        TriggerLights();
    }

    public void TriggerLights()
    {
        foreach (GameObject light in ligts)
        {
            Transform lightCone = light.transform.Find("lightCone");
            lightCone.gameObject.SetActive(!lightCone.gameObject.activeSelf);
        }
    }

    private void TurnOnLightQuicly()
    {
        enemy.shootTargetWithinRange = 5f;
        enemy.maxSpeed = enemy.maxSpeed * 2;

        GoToTheNearestLightSwitcher();
    }

    private void GoToTheNearestLightSwitcher()
    {
        float firstX = firstLightSwitch.transform.position.x;
        float secondX = secondLightSwitch.transform.position.x;

        float enemyX = transform.position.x;

        if (Math.Abs(secondX - enemyX) > Math.Abs(firstX - enemyX))
        {
            enemy.GoTo(firstX);
        }
        else
        {
            enemy.GoTo(secondX);
        }
    }
}
