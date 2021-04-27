using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public int Max = 10;


    // delegate signature de fonction
    public delegate void ManaEvent(Mana mana);

    // listeners
    public ManaEvent OnChanged;
    public ManaEvent OnUse;
    private int _value;

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
                
                OnUse?.Invoke(this);
            }
        }
    }
}
