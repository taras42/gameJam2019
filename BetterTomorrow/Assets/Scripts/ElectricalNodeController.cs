using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalNodeController : MonoBehaviour
{

    public List<GameObject> ligts;
    public EnemyBehaviour enemy;

    public Sprite openSprite;
    public Sprite closeClose;

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
            spriteRenderer.sprite = openSprite;
            StartCoroutine(LightOff());
        }
    }

    private IEnumerator LightOff()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(timer / 3);
            spriteRenderer.sprite = offSpritesList[i];
        }

        foreach (GameObject light in ligts)
        {
            Transform lightCone = light.transform.Find("lightCone");
            lightCone.gameObject.SetActive(!lightCone.gameObject.activeSelf);
        }
        enemy.TurnOnLightQuicly();   
    }
}
