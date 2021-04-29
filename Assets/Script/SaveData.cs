using System;

[Serializable]
public class SaveData
{
    public enum EquipedWeapon
    {
        Boomerang,
        MasterSword,
        Sword,
        MasterSword,

        None
    }


    public string characterName = "PasZelda";
    public bool hasSword = false;
    public bool hasMasterSword = false;
    public bool hasBoomerang = false;
    public bool hasSomeUpgrade = true;
    public EquipedWeapon equipedWeapon = EquipedWeapon.None;
    public int spokeoldman = 0;
}
