using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleArcModeTeslaCoilBehaviour : MonoBehaviour
{
    public List<GameObject> electricityArcs;
    public List<float> toggleElectricityArcWithDelays;
    public bool autoModePeriodic = true;

    private bool isContinious = false;
    private bool isPeriodic = false;
    private bool modeManualyToggled = false;

    private bool coroutineInProgress = false;

    // Start is called before the first frame update
    void Start()
    {

        if (!autoModePeriodic)
        {
            isContinious = true;
        }

        ActivateContiniousArcMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (!coroutineInProgress)
        {
            if (autoModePeriodic)
            {
                coroutineInProgress = true;
                StartCoroutine(ActivatePeriodicArcMode());
            }
            else
            {
                if (isContinious && modeManualyToggled)
                {
                    modeManualyToggled = false;
                    ActivateContiniousArcMode();
                }

                if (isPeriodic && modeManualyToggled)
                {
                    modeManualyToggled = false;
                    coroutineInProgress = true;
                    StartCoroutine(ActivatePeriodicArcMode());
                }
            }
        } 
    }

    private IEnumerator ActivatePeriodicArcMode()
    {
        for (int i = 0; i < toggleElectricityArcWithDelays.Count; i++)
        {
            for (int j = 0; j < electricityArcs.Count; j++)
            {
                electricityArcs[j].gameObject.SetActive(!electricityArcs[j].gameObject.activeSelf);
            }

            yield return new WaitForSeconds(toggleElectricityArcWithDelays[i]);
        }

        coroutineInProgress = false;
    }

    public void EnableContiniousArcMode()
    {
        isContinious = true;
        isPeriodic = false;
        modeManualyToggled = true;
    }

    public void EnablePeriodicArcMode()
    {
        isPeriodic = true;
        isContinious = false;
        modeManualyToggled = true;
    }

    private void ActivateContiniousArcMode()
    {
        for (int j = 0; j < electricityArcs.Count; j++)
        {
            electricityArcs[j].gameObject.SetActive(true);
        }
    }
}
