using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    public string quizAnswer;
    public List<Dialogue> nextLine;
    public int heartpoints;
}

[System.Serializable]
public class Dialogue 
{
    public string characterName;
    public string dialogueText;
    public Sprite image;
    public List<DialogueChoice> choices;
}




