using UnityEngine;

public class ConsumableRedPotion : MonoBehaviour, IConsumable
{
    private Player player;

    private void Awake()
    {
        player = GameManager.Instance.Player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            Destroy(gameObject);
            if (player.Health.Value < 2)
                player.Health.Value += 2;
            else if (player.Health.Value == 2)
                player.Health.Value++;

        }
    }

}
