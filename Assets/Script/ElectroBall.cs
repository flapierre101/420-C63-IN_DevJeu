using UnityEngine;

public class ElectroBall : MonoBehaviour
{
  public enum Animation
  {
    Cast,
    Impact,
  }

  private Animation _currentAnimation;

  public Animation CurrentAnimation
  {
    get { return _currentAnimation; }
    set
    {
      _currentAnimation = value;
      UpdateAnimations();
    }
  }
  public string AnimationName
  {
    get
    {
      var suffix = CurrentAnimation.ToString();
      return "ElectroBall_" + suffix;
    }
  }

  private void UpdateAnimations()
  {
    var animation = AnimationName;
    Animator.Play(animation);
  }
  public BoxCollider2D projectileCollider;
  public Animator Animator;
  public int Damage = 1;
  public float Speed = 1;

  private void Awake()
  {
    projectileCollider = GetComponent<BoxCollider2D>();
    Animator = GetComponent<Animator>();
    CurrentAnimation = Animation.Cast;

  }

  private void Update()
  {
    transform.position += Speed * Time.deltaTime * transform.right;
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    Health playerHealth = collision.GetComponent<Health>();

    if (collision.CompareTag("Player"))
    {
      projectileCollider.enabled = false;
      CurrentAnimation = Animation.Impact;
      Fade fade = gameObject.GetComponent<Fade>();
      fade.FadeWaitTime = 0.75f;
      fade.StartFade();
      playerHealth.Value -= Damage;
    }

  }
}
