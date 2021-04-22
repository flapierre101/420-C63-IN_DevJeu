using UnityEngine;

public class Goblin : MonoBehaviour
{
  public enum Animation
  {
    Attack,
    Walk,
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
  public string StateName
  {
    get
    {
      var prefix = CurrentAnimation.ToString();
      return prefix;
    }
  }

  private void UpdateAnimations()
  {
    var animation = StateName;
    Animator.Play(animation);
  }
  public bool PlayerInRange;
  public FacingController FacingController;
  public Animator Animator;
  private Health playerHealth;
  private void Awake()
  {
    PlayerInRange = false;
  }

  // Update is called once per frame
  void Update()
  {
    FacingController = GetComponent<FacingController>();
    Animator = GetComponent<Animator>();
    CurrentAnimation = Animation.Attack;
    FacingController.Facing = Facing.Left;
    UpdateAnimations();
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    Debug.Log("INSIDE ON TRIGGER");

    if (collision.CompareTag("Player"))
    {
      Debug.Log("INSIDE ON TRIGGER TAG PLAYER");
      playerHealth = collision.GetComponentInParent<Health>();
      PlayerInRange = true;
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (PlayerInRange)
    {
      PlayerInRange = false;
    }
  }

  public void OnSwordAttack()
  {
    if (PlayerInRange)
    {
      playerHealth.Value -= 1;
      Debug.Log(playerHealth.Value);
    }
  }
}
