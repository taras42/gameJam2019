using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBehaviour : MonoBehaviour
{
    public CharacterBehaviour hiddableObject;

    Vector3 cratePos;
    Vector3 crateSize;
    Vector3 hiddableObjectSize;

    float crateHalfWidth;
    float hiddableObjectHalfWidth;
    float cratePosXLeftBoundary;
    float cratePosXRightBoundary;

    Collider2D hiddableObjectCollider;
    Collider2D crateCollider;

    bool collisionsDisabled = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == hiddableObject.name)
        {
            DisableCollisions();
        }
    }

    void Start()
    {
        cratePos = transform.position;
        crateSize = GetComponent<Collider2D>().bounds.size;
        hiddableObjectSize = hiddableObject.GetComponent<Collider2D>().bounds.size;

        crateHalfWidth = crateSize[0] / 2;
        hiddableObjectHalfWidth = hiddableObjectSize[0] / 2;

        cratePosXLeftBoundary = cratePos[0] - crateHalfWidth;
        cratePosXRightBoundary = cratePos[0] + crateHalfWidth;

        hiddableObjectCollider = hiddableObject.GetComponent<Collider2D>();
        crateCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (hiddableObject != null && collisionsDisabled)
        {
            Vector3 hiddableObjectPos = hiddableObject.transform.position;
            float hiddableObjectXPos = hiddableObjectPos[0];

            bool isInvisible = hiddableObjectXPos >= cratePosXLeftBoundary && hiddableObjectXPos <= cratePosXRightBoundary;

            if (isInvisible)
            {
                hiddableObject.SetVisibility(false);
            }  else
            {
                hiddableObject.SetVisibility(true);
            }

            if (hiddableObjectXPos - hiddableObjectHalfWidth > cratePosXRightBoundary || hiddableObjectXPos + hiddableObjectHalfWidth < cratePosXLeftBoundary)
            {
                EnableCollisions();
            }
        }
    }

    void FixedUpdate()
    {
        
    }

    private void EnableCollisions()
    {
        if (collisionsDisabled)
        {
            collisionsDisabled = false;
            Physics2D.IgnoreCollision(hiddableObjectCollider, crateCollider, false);
        }
    }

    private void DisableCollisions()
    {
        collisionsDisabled = true;
        Physics2D.IgnoreCollision(hiddableObjectCollider, crateCollider);
    }
}
