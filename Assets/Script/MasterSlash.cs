using UnityEngine;

public class MasterSlash : MonoBehaviour
{
    private float Speed = 2;
    private float DestroyTimer = 0.5f;
    void Update()
    {
        transform.position += transform.right * Speed * Time.deltaTime;

        DestroyTimer -= Time.deltaTime;
        if (DestroyTimer <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponentInParent<Health>();

        if (health)
        {
            health.Value -= 1;
        }
    }
}
