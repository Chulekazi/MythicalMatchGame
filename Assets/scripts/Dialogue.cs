using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

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
    public string dialogueID;
}

[System.Serializable]
public class Pronouns
{
    public string subject;
    public string obj;
}



