using UnityEngine;

public class EnemyAI : MonoBehaviour
{
  private Player player;
  private bool playerDetected;
  public float Speed = 0.2f;
  private Rigidbody2D rb;
  private void Awake()
  {
    player = GameManager.Instance.Player;
    rb = GetComponentInParent<Rigidbody2D>();


  }

  // Update is called once per frame
  void Update()
  {
    if (gameObject != null && playerDetected)
    {
      rb.MovePosition(Vector2.MoveTowards(rb.transform.position, GameManager.Instance.Player.transform.position, Speed));

      rb.transform.forward = GameManager.Instance.Player.transform.position - rb.transform.position;

    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      playerDetected = true;
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (playerDetected)
    {
      playerDetected = false;
    }
  }
}
