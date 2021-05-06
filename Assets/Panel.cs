using UnityEngine;

public class Panel : MonoBehaviour, IInterractable
{
    private bool interacted;
    public Dialogue panelText;
    public void Interact()
    {
        if (!interacted)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(panelText);
            interacted = true;
        }
        else
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            interacted = false;
        }
    }



}
