using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Animator anim;
    Dialogue currentDialogue;
    string currentNPCName;
    private GameObject currentNPC;
    public float walkingSpeed, sprintSpeed;
    public GameObject blackFade;
    private int sceneToLoad;
    private int lastDoctorConversation;

    private void Start()
    {
        if (walkingSpeed == 0.0f)
            walkingSpeed = 1.0f;
        if (sprintSpeed == 0.0f)
            sprintSpeed = 2.0f;

        FindObjectOfType<DialogueManager>().endOfDialogueEvent += AdvanceDialogueuWithoutChoice;
    }

    void Update()
    {
        anim = GetComponent<Animator>();
        Movement();
    }

    void Movement()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Magnitude", movement.magnitude);

        if (Input.GetKey(KeyCode.LeftShift))
            transform.position = transform.position + movement.normalized * sprintSpeed * Time.deltaTime;
        else
            transform.position = transform.position + movement.normalized * walkingSpeed * Time.deltaTime;

        float lastInputX = Input.GetAxis("Horizontal");
        float lastInputY = Input.GetAxis("Vertical");

        if (lastInputX != 0 || lastInputY != 0)
        {
            anim.SetBool("running", true);
            if (lastInputX > 0)
            {
                anim.SetFloat("lastMoveX", 1f);
            }
            else if (lastInputX < 0)
            {
                anim.SetFloat("lastMoveX", -1f);
            }
            else
            {
                anim.SetFloat("lastMoveX", 0f);
            }

            if (lastInputY > 0)
            {
                anim.SetFloat("lastMoveY", 1f);
            }
            else if (lastInputY < 0)
            {
                anim.SetFloat("lastMoveY", -1f);
            }
            else
            {
                anim.SetFloat("lastMoveY", 0f);
            }
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.CompareTag("NPC"))
        {
            trigger.GetComponent<SpriteOutline>().outlineSize = 1;
            InteractWithNPC(trigger);
        }
        else if (trigger.CompareTag("Item"))
        {
            trigger.GetComponent<SpriteOutline>().outlineSize = 1;
            InteractWithItem(trigger);
        }
        else if (trigger.CompareTag("Transition"))
            Transition(trigger);
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.CompareTag("Item") || trigger.CompareTag("NPC"))
        {
            trigger.GetComponent<SpriteOutline>().outlineSize = 0;
            if (trigger.CompareTag("NPC"))
                FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }

    void InteractWithNPC(Collider2D NPC)
    {
        if (Input.GetKeyDown(KeyCode.Space) && !FindObjectOfType<DialogueManager>().isAnimating)
        {
            if (FindObjectOfType<DialogueManager>().dialogueOpen == false)
            {
                NPC.GetComponent<NPC>().TriggerDialogue();
                currentNPC = NPC.gameObject;
                currentDialogue = currentNPC.GetComponent<NPC>().dialogue;
                currentNPCName = currentDialogue.name;
            }
            else
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        }
    }

    void InteractWithItem(Collider2D item)
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Destroy(item.gameObject);
            StoryState.FlowersCollected++;
        }
    }

    void Transition(Collider2D transition)
    {
        string toDestination = transition.name;
        if (toDestination.Contains("Office"))
            blackFade.GetComponent<SceneChange>().FadeToNextScene(0);
        else if (toDestination.Contains("Town"))
            blackFade.GetComponent<SceneChange>().FadeToNextScene(1);
        else if (toDestination.Contains("Graveyard"))
            blackFade.GetComponent<SceneChange>().FadeToNextScene(2);
        else if (toDestination.Contains("Forest"))
            blackFade.GetComponent<SceneChange>().FadeToNextScene(3);


    }

    public void Choice1Made()
    {
        currentDialogue = currentNPC.GetComponent<NPC>().dialogue;
        NPCChoices(currentDialogue.CurrentConversation, 1);

    }
    public void Choice2Made()
    {
        currentDialogue = currentNPC.GetComponent<NPC>().dialogue;
        NPCChoices(currentDialogue.CurrentConversation, 2);
    }
    public void Choice3Made()
    {
        currentDialogue = currentNPC.GetComponent<NPC>().dialogue;
        NPCChoices(currentDialogue.CurrentConversation, 3);
    }

    public void AdvanceDialogueuWithoutChoice(Dialogue dialogue) // State changes from end of conversation
    {

    }

    void NPCChoices(int convoNum, int choiceNum)
    {
        if (currentNPCName == "Bartender Dan")
        {
            if (convoNum == 1)
            {
                if (choiceNum == 1)
                    StoryState.Plot2FromGertrude = true;
                else if (choiceNum == 2)
                    StoryState.Plot2Anonymous = true;
            }
        }

        if (currentNPCName == "Doctor Grady") //Plot2Refused as this can only be accessed if Doctor found inside Gertrude's house.
        {
            if (convoNum == 0 && StoryState.Plot2Refused) {
                if(choiceNum == 1)
                    StoryState.Plot3AboutGertrude = true;
                else if (choiceNum == 2) {
                    StoryState.Plot3DoctorNoChoice = true;
                }
                    

            }
  
        }
         
        if(currentNPCName == "Prostitute Molly") {
            if(convoNum == 1) {
                if(choiceNum ==1)
                    StoryState.HelpYourself = true;

                else if(choiceNum ==2)
                    StoryState.CallNightwatch = true;
            }
        }

        if(currentNPCName == "Maid's House") {
            if(convoNum == 1) {
                if(choiceNum ==1)
                    StoryState.Plot2BlockDoor = true;

                if(choiceNum == 2)
                    StoryState.Plot2AskBlood = true;

                if(choiceNum == 3)
                    StoryState.Plot2Leave = true;
            }
        }

        if(currentNPCName == "Gertrude's House") {
            if(convoNum == 1) {
                if(choiceNum == 1)
                    StoryState.Plot2Run = true;

                if(choiceNum == 2)
                    StoryState.Plot2Hide = true;

                if(choiceNum == 3)
                    StoryState.Plot2ComeClean = true;

            }
        }
        if (currentNPCName == "Butcher's House")
        {
            if (convoNum == 1)
            {
                if (choiceNum == 1)
                    StoryState.Plot2BlockDoor = true;

                if (choiceNum == 2)
                    StoryState.Plot2AskBlood = true;

                if (choiceNum == 3)
                    StoryState.Plot2Leave = true;

            }
        }
        if (currentNPCName == "Watchman Baxter")
        {
            if(convoNum == 1)
            {
                if (choiceNum == 1)
                    StoryState.SuspectChosen = true;
                else if (choiceNum == 2)
                    StoryState.NotYet = true;
            }
            else if (convoNum == 2)
            {
                if (choiceNum == 1)
                    StoryState.ProstituteMollySelected = true;
                else if (choiceNum == 2)
                    StoryState.DoctorGradySelected = true;
                else if (choiceNum == 3)
                    StoryState.MaidEllaSelected = true;
            }
        }
    }
}
