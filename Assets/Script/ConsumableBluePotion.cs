using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableBluePotion : MonoBehaviour, IConsumable
{
    private Player player;

    private void Awake()
    {
        player = GameManager.Instance.Player;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            Destroy(gameObject);
            if (player.Mana.Value < 10)
                player.Mana.Value += 2;
        }
    }
}
