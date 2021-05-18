using UnityEngine;

public class Sorcerer : MonoBehaviour
{

  public enum Animation
  {
    Cast,
    Idle,
    Sorcerer_Death_Down,
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
  public string AnimationName
  {
    get
    {
      var action = CurrentAnimation.ToString();

      return action;
    }
  }

  private void UpdateAnimations()
  {
    var animation = AnimationName;
    Animator.Play(animation);
  }
  public BoxCollider2D SorcererCollider;
  public Animator Animator;
  public Health SorcererHealth;
  public FacingController FacingController;
  public Transform SpawnPoint;
  public BoxCollider2D playerPosition;
  public BoxCollider2D AreaOfEffect;
  public float Speed = 1;
  public float AttackTimer = 1.9f;
  private bool hasAttacked = true;
  private float xRange = 1.5f;

  private void Start()
  {
    SorcererCollider = gameObject.GetComponent<BoxCollider2D>();
    Animator = gameObject.GetComponent<Animator>();
    SorcererHealth = gameObject.GetComponent<Health>();
    FacingController = gameObject.GetComponent<FacingController>();
    playerPosition = FindObjectOfType<Player>().GetComponent<BoxCollider2D>();
    AreaOfEffect = GetComponent<BoxCollider2D>();
    FacingController.Facing = Facing.Down;
    CurrentAnimation = Animation.Idle;
  }

  void Update()
  {
    if (hasAttacked && AttackTimer > 0)
    {
      AttackTimer -= Time.deltaTime;
    }
    else if (hasAttacked && AttackTimer <= 0)
    {
      AttackTimer = 2;
      hasAttacked = false;
      Teleport();
    }



  }

  public void ElectroBallAttack()
  {
    hasAttacked = true;
    GameManager.Instance.PrefabManager.Instanciate(PrefabManager.Global.ElectroBall, transform.position, transform.rotation);
  }

  public void Teleport()
  {
    var halfX = SorcererCollider.bounds.extents.x;
    //var halfY = SorcererCollider.bounds.extents.y;
    var x = Random.Range((AreaOfEffect.bounds.min.x), (AreaOfEffect.bounds.max.x));
    //var y = Random.Range((AreaOfEffect.bounds.min.y), (AreaOfEffect.bounds.max.y));

    Vector3 teleportToXY = transform.position;
    teleportToXY.x = x;
    //teleportToXY.y = y;

    transform.position = teleportToXY;
    ElectroBallAttack();
    SpecialAttack();
  }

  public void LookAtPlayer()
  {
    Vector3 playerTransform = GameManager.Instance.Player.GetComponent<Transform>().position;

    float xDelta = Mathf.Abs(transform.position.x - playerTransform.x);
    //float yDelta = Mathf.Abs(transform.position.y - playerTransform.y);

    if (xDelta <= xRange)
    {
      GameManager.Instance.PrefabManager.Instanciate(PrefabManager.Global.ElectroBall, SpawnPoint.position, transform.rotation);
    }
    else
    {
      SpecialAttack();
    }
  }

  public void SpecialAttack()
  {
    var z = 0;
    var rotation = transform.rotation * Quaternion.Euler(0, 0, z);

    for (int i = 0; i < 8; ++i)
    {
      GameManager.Instance.PrefabManager.Instanciate(PrefabManager.Global.ElectroBall, transform.position, rotation);
      z += 90;
      rotation = transform.rotation * Quaternion.Euler(0, 0, z);
    }

    hasAttacked = true;

  }
}
