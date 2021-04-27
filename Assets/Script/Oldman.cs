using UnityEngine;

public class Oldman : MonoBehaviour, INPCBehaviour, IInterractable
{

    private GameManager instance;
    public Dialogue annoyed;




    public void Awake()
    {
        instance = GameManager.Instance;

    }

    //Pour l'interface IInterractable 
    public void Interact()
    {
        throw new System.NotImplementedException();
    }



    public void UpdateBehaviour()
    {

        if (instance.SavegameManager.saveData.spokeoldman == 0)
        {
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            instance.SavegameManager.saveData.spokeoldman++;
        }
        else if (instance.SavegameManager.saveData.spokeoldman == 3)
        {
            FindObjectOfType<DialogueTrigger>().DisplayNextSentence();
            instance.SavegameManager.saveData.spokeoldman++;
            instance.SavegameManager.saveData.hasSword = true;
            instance.SavegameManager.saveData.equipedWeapon = SaveData.EquipedWeapon.Sword;
            instance.UIManager.updateWeapon();
            instance.SoundManager.Play(SoundManager.Sfx.itemCatch);
        }
        else if (instance.SavegameManager.saveData.spokeoldman <= 4)
        {
            FindObjectOfType<DialogueTrigger>().DisplayNextSentence();
            instance.SavegameManager.saveData.spokeoldman++;
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
