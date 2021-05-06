using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public enum Global
    {
        Bomb,
        BombExplosion,
        Consumable_BluePotion,
        Consumable_Heart,
        Consumable_RedPotion,
        Destructable_Barrel,
        Destructable_Bush,
        Destructable_MetalCrate,
        Destructable_WoodCrate,
        Fireball,
        Frostbolt,
        Goblin,
        MasterSlashDown,
        MasterSlashLeft,
        MasterSlashRight,
        MasterSlashUp,
        OldMan,
        Player,
        SlashDown,
        SlashLeft,
        SlashRight,
        SlashUp,

        Count
    }



    public enum Vfx
    {


        Count
    }



    public GameObject[] GlobalPrefabs;

    public GameObject[] VfxPrefabs;



    public void Awake()
    {
        // https://docs.unity3d.com/ScriptReference/Resources.html

        GlobalPrefabs = Resources.LoadAll<GameObject>("prefabs/global");
        Debug.Assert((int)Global.Count == GlobalPrefabs.Length, "Prefabmanager : Global enum length (" + (int)Global.Count + ") does not match Resources folder (" + GlobalPrefabs.Length + ")");


        VfxPrefabs = Resources.LoadAll<GameObject>("prefabs/vfx");
        Debug.Assert((int)Vfx.Count == VfxPrefabs.Length, "Prefabmanager : Vfx enum length " + (int)Vfx.Count + ") does not match Resources folder (" + VfxPrefabs.Length + ")");

        // https://docs.unity3d.com/ScriptReference/GameObject.AddComponent.html

    }

    public GameObject Instanciate(Global prefab, Transform parent)
    {
        return Instantiate(GlobalPrefabs[(int)prefab], parent);
    }

    public GameObject Instanciate(Global prefab, Vector3 position, Quaternion rotation)
    {
        return Instantiate(GlobalPrefabs[(int)prefab], position, rotation);
    }

    public GameObject Spawn(Global prefab, Vector3 position)
    {
        return Instantiate(GlobalPrefabs[(int)prefab], position, Quaternion.identity);
    }

    public GameObject Instanciate(Global prefab, Vector3 position)
    {
        return Instantiate(GlobalPrefabs[(int)prefab], position, Quaternion.identity);
    }

    public void ItemDrop(GameObject gameObject)
    {
        var itemSpawn = Random.Range(1, 101);

        switch (itemSpawn)
        {
            case int n when n >= 1 && n <= 25:
                Instanciate(Global.Consumable_BluePotion, gameObject.transform.position, gameObject.transform.rotation);
                break;
            case int n when n >= 26 && n <= 50:
                Instanciate(Global.Consumable_Heart, gameObject.transform.position, gameObject.transform.rotation);
                break;
            case int n when n >= 51 && n <= 75:
                Instanciate(Global.Consumable_RedPotion, gameObject.transform.position, gameObject.transform.rotation);
                break;
            default:
                break;
        }
    }
}
