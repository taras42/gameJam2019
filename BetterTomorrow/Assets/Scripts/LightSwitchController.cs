using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchController : MonoBehaviour
{
    public ElectricalNodeController electricalNodeController;

    private bool shouldResetElectricNode = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Enemy")
        {
            shouldResetElectricNode = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

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
