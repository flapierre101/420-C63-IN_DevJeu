using UnityEngine;

public class Health : MonoBehaviour
{
    public int Max = 5;


    // delegate signature de fonction
    public delegate void HealthEvent(Health health);

    // listeners
    public HealthEvent OnChanged;
    public HealthEvent OnHit;
    public HealthEvent OnDeath;
    public HealthEvent OnHeal;
    private int _value;

    public float InvincibilityTime = 0.1f;
    public float InvincibilityTimer { get; private set; }

    private void Awake()
    {
        _value = Max;
    }

    public int Value
    {
        get { return _value; }
        set
        {

            var previous = _value;

            _value = Mathf.Clamp(value, 0, Max);

            if (_value != previous)
                OnChanged?.Invoke(this);

            if (_value < previous)
            {
                InvincibilityTimer = InvincibilityTime;
                OnHit?.Invoke(this);
            }

            if (_value <= 0)
                OnDeath?.Invoke(this);

        }
    }

    private void Update()
    {
        InvincibilityTimer -= Time.deltaTime;
    }

    public bool CanBeDamaged
    {
        get
        {
            return InvincibilityTimer <= 0.0f;
        }
    }






}
