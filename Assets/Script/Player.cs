using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Animation
    {
        Idle,
        Walk,
        Attack_BT,
    }

    public Health Health { get; private set; }
    public Mana Mana { get; private set; }
    private FacingController FacingController;
    private Animator Animator;
    private Animation _currentAnimation;
    private Flash Flash;
    private GameManager instance;

    private INPCBehaviour npc;


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
            return suffix;
        }
    }
    private void UpdateAnimations()
    {
        var animation = AnimationName;
        Animator.Play(animation);
    }

    void Awake()
    {
        instance = GameManager.Instance;
        Health = GetComponent<Health>();
        Health.OnHit += OnHit;
        Health.OnDeath += OnDeath;
        Mana = GetComponent<Mana>();
        Mana.OnUse += OnUse;
        FacingController = GetComponent<FacingController>();
        Animator = GetComponent<Animator>();
        Flash = GetComponent<Flash>();
    }

    private void OnUse(Mana mana)
    {
        mana.Value--;
    }

    private void OnDeath(Health health)
    {
        gameObject.SetActive(false);
    }

    private void OnHit(Health health)
    {
        Flash.StartFlash();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && instance.SavegameManager.saveData.hasSword)
        {
            CurrentAnimation = Animation.Attack_BT;

        }
        // si animation terminee reset currentanimation
        if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            CurrentAnimation = Animation.Idle;

        if (CurrentAnimation != Animation.Attack_BT)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            if (horizontal != 0.0f || vertical != 0.0f)
            {
                Animator.SetFloat("FacingX", horizontal);
                Animator.SetFloat("FacingY", vertical);
                CurrentAnimation = Animation.Walk;
            }
            else
            {
                CurrentAnimation = Animation.Idle;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (npc != null)
            {
                npc.UpdateBehaviour();

            }

        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        INPCBehaviour Inpc = other.GetComponentInParent<INPCBehaviour>();
        if (Inpc != null)
        {
            instance.SoundManager.Play(SoundManager.Sfx.hey_listen);
            npc = Inpc;

        }



    }


    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("patttattteee1111");




        Oldman oldman = other.GetComponentInParent<Oldman>();
        if (oldman != null)
        {
            npc = null;
        }
    }





}
