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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var parent = collision.gameObject.GetComponentInParent<IDestructable>();

        Debug.Log("colision avec: " + parent);
        if (parent != null)
        {
            parent.Health.Value -= 1;
        }
    }

}
