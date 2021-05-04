using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float Speed = 2;
    private Player Player;
    private int facingAxisX, facingAxisY;

    private void Awake()
    {
        Player = GameManager.Instance.Player;
        if (Player.Animator.GetFloat("FacingX") == 1)
        {
            facingAxisX = 1;
        }
        else if (Player.Animator.GetFloat("FacingX") == -1)
        {
            facingAxisX = -1;
        }
        else if (Player.Animator.GetFloat("FacingY") == 1)
        {
            facingAxisY = 1;
        }
        else if (Player.Animator.GetFloat("FacingY") == -1)
        {
            facingAxisY = -1;
        }

    }
    void Update()
    {
        switch (facingAxisX)
        {
            case 1:
                transform.position += transform.right * Speed * Time.deltaTime;
                break;
            case -1:
                transform.position -= transform.right * Speed * Time.deltaTime;
                break;
            default:
                break;
        }

        switch (facingAxisY)
        {
            case 1:
                transform.position += transform.up * Speed * Time.deltaTime;
                break;
            case -1:
                transform.position -= transform.up * Speed * Time.deltaTime;
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponentInParent<Health>();

        if (health)
        {
            health.Value -= 2;
        }
        //anim
    }
}
