using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBarrel : MonoBehaviour
{
    //private Player player;
    public Health Health { get; private set; }
    public Animator Animator { get; private set; }

    void Awake()
    {
        //player = GameManager.Instance.Player;
        Animator = GetComponent<Animator>();
        Animator.enabled = false;
        Health = GetComponent<Health>();
        Health.OnDeath += OnDeath;
    }

    private void OnDeath(Health health)
    {
        Animator.enabled = true;
        Animator.Play("Destroy_Barrel");
        
    }



}
