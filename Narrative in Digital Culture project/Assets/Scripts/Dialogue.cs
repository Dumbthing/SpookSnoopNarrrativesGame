using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{   
    public string name;
    public Conversations[] conversations;
    public int CurrentConversation { get; set; }
}
