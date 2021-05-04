using UnityEngine;

public class MasterSlash : MonoBehaviour
{
    private float Speed = 2;
    private Player Player;

    private void Awake()
    {
        Player = GameManager.Instance.Player;
    }
    void Update()
    {
        if (Player.Animator.GetFloat("FacingX") == 1)
        {
            transform.position += transform.right * Speed * Time.deltaTime;
        }
        else if (Player.Animator.GetFloat("FacingX") == -1)
        {
            transform.position -= transform.right * Speed * Time.deltaTime;
        }
        else if (Player.Animator.GetFloat("FacingY") == 1)
        {
            transform.position += transform.up * Speed * Time.deltaTime;
        }
        else if (Player.Animator.GetFloat("FacingY") == -1)
        {
            transform.position -= transform.up * Speed * Time.deltaTime;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponentInParent<Health>();

        if (health)
        {
            health.Value -= 1;
        }
    }
}
