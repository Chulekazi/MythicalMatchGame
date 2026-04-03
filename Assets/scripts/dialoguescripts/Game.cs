using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
[System.Serializable]
public class Game
{
    public string char_name;
    [TextArea(2,3)]
    public string dialogue_text;
}

public class Dialogue_ : MonoBehaviour
{
    public List<Game> lines;
}