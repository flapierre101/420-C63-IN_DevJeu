﻿using UnityEngine;

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
    private INPCBehaviour npc;
    private float npcTimer;


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
        gameObject.SetActive(false);
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

        if (Input.GetMouseButtonDown(0) && instance.SavegameManager.saveData.hasSword)
        {
            CurrentAnimation = Animation.Attack_BT;
            Animator.Update(0);

            if (Animator.GetFloat("FacingX") == 1)
            {
                MasterSlash = PrefabManager.Global.MasterSlashRight;
                NormalSlash = PrefabManager.Global.SlashRight;
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
            if (instance.SavegameManager.saveData.hasMasterSword)
                GameManager.Instance.PrefabManager.Instanciate(MasterSlash, SlashSpawn.position, transform.rotation);
            else
                GameManager.Instance.PrefabManager.Instanciate(NormalSlash, SlashSpawn.position, SlashSpawn.rotation);
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
            instance.SavegameManager.saveData.hasMasterSword = true;
            instance.SavegameManager.saveData.equipedWeapon = SaveData.EquipedWeapon.MasterSword;
            instance.UIManager.updateWeapon();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        INPCBehaviour Inpc = other.GetComponentInParent<INPCBehaviour>();
        if (Inpc != null)
        {
            if (npcTimer <= 0.0f)
            {
                instance.SoundManager.Play(SoundManager.Sfx.heyListen);
                npcTimer = 5.0f;

            }
            npc = Inpc;

        }



    }


    private void OnTriggerExit2D(Collider2D other)
    {
        Oldman oldman = other.GetComponentInParent<Oldman>();
        if (oldman != null)
        {
            npc = null;
        }
    }
}
