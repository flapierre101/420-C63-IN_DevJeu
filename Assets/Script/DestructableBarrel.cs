
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBarrel : MonoBehaviour, IDestructable
{
    //private Player player;
    public Health Health { get; private set; }
    public Animator Animator { get; private set; }
    private int itemSpawn; 

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
        Animator.Play("Destroy_Barrel");
    }

    private void Update()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Health.Value == 0)
        {
            Destroy(gameObject);
            itemSpawn = Random.Range(1, 3);

            switch (itemSpawn)
            {
                case 1: GameManager.Instance.PrefabManager.Instanciate(PrefabManager.Global.Consumable_BluePotion, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case 2: GameManager.Instance.PrefabManager.Instanciate(PrefabManager.Global.Consumable_Heart, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                case 3: GameManager.Instance.PrefabManager.Instanciate(PrefabManager.Global.Consumable_RedPotion, gameObject.transform.position, gameObject.transform.rotation);
                    break;
                default:
                    break;
            }
        }
    }

}
