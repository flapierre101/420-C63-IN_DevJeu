using UnityEngine;

public class Oldman : MonoBehaviour, INPCBehaviour, IInterractable
{

    private GameManager instance;
    public Dialogue annoyed;
    public Dialogue intro;




    public void Awake()
    {
        instance = GameManager.Instance;

    }





    public void Interact()
    {

        if (instance.SavegameManager.saveData.spokeoldman == 0)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(intro);
            instance.SavegameManager.saveData.spokeoldman++;
        }
        else if (instance.SavegameManager.saveData.spokeoldman == 3)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            instance.SavegameManager.saveData.spokeoldman++;
            instance.SavegameManager.saveData.hasSword = true;
            instance.SavegameManager.saveData.equipedWeapon = SaveData.EquipedWeapon.Sword;
            instance.UIManager.updateWeapon();
            instance.SoundManager.Play(SoundManager.Sfx.itemCatch);
        }
        else if (instance.SavegameManager.saveData.spokeoldman <= 4)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            instance.SavegameManager.saveData.spokeoldman++;
        }
        else if (instance.SavegameManager.saveData.spokeoldman == 5)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(annoyed);
            instance.SavegameManager.saveData.spokeoldman++;
        }
        else if (instance.SavegameManager.saveData.spokeoldman == 6)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            instance.SavegameManager.saveData.spokeoldman++;
        }
        else if (instance.SavegameManager.saveData.spokeoldman == 7)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            instance.SavegameManager.saveData.spokeoldman = 5;
        }
    }

    public void UpdateBehaviour()
    {
        throw new System.NotImplementedException();
    }
}
