using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityGenericBehaviour : MonoBehaviour
{

    public CharacterBehaviour character;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
