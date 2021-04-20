using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RawImage heart1;
    public RawImage heart2;
    public RawImage heart3;

    private int currentHealth;
    private GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 3;
        instance = GameManager.Instance;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loseHeart()
    {
        switch (instance.Player.Health.Value)
        {
            case 2:
                heart3.enabled = false;
                break;
            case 1:
                heart2.enabled = false;
                break;
            case 0:
                heart1.enabled = false;
                break;
            default:
                break;
        }
        if (currentHealth > 0)
        {
            currentHealth -= 1;
        }
    }

    public void gainHeart()
    {

        switch (currentHealth)
        {
            case 2:
                heart3.enabled = true;
                break;
            case 1:
                heart2.enabled = true;
                break;
            case 0:
                heart1.enabled = true;
                break;
            default:
                break;
        }
        if (currentHealth < 3)
        {
            currentHealth += 1;
        }
    }
}
