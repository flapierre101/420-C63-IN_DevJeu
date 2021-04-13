using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<DestructableBarrel>() != null)
        {
            collision.GetComponent<DestructableBarrel>().Health.Value -= 1;
        }
        
    }
}
