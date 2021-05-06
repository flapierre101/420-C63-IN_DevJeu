using UnityEngine;

public class BombExplosion : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.gameObject.GetComponentInParent<Health>();

        if (health)
        {
            health.Value -= 2;
        }
    }
}
