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
  public float TeleportTimer = 5;
  private bool hasTeleported = true;
  private Vector3[] teleportPositions = new Vector3[] { new Vector3(0, 0, 0), new Vector3(1.5f, 0.52f, 0f), new Vector3(1.14f, -0.23f, 0f), new Vector3(-1.8f, 0.5f, 0f), new Vector3(-0.4f, -0.39f, 0f), new Vector3(1.72f, -0.38f, 0f) };
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
    if (hasTeleported && TeleportTimer > 0)
    {
      TeleportTimer -= Time.deltaTime;
    }
    else if (hasTeleported && TeleportTimer <= 0)
    {
      TeleportTimer = 5;
      hasTeleported = false;
      Teleport();
      SpecialAttack();
    }



  }

  public void Teleport()
  {

    transform.position = teleportPositions[Random.Range(0, teleportPositions.Length)];
    Debug.Log("I TELEPORTED: " + transform.position);
    hasTeleported = true;
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

  }
}
