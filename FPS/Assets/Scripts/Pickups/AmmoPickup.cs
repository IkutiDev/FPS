using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 5;
    [SerializeField] private AmmoType ammoType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Ammo>().IncreaseCurrentAmmo(ammoType,ammoAmount);
            Destroy(gameObject);
        }
    }
}
