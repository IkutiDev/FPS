using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerHealth target;

    [SerializeField] private float damage = 40f;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void OnDamageTaken()
    {
        Debug.Log("I'm angry now!");
        damage = 60f;
    }
    public void AttackHitEvent()
    {
        if(target==null) return;
        target.TakeDamage(damage);
        Debug.Log("bang bang");
    }
}
