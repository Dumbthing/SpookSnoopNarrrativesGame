using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator anim;
    public bool dialogueOpen = false, isAnimating = false, playerChoosing;
    private Dialogue currentDialogue;
    public Button continueBtn, choice1, choice2, choice3;
    public event Action<Dialogue> endOfDialogueEvent; // Currently redundant event, but could be used to have the states update at end of dialogue instead of at start

    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        dialogueOpen = true;
        anim.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        if (dialogue.CurrentConversation > dialogue.conversations.Length - 1)
        {
            dialogue.CurrentConversation = dialogue.CurrentConversation - 1; // Ensures dialogue has not gone out of bounds
        }

        foreach (string sentence in dialogue.conversations[dialogue.CurrentConversation].sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    { 
        if (!isAnimating && !playerChoosing)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            StopAllCoroutines();    // In case of current active sentence being animated, while new sentence is requested
            StartCoroutine(SentenceAnim(sentence));

            /// add choices at the last sentence
            if (currentDialogue.conversations[currentDialogue.CurrentConversation].playerChoices.Length > 0 && sentences.Count == 0) // there are player choices for this sentence  
            {
                playerChoosing = true;
                if (currentDialogue.name == "Watchman Baxter" && currentDialogue.CurrentConversation == 2)
                {
                    if (StoryState.ProstituteMollyIsSuspicious)
                    {
                        choice1.GetComponentInChildren<Text>().text = currentDialogue.conversations[currentDialogue.CurrentConversation].playerChoices[0];
                        choice1.gameObject.SetActive(true);
                    }
                    if (StoryState.DoctorGradyIsSuspicious)
                    {
                        choice2.GetComponentInChildren<Text>().text = currentDialogue.conversations[currentDialogue.CurrentConversation].playerChoices[1];
                        choice2.gameObject.SetActive(true);
                    }
                    if (StoryState.MaidEllaIsSuspicious)
                    {
                        choice3.GetComponentInChildren<Text>().text = currentDialogue.conversations[currentDialogue.CurrentConversation].playerChoices[2];
                        choice3.gameObject.SetActive(true);
                    }
                    continueBtn.gameObject.SetActive(false);
                }
                else
                {
                    choice1.GetComponentInChildren<Text>().text = currentDialogue.conversations[currentDialogue.CurrentConversation].playerChoices[0];
                    choice1.gameObject.SetActive(true);
                    choice2.GetComponentInChildren<Text>().text = currentDialogue.conversations[currentDialogue.CurrentConversation].playerChoices[1];
                    choice2.gameObject.SetActive(true);
                    if (currentDialogue.conversations[currentDialogue.CurrentConversation].playerChoices.Length > 2)
                    {
                        choice3.GetComponentInChildren<Text>().text = currentDialogue.conversations[currentDialogue.CurrentConversation].playerChoices[2];
                        choice3.gameObject.SetActive(true);
                    }
                    continueBtn.gameObject.SetActive(false);
                }
            }
        }
    }

    public IEnumerator SentenceAnim(string sentence)
    {
        isAnimating = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        isAnimating = false;
    }

    public void EndDialogue()
    {
        if (dialogueOpen && sentences.Count == 0 && !playerChoosing)
        {
            endOfDialogueEvent(currentDialogue);
        }
        if (playerChoosing)
            ChoiceMade(false);
        anim.SetBool("IsOpen", false);
        dialogueOpen = false;
        currentDialogue = null;
    }

    public void ChoiceMade(bool choiceMade)
    {
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        choice3.gameObject.SetActive(false);
        continueBtn.gameObject.SetActive(true);
        playerChoosing = false;
        if (choiceMade)
        {
            EndDialogue();
        }
    }


}
