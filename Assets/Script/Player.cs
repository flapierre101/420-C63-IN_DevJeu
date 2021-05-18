using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Transform SlashSpawn;
    public PrefabManager.Global MasterSlash, NormalSlash;
    public Animator Animator { get; set; }
    private Animation _currentAnimation;
    private Flash Flash;
    private GameManager instance;
    private IInterractable npc;
    private float npcTimer, attackTime;

    // Début des fonctions

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
        Mana = GetComponent<Mana>();
        Health.OnHit += OnHit;
        Health.OnDeath += OnDeath;
        Health.OnChanged += OnChanged;
        Mana.OnChanged += OnChangedMana;
        Animator = GetComponent<Animator>();
        Flash = GetComponent<Flash>();
        npcTimer = 0.0f;
    }


    private void OnDeath(Health health)
    {
        GameManager.Instance.SoundManager.Play(SoundManager.Music.Gameover);
        GameManager.Instance.SoundManager.Play(SoundManager.Sfx.laugh);
        instance.UIManager.gameover.enabled = true;
        StartCoroutine(LoadLevelAfterDelay(3));
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.SavegameManager.onDeath();
        GameManager.Instance.SoundManager.Stop(SoundManager.Music.Gameover);
        instance.UIManager.gameover.enabled = false;
        instance.UIManager.password.enabled = true;
        yield return new WaitForSeconds(delay);
        GameManager.Instance.destroyInstance();
        SceneManager.LoadScene("MainMenu");
    }


    private void OnHit(Health health)
    {
        Flash.StartFlash();

    }

    private void OnChanged(Health health)
    {
        instance.UIManager.updateHeart();
    }

    private void OnChangedMana(Mana mana)
    {
        instance.UIManager.updateMana();
    }

    private void OnUse(Mana mana)
    {
        mana.Value--;
    }

    void Update()
    {
        if (npcTimer > 0.0f)
        {
            npcTimer = npcTimer - Time.deltaTime;
        }
        if (attackTime > 0)
        {
            attackTime -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && instance.SavegameManager.saveData.hasSword && attackTime <= 0.0f)
        {
            GameManager.Instance.SoundManager.AttackSound();
            CurrentAnimation = Animation.Attack_BT;
            Animator.Update(0);

            if (Animator.GetFloat("FacingX") == 1)
            {
                NormalSlash = PrefabManager.Global.SlashRight;

                MasterSlash = PrefabManager.Global.MasterSlashRight;
            }
            else if (Animator.GetFloat("FacingX") == -1)
            {
                MasterSlash = PrefabManager.Global.MasterSlashLeft;
                NormalSlash = PrefabManager.Global.SlashLeft;
            }

            else if (Animator.GetFloat("FacingY") == -1)
            {
                MasterSlash = PrefabManager.Global.MasterSlashDown;
                NormalSlash = PrefabManager.Global.SlashDown;
            }
            else if (Animator.GetFloat("FacingY") == 1)
            {
                MasterSlash = PrefabManager.Global.MasterSlashUp;
                NormalSlash = PrefabManager.Global.SlashUp;
            }
            if (instance.SavegameManager.saveData.equipedWeapon == SaveData.EquipedWeapon.MasterSword)
                GameManager.Instance.PrefabManager.Instanciate(MasterSlash, SlashSpawn.position, transform.rotation);
            else
                GameManager.Instance.PrefabManager.Instanciate(NormalSlash, SlashSpawn.position, SlashSpawn.rotation);

            attackTime = 0.5f;
        }

        if (Input.GetMouseButtonDown(1) && Mana.Value > 0)
        {
            if (instance.SavegameManager.saveData.equipedMagic == SaveData.EquipedMagic.Fireball)
            {
                GameManager.Instance.PrefabManager.Instanciate(PrefabManager.Global.Fireball, SlashSpawn.position, transform.rotation);
                Mana.Value -= 1;
            }
            else if (instance.SavegameManager.saveData.equipedMagic == SaveData.EquipedMagic.Frostbolt)
            {
                GameManager.Instance.PrefabManager.Instanciate(PrefabManager.Global.Frostbolt, SlashSpawn.position, transform.rotation);
                Mana.Value -= 1;
            }
            else if (instance.SavegameManager.saveData.equipedMagic == SaveData.EquipedMagic.Bomb)
            {
                GameManager.Instance.PrefabManager.Instanciate(PrefabManager.Global.Bomb, SlashSpawn.position, transform.rotation);
                Mana.Value -= 1;
            }

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
                npc.Interact();

            }

        }
        // Pour debug le heart UI
        if (Input.GetKeyUp(KeyCode.H))
        {
            Health.Value -= 1;
        }
        // Pour debug le heart UI
        if (Input.GetKeyUp(KeyCode.J))
        {
            Health.Value += 1;
        }
        // Pour debug le mana UI
        if (Input.GetKeyUp(KeyCode.N))
        {
            Mana.Value -= 1;
        }
        // Pour debug le mana UI
        if (Input.GetKeyUp(KeyCode.M))
        {
            Mana.Value += 1;
        }
        // Debug : Ajout de l'épée
        if (Input.GetKeyUp(KeyCode.K))
        {
            instance.SavegameManager.saveData.hasSword = true;
            instance.SavegameManager.saveData.equipedWeapon = SaveData.EquipedWeapon.Sword;
            instance.UIManager.updateWeapon();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            instance.SavegameManager.saveData.hasSword = true;
            instance.SavegameManager.saveData.hasMasterSword = true;
            instance.SavegameManager.saveData.equipedWeapon = SaveData.EquipedWeapon.MasterSword;
            instance.UIManager.updateWeapon();
        }
        // Debug : Ajout de l'épée
        if (Input.GetKeyUp(KeyCode.O))
        {
            instance.SavegameManager.saveData.hasFireball = true;
            instance.SavegameManager.saveData.equipedMagic = SaveData.EquipedMagic.Fireball;
            instance.UIManager.updateMagic();
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            instance.SavegameManager.saveData.hasFrostbolt = true;
            instance.SavegameManager.saveData.equipedMagic = SaveData.EquipedMagic.Frostbolt;
            instance.UIManager.updateMagic();
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            instance.SavegameManager.saveData.hasBomb = true;
            instance.SavegameManager.saveData.equipedMagic = SaveData.EquipedMagic.Bomb;
            instance.UIManager.updateMagic();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        IInterractable Inpc = other.GetComponentInParent<IInterractable>();
        if (Inpc != null)
        {
            if (npcTimer <= 0.0f)
            {
                instance.SoundManager.Play(SoundManager.Sfx.heyListen);
                npcTimer = 5.0f;

            }
            npc = Inpc;
            instance.UIManager.interactPrompt.enabled = true;

        }



    }


    private void OnTriggerExit2D(Collider2D other)
    {
        IInterractable Inpc = other.GetComponentInParent<IInterractable>();
        if (Inpc != null)
        {
            npc = null;
            instance.UIManager.interactPrompt.enabled = false;
        }
    }

    public void OnLevelStart(LevelEntrance levelEntrance)
    {
        if (levelEntrance != null)
        {
            transform.position = levelEntrance.transform.position;
        }
        else
        {
            transform.position = Vector3.zero;
        }

        //PlatformController.Reset();
    }
    public void OnLevelRestart()
    {
        GameManager.Instance.Camera.GetComponent<FollowObject>().TargetTransform = gameObject.transform;
    }

    private void Start()
    {
        OnLevelRestart();
    }
}
