using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var objet = collision.gameObject.GetComponent<IDestructable>();
        var stop = 1;
        if (objet != null)
        {
            objet.Health.Value -= 1;
        }
     
        
        
    }
}
