using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBush : DestructableSuperClass
{

    void Awake()
    {
        base.Awake();
        Health.OnDeath += OnDeath;
    }

    private void OnDeath(Health health)
    {
        Destroy(gameObject);
        DropItem();
    }
}
