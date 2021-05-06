using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Transform Transform;
    private Flash Flash;
    public float DestroyTimer;

    private void Awake()
    {
        Flash = GetComponent<Flash>();
        DestroyTimer = Flash.Duration;
        Invoke("BombDrop", DestroyTimer);
    }

    private void BombDrop()
    {
        Flash.StartFlash();
        Invoke("Explosion", DestroyTimer);
    }
    private void Explosion()
    {
        GameManager.Instance.PrefabManager.Instanciate(PrefabManager.Global.BombExplosion, Transform.position, transform.rotation);
        Destroy(gameObject);
        Flash.StopFlash();
    }
}
