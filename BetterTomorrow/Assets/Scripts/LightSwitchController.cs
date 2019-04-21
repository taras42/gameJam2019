using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchController : MonoBehaviour
{
    public ElectricalNodeController electricalNodeController;

    private bool shouldResetElectricNode = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Enemy")
        {
            shouldResetElectricNode = true;
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (shouldResetElectricNode)
        {
            shouldResetElectricNode = false;
            electricalNodeController.ResetNode();
        }
    }
}
