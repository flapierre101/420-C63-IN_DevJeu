using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWoodenCrate : MonoBehaviour, IDestructable
{
    public Health Health { get; private set; }
    public Animator Animator { get; private set; }

    void Awake()
    {
        Animator = GetComponent<Animator>();
        Animator.enabled = false;
        Health = GetComponent<Health>();
        Health.OnDeath += OnDeath;
    }

    private void OnDeath(Health health)
    {
        Animator.enabled = true;
        Animator.Play("Destroy_WoodenCrate");

    }
}
