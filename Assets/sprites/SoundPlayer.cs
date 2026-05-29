using UnityEngine;
[CreateAssetMenu(fileName = "NewNPCMusic", menuName = "Sound/NPCMusic")]
public class SoundPlayer : ScriptableObject
{
    public string npcName;
    public AudioClip song;
}