using UnityEngine;

public class Goblin : EnemyAI
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
    FacingController = GetComponent<FacingController>();
    Animator = GetComponent<Animator>();
    FacingController.Facing = Facing.Left;
    CurrentAnimation = Animation.Attack;
    UpdateAnimations();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnTriggerStay2D(Collider2D collision)
  {

    if (collision.CompareTag("Player"))
    {
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

    CurrentAnimation = Animation.Walk;
  }
}
