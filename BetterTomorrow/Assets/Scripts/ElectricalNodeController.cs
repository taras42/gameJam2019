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

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
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
        if (active && Input.GetKeyDown(KeyCode.E))
        {
            characterBehaviour.interactionWithElectrycityNode();

            spriteRenderer.sprite = openSprite;
            StartCoroutine(LightOff());
        }
    }

    private IEnumerator LightOff()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.sprite = offSpritesList[i];
            yield return new WaitForSeconds(timer / 3);
        }
        spriteRenderer.sprite = closeOffSprite;

        TriggerLights();

        enemy.TurnOnLightQuicly(); 
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
}
