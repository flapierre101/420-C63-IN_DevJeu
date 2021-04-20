using System;

[Serializable]
public class SaveData
{
    public enum EquipedWeapon
    {
        Boomerang,
        Sword,

        None
    }


    public string characterName = "PasZelda";
    public bool hasSword = false;
    public bool hasBoomerang = false;
    public bool hasSomeUpgrade = true;
    public EquipedWeapon equipedWeapon = EquipedWeapon.None;
    public int spokeoldman = 0;



}
