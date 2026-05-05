using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class PlayerData
{
    public static string playerName;
    private static int playerHeartPoints = 0;

    public static int PlayerHeartPoints { get => playerHeartPoints; set => playerHeartPoints = value; }
    public static HashSet<string> clicked_ = new HashSet<string> ();
}

