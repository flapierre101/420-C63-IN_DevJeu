using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RawImage heart1;
    public RawImage heart2;
    public RawImage heart3;
    public RawImage mana0;
    public RawImage mana25;
    public RawImage mana50;
    public RawImage mana75;
    public RawImage mana100;
    public RawImage woodenSword;
    public RawImage masterSword;
    public RawImage currentWeapon;
    public RawImage currentMagic;
    public RawImage currentMana;
    public RawImage fireball;
    public RawImage frostbolt;
    public RawImage gameover;
    public RawImage password;




    private GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = GameManager.Instance;
        woodenSword.enabled = false;
        masterSword.enabled = false;
        currentMana = mana100;
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
        if (instance.SavegameManager.saveData.equipedWeapon == SaveData.EquipedWeapon.MasterSword)
        {
            currentWeapon = masterSword;
        }

        currentWeapon.enabled = true;
    }

    public void updateMagic()
    {
        if (currentMagic)
            currentMagic.enabled = false;

        Debug.Log(instance.SavegameManager.saveData.equipedWeapon);
        if (instance.SavegameManager.saveData.equipedMagic == SaveData.EquipedMagic.Fireball)
        {
            currentMagic = fireball;
        }
        if (instance.SavegameManager.saveData.equipedMagic == SaveData.EquipedMagic.Frostbolt)
        {
            currentMagic = frostbolt;
        }

        currentMagic.enabled = true;
    }

    public void updateHeart()
    {
        switch (instance.Player.Health.Value)
        {
            case 3:
                heart3.enabled = true;
                heart2.enabled = true;
                heart1.enabled = true;
                break;
            case 2:
                heart3.enabled = false;
                heart2.enabled = true;
                heart1.enabled = true;
                break;
            case 1:
                heart3.enabled = false;
                heart2.enabled = false;
                heart1.enabled = true;
                break;
            case 0:
                heart3.enabled = false;
                heart2.enabled = false;
                heart1.enabled = false;
                break;
            default:
                break;
        }

    }

    public void updateMana()
    {
        if (currentMana)
            currentMana.enabled = false;

        switch (instance.Player.Mana.Value)
        {
            case 4:
                currentMana = mana100;
                break;
            case 3:
                currentMana = mana75;
                break;
            case 2:
                currentMana = mana50;
                break;
            case 1:
                currentMana = mana25;
                break;
            case 0:
                currentMana = mana0;
                break;
            default:
                break;
        }
        Debug.Log("patate" + instance.Player.Mana.Value);
        currentMana.enabled = true;

    }
}
