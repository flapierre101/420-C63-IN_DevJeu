using UnityEngine;

public class Goblin : MonoBehaviour, IDestructable
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
  public bool PlayerInAttackRange;
  public FacingController FacingController;
  public Animator Animator;
  public Rigidbody2D Rigidbody2D;
  private Health playerHealth;
  private EnemyAI enemyAI;
  public Health Health { get; private set; }
  private Flash Flash;
  private float speed;
  private float defaultSpeed = 0.75f;
  private float rushSpeed = 1;
  private float attackTimer = 1.0f;
  private bool onCoolDown = false;
  private void Start()
  {
    playerHealth = FindObjectOfType<Player>().GetComponent<Health>();
    Health = GetComponent<Health>();
    Health.OnHit += OnHit;
    Health.OnDeath += OnDeath;
    Flash = GetComponent<Flash>();
    FacingController = GetComponent<FacingController>();
    Animator = GetComponent<Animator>();
    Rigidbody2D = GetComponent<Rigidbody2D>();
    enemyAI = GetComponentInChildren<EnemyAI>();
    FacingController.Facing = Facing.Left;
    CurrentAnimation = Animation.Walk;
    speed = defaultSpeed;
    UpdateAnimations();
  }

  private void OnDeath(Health health)
  {
    Destroy(gameObject);
    GameManager.Instance.PrefabManager.ItemDrop(gameObject);
  }

  private void OnHit(Health health)
  {
    Flash.StartFlash();
  }

  // Update is called once per frame
  void Update()
  {
    UpdateAnimations();
    float x = this.transform.position.x;
    float y = this.transform.position.y;

    if (enemyAI.playerDetected)
    {
      speed = rushSpeed;
    }
    else
    {
      speed = defaultSpeed;
    }


    if (CurrentAnimation == Animation.Walk)
    {
      if (FacingController.Facing == Facing.Left)
      {
        x -= speed * Time.deltaTime;
      }
      else if (FacingController.Facing == Facing.Right)
      {
        x += speed * Time.deltaTime;
      }
      else if (FacingController.Facing == Facing.Up)
      {
        y += speed * Time.deltaTime;
      }
      else if (FacingController.Facing == Facing.Down)
      {
        y -= speed * Time.deltaTime;
      }

      if (enemyAI.PlayerInAttackRange && !onCoolDown)
      {
        CurrentAnimation = Animation.Attack;
      }

      if (onCoolDown && attackTimer > 0)
      {
        attackTimer -= Time.deltaTime;
      }
      else if (onCoolDown && attackTimer <= 0)
      {
        onCoolDown = false;
        attackTimer = 1.0f;
      }

      Vector3 move = new Vector3(x, y, 0);

      this.transform.position = move;
    }
  }


  private void OnTriggerEnter2D(Collider2D collision)
  {
    var player = collision.GetComponentInParent<Player>();
    if (CurrentAnimation == Animation.Attack && !onCoolDown && player)
    {
      onCoolDown = true;
      playerHealth.Value -= 1;
      Debug.Log("PLAYER HEALTH: " + playerHealth.Value);
    }
    else
    {
      CurrentAnimation = Animation.Walk;
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    CurrentAnimation = Animation.Walk;
    onCoolDown = false;
    attackTimer = 1.0f;
  }

  public void OnSwordAttack()
  {
    CurrentAnimation = Animation.Walk;
  }
}
