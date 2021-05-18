using UnityEngine;

public class Frostbolt : MonoBehaviour
{
    public enum Animation
    {
        Frostbolt,
        Frostbolt_Impact,
    }
    private float Speed = 2;
    private Player Player;
    public Animator Animator { get; set; }
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

    private void Awake()
    {
        Player = GameManager.Instance.Player;
        Animator = GetComponent<Animator>();
        CurrentAnimation = Animation.Frostbolt;

        if (Player.Animator.GetFloat("FacingX") == 1)
            gameObject.transform.eulerAngles = gameObject.transform.eulerAngles.WithZ(0);
        else if (Player.Animator.GetFloat("FacingX") == -1)
            gameObject.transform.eulerAngles = gameObject.transform.eulerAngles.WithZ(180);
        else if (Player.Animator.GetFloat("FacingY") == 1)
            gameObject.transform.eulerAngles = gameObject.transform.eulerAngles.WithZ(90);
        else if (Player.Animator.GetFloat("FacingY") == -1)
            gameObject.transform.eulerAngles = gameObject.transform.eulerAngles.WithZ(270);

    }
    void Update()
    {


        if (CurrentAnimation == Animation.Frostbolt_Impact && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Destroy(gameObject);
        }
        if (CurrentAnimation == Animation.Frostbolt)
        {
            transform.position += transform.right * Speed * Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var parent = collision.gameObject.GetComponentInParent<IDestructable>();

        Debug.Log("colision avec: " + parent);
        if (parent != null)
        {
            parent.Health.Value -= 2;
        }
        CurrentAnimation = Animation.Frostbolt_Impact;
        Animator.Update(0);
    }

}
