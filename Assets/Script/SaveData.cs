using System;

[Serializable]
public class SaveData
{
    public enum EquipedWeapon
    {
        Boomerang,
        MasterSword,
        Sword,

        None
    }

    public enum EquipedMagic
    {
        Bomb,
        Fireball,
        Frostbolt,

        None
    }


    public string characterName = "PasZelda";
    public bool hasSword = false;
    public bool hasMasterSword = false;
    public bool hasBoomerang = false;
    public bool hasFireball = false;
    public bool hasFrostbolt = false;
    public bool hasBomb = false;
    public EquipedWeapon equipedWeapon = EquipedWeapon.None;
    public EquipedMagic equipedMagic = EquipedMagic.None;
    public int spokeoldman = 0;
}
