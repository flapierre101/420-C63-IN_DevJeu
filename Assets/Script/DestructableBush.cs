﻿using UnityEngine;

public class DestructableBush : MonoBehaviour, IDestructable
{
    public Health Health { get; private set; }

    void Awake()
    {
        Health = GetComponent<Health>();
        Health.OnDeath += OnDeath;
    }

    private void OnDeath(Health health)
    {
        Destroy(gameObject);
        GameManager.Instance.PrefabManager.ItemDrop(gameObject);
    }
}
