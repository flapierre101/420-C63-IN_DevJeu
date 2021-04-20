using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RawImage heart1;
    public RawImage heart2;
    public RawImage heart3;
    public RawImage woodenSword;
    public RawImage currentWeapon;



    private GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = GameManager.Instance;
        woodenSword.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateWeapon()
    {
        if (currentWeapon)
            currentWeapon.enabled = false;

        Debug.Log(instance.SavegameManager.saveData.equipedWeapon);
        if (instance.SavegameManager.saveData.equipedWeapon == SaveData.EquipedWeapon.Sword)
        {
            currentWeapon = woodenSword;
        }

        currentWeapon.enabled = true;
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

    }

    public void gainHeart()
    {

        switch (instance.Player.Health.Value)
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

    }
}
