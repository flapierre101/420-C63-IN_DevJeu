using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestructableSuperClass : MonoBehaviour, IDestructable
{
    public Health Health { get; private set; }
    private int itemSpawn;

    protected void Awake()
    {
        Health = GetComponent<Health>();

    }


    public void DropItem ()
    {
        itemSpawn = Random.Range(1, 101);

        switch (itemSpawn)
        {
            case int n when n >= 1 && n <= 25:
                GameManager.Instance.PrefabManager.Instanciate(PrefabManager.Global.Consumable_BluePotion, gameObject.transform.position, gameObject.transform.rotation);
                break;
            case int n when n >= 26 && n <= 50:
                GameManager.Instance.PrefabManager.Instanciate(PrefabManager.Global.Consumable_Heart, gameObject.transform.position, gameObject.transform.rotation);
                break;
            case int n when n >= 51 && n <= 75:
                GameManager.Instance.PrefabManager.Instanciate(PrefabManager.Global.Consumable_RedPotion, gameObject.transform.position, gameObject.transform.rotation);
                break;
            default:
                break;
        }
    }
}

