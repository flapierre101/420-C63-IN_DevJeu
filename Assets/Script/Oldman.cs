using UnityEngine;

public class Oldman : MonoBehaviour, INPCBehaviour, IInterractable
{

    private GameManager instance;
    private DialogueTrigger dialogueTrigger;
    public Dialogue annoyed;


    void Update()
    {

    }


    public void Awake()
    {
        instance = GameManager.Instance;

    }

    public void Interact()
    {





    }

    public void UpdateBehaviour()
    {

        if (instance.SavegameManager.saveData.spokeoldman == 0)
        {
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            instance.SavegameManager.saveData.spokeoldman++;
        }
        else if (instance.SavegameManager.saveData.spokeoldman <= 4)
        {
            FindObjectOfType<DialogueTrigger>().DisplayNextSentence();
            instance.SavegameManager.saveData.spokeoldman++;
            instance.SavegameManager.saveData.hasSword = true;
        }
        else if (instance.SavegameManager.saveData.spokeoldman == 5)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(annoyed);
            instance.SavegameManager.saveData.spokeoldman++;

        }
        else if (instance.SavegameManager.saveData.spokeoldman == 6)
        {
            FindObjectOfType<DialogueTrigger>().DisplayNextSentence();
            instance.SavegameManager.saveData.spokeoldman++;
        }
        else if (instance.SavegameManager.saveData.spokeoldman == 7)
        {
            FindObjectOfType<DialogueTrigger>().DisplayNextSentence();
            instance.SavegameManager.saveData.spokeoldman = 5;
        }
    }

}
