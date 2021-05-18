using UnityEngine;

public class DestructableMetalCrate : MonoBehaviour, IDestructable
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
        Animator.Play("Destroy_MetalCrate");
        GameManager.Instance.PrefabManager.ItemDrop(gameObject);

    }

    private void Update()
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && Health.Value == 0)
            Destroy(gameObject);
    }
}
