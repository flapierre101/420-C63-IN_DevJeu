using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var parent = collision.gameObject.GetComponentInParent<IDestructable>();

        if (parent != null)
        {
            Debug.Log("colision avec: " + parent);
            parent.Health.Value -= 1;
        }



    }
}
