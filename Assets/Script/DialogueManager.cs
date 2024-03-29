﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private GameManager instance;
    void Start()
    {
        instance = GameManager.Instance;
        sentences = new Queue<string>();
    }

    internal void StartDialogue(Dialogue dialogue)
    {
        instance.Player.GetComponent<ControllerMovement>().enabled = false;

        animator.SetBool("IsOpen", true);
        Debug.Log("Starting conversation with " + dialogue.name);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return 1;
        }
    }

    private void EndDialogue()
    {
        instance.Player.GetComponent<ControllerMovement>().enabled = true;
        animator.SetBool("IsOpen", false);
        Debug.Log("End of conversation");
    }
}
