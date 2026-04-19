using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "DialogueData")]
public class DialogueData : ScriptableObject
{
    [TextArea(2, 2)]
    public string[] dialoguestring;
}


[System.Serializable]
public class Game
{
    public string char_name;
    [TextArea(2,3)]
    public string dialogue_text;
    public List<DialogueOptions> options;
}

[System.Serializable]
public class DialogueOptions
{
    public string optionText;
    public int next_line;
}


public class Dialogue_ : MonoBehaviour
{
    public List<Game> lines;
}

