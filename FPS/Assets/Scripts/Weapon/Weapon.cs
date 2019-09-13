using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera FPCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private Ammo ammoSlot;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private float shootingDelay=1f;
    [SerializeField] private bool isAutomatic;
    private bool canShoot=true;


    private void OnEnable()
    {
        canShoot = true;
    }

    private void Update()
    {
        if (isAutomatic)
        {
            if (Input.GetButton("Fire1") && canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
        else if (Input.GetButtonDown("Fire1") && canShoot)
        {
            StartCoroutine(Shoot());
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (GetComponent<WeaponZoom>())
            {
                GetComponent<WeaponZoom>().Zoom();
            }
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            ammoSlot.ReduceCurrentAmmo(ammoType);
            PlayMuzzleFlash();
            ProcessRaycast();
        }
        yield return new WaitForSeconds(shootingDelay);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        var instantiatedHitEffect= Instantiate(hitEffect,hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(instantiatedHitEffect,1f);
    }
}
