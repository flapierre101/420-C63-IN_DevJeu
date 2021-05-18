using UnityEngine;

public class BombExplosion : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.gameObject.GetComponentInParent<IDestructable>();

        if (health != null)
        {
            health.Health.Value -= 2;
        }
    }
}
