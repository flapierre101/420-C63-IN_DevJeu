using UnityEngine;

public class Oldman : MonoBehaviour, INPCBehaviour, IInterractable
{

    private GameManager instance;



    void Update()
    {

    }


    public void Awake()
    {
        instance = GameManager.Instance;
    }

    public void Interact()
    {
        if (instance.SavegameManager.saveData.spokeoldman == 0)
        {
            // dire son premier texte
            instance.SavegameManager.saveData.spokeoldman++;
        }
        else if (instance.SavegameManager.saveData.spokeoldman == 1)
        {
            // dire 2e phrase
            instance.SavegameManager.saveData.spokeoldman++;
        }
        else
        {
            // dire la dernière phrase à répétition.
        }

    }

    public void UpdateBehaviour()
    {
        throw new System.NotImplementedException();
    }
}
