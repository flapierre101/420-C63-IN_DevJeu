using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public LevelManager.Level Level;
    public string LevelEntranceId;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Player = collision.GetComponent<Player>();
        if (Player)
        {
            GameManager.Instance.LevelManager.GoToLevel(Level, LevelEntranceId);

        }
    }
}
