using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchController : MonoBehaviour
{
    public GameObject light;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Transform lightCone = light.transform.Find("lightCone");
            lightCone.gameObject.SetActive(!lightCone.gameObject.activeSelf);
        }
    }
}
