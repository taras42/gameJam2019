using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityGenericBehaviour : MonoBehaviour
{
    public CharacterBehaviour character;

    private AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == character.name)
        {
            character.Die();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 characterPosition = character.transform.position;

        float dist = Vector2.Distance(transform.position, characterPosition);
        Debug.Log(dist);
        if (dist < 12)
        {
            audioSource.Play();
        }
        else 
        {
            audioSource.Pause();
        }
    }
}
