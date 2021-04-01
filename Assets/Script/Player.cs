using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Health Health { get; private set; }
    public FacingController FacingController;

    // Start is called before the first frame update
    void Awake()
    {
        Health = GetComponent<Health>();
        Health.OnHit += OnHit;
        Health.OnDeath += OnDeath;
        FacingController = GetComponent<FacingController>();
    }

    private void OnDeath(Health health)
    {
        gameObject.SetActive(false);
    }

    private void OnHit(Health health)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        if  (horizontal < 0.0f)
        {
            FacingController.Facing = Facing.Left;
        }
        else
        {
            FacingController.Facing = Facing.Right;
        }
    }
}
