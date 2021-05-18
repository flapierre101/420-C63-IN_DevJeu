using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.SoundManager.Play(SoundManager.Sfx.BOOM);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.gameObject.GetComponentInParent<IDestructable>();

        if (health != null)
        {
            health.Health.Value -= 2;
        }
    }
}
