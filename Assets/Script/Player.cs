using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Animation
    {
        Idle_Down,
        Walk,
        Attack,
    }

    public Health Health { get; private set; }
    public FacingController FacingController;
    public Animator Animator;
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
        Health = GetComponent<Health>();
        Health.OnHit += OnHit;
        Health.OnDeath += OnDeath;
        FacingController = GetComponent<FacingController>();
        Animator = GetComponent<Animator>();
    }

    private void OnDeath(Health health)
    {
        gameObject.SetActive(false);
    }

    private void OnHit(Health health)
    {
        throw new NotImplementedException();
    }

    void Update()
    {

        if (gameObject.GetComponent<Rigidbody2D>().velocity.x != 0.0f || gameObject.GetComponent<Rigidbody2D>().velocity.y != 0.0f)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Animator.SetFloat("FacingX", horizontal);
            Animator.SetFloat("FacingY", vertical);
            CurrentAnimation = Animation.Walk;

        }
        else
        {
            CurrentAnimation = Animation.Idle_Down;
        }
    }
}
