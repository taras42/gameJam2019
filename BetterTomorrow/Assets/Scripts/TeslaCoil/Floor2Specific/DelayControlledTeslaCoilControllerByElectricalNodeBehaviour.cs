using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayControlledTeslaCoilControllerByElectricalNodeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject leftElectricityArc;
    public GameObject rightElectricityArc;

    void Start()
    {
        leftElectricityArc.gameObject.SetActive(false);
        rightElectricityArc.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        leftElectricityArc.gameObject.SetActive(true);
        rightElectricityArc.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        leftElectricityArc.gameObject.SetActive(false);
        rightElectricityArc.gameObject.SetActive(false);
    }
}
