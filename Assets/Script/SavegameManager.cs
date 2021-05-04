using UnityEngine;

public class SavegameManager : MonoBehaviour
{
    public SaveData saveData;
    // Start is called before the first frame update
    void Awake()
    {
        saveData = new SaveData();
    }

    public void onDeath()
    {
        saveData = new SaveData();
    }



}
