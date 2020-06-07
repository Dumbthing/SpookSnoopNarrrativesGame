using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StoryState
{
    /// Get/Set booleans to determine story states
    // Plot 1 choices
    public static bool CanTalkToHector{ get; set; }
    public static bool CanTalkToGertrude { get; set; }
    public static bool TalkToMolly { get; set; }


    public static bool HelpYourself{get; set;}

    public static bool CallNightwatch{get; set;}

    public static bool DoctorIn{get; set; }
    public static bool DoctorWasInGertrudesHouse { get; set; }
    // Use both True and False if he's in or out. 

    public static bool EscapeNightWatchman{get; set;}
    // Complete either plot 1 choice to get this true.

    // Plot 2 Choices
    public static bool GertrudeClosure{get; set;}
    public static bool GertrudeCase{get; set;}
    public static bool GertrudeRumors{get; set;}

    // If !Doctorin
    public static bool Plot2Run{get; set;}
    public static bool Plot2Hide{get; set;}
    public static bool Plot2ComeClean{get; set;}

    // if Plot2Hide || Plot2ComeClean
    public static bool Plot2Refused{get; set;}
    public static bool Plot2HectorKnowsAboutThePackage{ get; set; }
    public static bool Plot2PackageForButcher { get; set; }
    public static bool Plot2ButcherPackageDelivered { get; set; }
    public static bool Plot2DoctorPackageDelivered { get; set; }


    // if Plot2Run
    public static bool Plot2FromGertrude{get; set;}
    public static bool Plot2Anonymous{get; set;}
    public static bool Plot2Delivered{get; set;}
    public static bool Plot2VisitProstitute{get; set;}

    // if Plot2VisitProstitute
    public static bool Plot2BlockDoor{get; set;}
    public static bool Plot2AskBlood{get; set;}
    public static bool Plot2Leave{get; set;}

    // Plot 3 choices
    public static bool Plot3Doctor{get; set;}

    // if Plot2Refused
    public static bool Plot3Butcher{get; set;}

    public static bool Plot3AboutGertrude{get; set;}
    public static bool Plot3DoctorNoChoice{get; set;}

    public static bool Plot3AnyNPCPackage{get; set;}

    public static bool Plot2ButcherPackage{get; set;}
    // In story you go to Maid's house but Butcher answers, we can just make it so if you talk to the Butcher, that triggers. 
    
    public static bool Plot3ButcherMaid{get; set;}
    // If Plot2VisitProstitute
    public static bool Plot3ButcherProstitute{get; set;}
    
    public static bool Plot3ButcherDoctor{get; set;}

    public static bool Plot3BartenderMaid{get; set;}
    public static bool Plot3BartenderDoctor{get; set;}
    // If Plot2VisitProstitute
    public static bool Plot3BartenderProstitute{get; set;}

    public static int FlowersCollected { get; set; }

    public static bool ProstituteMollyIsSuspicious { get; set; }
    public static bool DoctorGradyIsSuspicious { get; set; }
    public static bool MaidEllaIsSuspicious { get; set; }

    public static bool SuspectChosen { get; set; }
    public static bool NotYet { get; set; }

    public static bool ProstituteMollySelected { get; set; }
    public static bool DoctorGradySelected { get; set; }
    public static bool MaidEllaSelected { get; set; }
    // If !Plot2VisitProstitute
    public static bool EndingNeutralDoctor { get; set;}
    // If Plot2VisitProstitute && Maid
    public static bool EndingGoodMaid{get; set;}
    // If !Maid
    public static bool EndingBadProstitute{get; set; }

}