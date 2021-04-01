using UnityEngine;

public class Bootstrap : MonoBehaviour

{
    private GameManager instance;

    private void Awake()
    {
        instance = GameManager.Instance;

    }
}
