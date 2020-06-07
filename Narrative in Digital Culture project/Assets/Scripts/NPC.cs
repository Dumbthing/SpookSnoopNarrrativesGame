using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    private int lastDoctorConversation;

    private void Update()
    {
        if (dialogue.name == "Prostitute Molly" && StoryState.Plot2VisitProstitute)
            gameObject.SetActive(false);
    }

    public void TriggerDialogue()
    {
        UpdateConversation(dialogue);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void UpdateConversation(Dialogue dialogue)
    {
        if (dialogue.name == "Salesman Hector (Ghost)")
        {
            if (dialogue.CurrentConversation == 0 && StoryState.CanTalkToHector)
            {
                dialogue.CurrentConversation = 1;
            }
            if (dialogue.CurrentConversation == 1)
            {
                StoryState.DoctorGradyIsSuspicious = true;
                StoryState.CanTalkToGertrude = true;
                if (StoryState.Plot2HectorKnowsAboutThePackage)
                {
                    dialogue.CurrentConversation = 2;
                    StoryState.Plot2PackageForButcher = true;
                }
            }
        }
        else if (dialogue.name == "Gertrude (Ghost)")
        {
            if (dialogue.CurrentConversation == 0 && StoryState.CanTalkToGertrude)
            {
                dialogue.CurrentConversation = 1;
                StoryState.TalkToMolly = true;
            }
            else if (dialogue.CurrentConversation == 1)
            {
                Debug.Log("Story State for plot 2 is accepted: " + StoryState.Plot2Delivered);
                if (StoryState.Plot2Delivered)
                {
                    dialogue.CurrentConversation = 2;
                    StoryState.Plot2VisitProstitute = true;
                }
                else if (StoryState.Plot2Refused)
                {
                    dialogue.CurrentConversation = 3;
                    StoryState.Plot2HectorKnowsAboutThePackage = true;
                }
            }
        }
        else if (dialogue.name == "Bartender Dan")
        {
            if (dialogue.CurrentConversation == 0)
            {
                StoryState.CanTalkToHector = true;
                if (StoryState.Plot2Run)
                    dialogue.CurrentConversation = 1;
            }
            else if (dialogue.CurrentConversation == 1)
            {
                Debug.Log("Story state: Plot2fromgertrude " + StoryState.Plot2FromGertrude);
                if (StoryState.Plot2FromGertrude)
                {
                    dialogue.CurrentConversation = 2;
                    StoryState.Plot2Delivered = true;
                    Debug.Log("Story state: Plot2Delivered " + StoryState.Plot2Delivered);
                }
                else if (StoryState.Plot2Anonymous)
                {
                    dialogue.CurrentConversation = 3;
                    StoryState.Plot2Refused = true;
                }
            }
            else if (dialogue.CurrentConversation == 2 || dialogue.CurrentConversation == 3)
            {
                if (StoryState.Plot2Hide)
                    dialogue.CurrentConversation = 4;
                else if (StoryState.Plot2ComeClean)
                    dialogue.CurrentConversation = 5;
            }
        }
        else if (dialogue.name == "Doctor Grady")
        {
            if (dialogue.CurrentConversation == 4)
                dialogue.CurrentConversation = lastDoctorConversation;
            else if (StoryState.Plot2ButcherPackage && !StoryState.Plot2DoctorPackageDelivered)
            {
                dialogue.CurrentConversation = 4;
                StoryState.Plot2DoctorPackageDelivered = true;
            }

            if (dialogue.CurrentConversation == 0 && StoryState.DoctorWasInGertrudesHouse)
            {
                dialogue.CurrentConversation = 1;
                lastDoctorConversation = dialogue.CurrentConversation;
            }
            else if (dialogue.CurrentConversation == 1)
            {
                if (StoryState.Plot3AboutGertrude)
                    dialogue.CurrentConversation = 2;
                else if (StoryState.Plot3DoctorNoChoice)
                    dialogue.CurrentConversation = 3;
                lastDoctorConversation = dialogue.CurrentConversation;
            }
            if (dialogue.CurrentConversation != 4 && StoryState.FlowersCollected >= 5)
            {
                dialogue.CurrentConversation = 5;
                StoryState.ProstituteMollyIsSuspicious = true;
                lastDoctorConversation  = dialogue.CurrentConversation;
            }
        }
        else if (dialogue.name == "Butcher Henry")
        {
            if (dialogue.CurrentConversation == 0 && StoryState.Plot2PackageForButcher)
            {
                StoryState.Plot2ButcherPackage = true;
                dialogue.CurrentConversation = 1;
            }
            else if (dialogue.CurrentConversation == 1 && StoryState.Plot2ButcherPackageDelivered)
                dialogue.CurrentConversation = 2;
        }
        else if (dialogue.name == "Prostitute Molly")
        {
            if (dialogue.CurrentConversation == 0)
            {
                if (StoryState.TalkToMolly)
                    dialogue.CurrentConversation = 1;
            }
            else if (dialogue.CurrentConversation == 1)
            {
                if (StoryState.HelpYourself)
                {
                    dialogue.CurrentConversation = 2;
                    StoryState.EscapeNightWatchman = true;
                }
                else if (StoryState.CallNightwatch)
                {
                    dialogue.CurrentConversation = 3;
                    StoryState.DoctorIn = true;
                    StoryState.EscapeNightWatchman = true;
                }
            }
        }
        else if (dialogue.name == "Watchman Baxter")
        {
            if (dialogue.CurrentConversation == 0)
            {
                if (StoryState.ProstituteMollyIsSuspicious || StoryState.DoctorGradyIsSuspicious || StoryState.MaidEllaIsSuspicious)
                {
                    dialogue.CurrentConversation = 1;
                }
            }
            else if (dialogue.CurrentConversation == 1)
            {
                if (StoryState.SuspectChosen)
                    dialogue.CurrentConversation = 2;
            }
            else if (dialogue.CurrentConversation == 2)
            {
                if (StoryState.ProstituteMollySelected)
                    dialogue.CurrentConversation = 3;
                else if (StoryState.DoctorGradySelected)
                    dialogue.CurrentConversation = 4;
                else if (StoryState.MaidEllaSelected)
                    dialogue.CurrentConversation = 5;
            }

        }
        else if (dialogue.name == "Butcher's House")
        {
            if (dialogue.CurrentConversation == 0 && StoryState.Plot2VisitProstitute)
            {
                dialogue.CurrentConversation = 1;
            }
            else if (dialogue.CurrentConversation == 1)
            {
                if (StoryState.Plot2BlockDoor)
                    dialogue.CurrentConversation = 2;
                else if (StoryState.Plot2AskBlood)
                    dialogue.CurrentConversation = 3;
                else if (StoryState.Plot2Leave)
                    dialogue.CurrentConversation = 4;
            }
            else if (dialogue.CurrentConversation == 2 || dialogue.CurrentConversation == 3 || dialogue.CurrentConversation == 4)
            {
                dialogue.CurrentConversation = 0;
                StoryState.Plot2VisitProstitute = false;
            }
        }
        else if (dialogue.name == "Gertrude's House")
        {
            if (dialogue.CurrentConversation == 0 && StoryState.EscapeNightWatchman)
            {
                if (!StoryState.DoctorIn)
                    dialogue.CurrentConversation = 1;
                else if (StoryState.DoctorIn)
                    dialogue.CurrentConversation = 5;
            }
            else if (dialogue.CurrentConversation == 1)
            {
                if (StoryState.Plot2Run)
                    dialogue.CurrentConversation = 2;
                else if (StoryState.Plot2Hide)
                    dialogue.CurrentConversation = 3;
                else if (StoryState.Plot2ComeClean)
                    dialogue.CurrentConversation = 4;
            }
            else if (dialogue.CurrentConversation > 1 && dialogue.CurrentConversation != 6)
                dialogue.CurrentConversation = 6;
        }
        else if (dialogue.name == "Wife (Ghost)")
        {
            if (dialogue.CurrentConversation == 0)
            {
                if (StoryState.MaidEllaSelected)
                    dialogue.CurrentConversation = 1;
                else if (StoryState.DoctorGradySelected)
                    dialogue.CurrentConversation = 2;
                else if (StoryState.ProstituteMollySelected)
                    dialogue.CurrentConversation = 3;
            }
        }
    }
}
