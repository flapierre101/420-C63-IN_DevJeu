using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWoodenCrate : DestructableSuperClass
{
    public Animator Animator { get; private set; }

    void Awake()
    {
        base.Awake();
        Animator = GetComponent<Animator>();
        Animator.enabled = false;
        Health.OnDeath += OnDeath;
    }

    private void OnDeath(Health health)
    {
        Animator.enabled = true;
        Animator.Play("Destroy_WoodenCrate");

    }

    private void Update()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Health.Value == 0)
        {
            Destroy(gameObject);
            DropItem();
        }

    }
}
