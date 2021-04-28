using System;

[Serializable]
public class SaveData
{
    public enum EquipedWeapon
    {
        Boomerang,
        Sword,
        MasterSword,

        None
    }


    public string characterName = "PasZelda";
    public bool hasSword = true;
    public bool hasMasterSword = true;
    public bool hasBoomerang = false;
    public bool hasSomeUpgrade = true;
    public EquipedWeapon equipedWeapon = EquipedWeapon.None;
    public int spokeoldman = 0;
}
