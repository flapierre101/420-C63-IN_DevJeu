using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public enum Global
    {
        Consumable_BluePotion,
        Consumable_Heart,
        Consumable_RedPotion,
        Destructable_Barrel,
        Destructable_Bush,
        Destructable_MetalCrate,
        Destructable_WoodCrate,
        Goblin,
        MasterSlashLeft,
        MasterSlashRight,
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


}
