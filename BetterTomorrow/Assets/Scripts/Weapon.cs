using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Fire", true);
            animator.SetBool("DrawWeapon", true);
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
