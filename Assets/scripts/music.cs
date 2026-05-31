using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
   
    public AudioSource musicSource; // Assign in Inspector
    public List<AudioClip> musicClips; // Drag 9 clips here in Inspector

    private Dictionary<string, AudioClip> dialogueToMusic;

    void Awake()
    {
        // Initialize dictionary with your dialogue lines
        dialogueToMusic = new Dictionary<string, AudioClip>
        {
            { "I think I’m gonna be sick-", musicClips[0] },
            { "YOU’RE HEREEEEEEE!", musicClips[1] },
            { "Surely I’ll get used to this weird travelling method at some point right…eugh…", musicClips[2] },
            { "They rush me to get ready, and Vikram’s not even here yet. ", musicClips[3] },
            { "Hey! It’s soo great to see you again!!", musicClips[4] },
            { "I’ll give you a minute. I know how awful the travel can be. ", musicClips[5] },
            { "Oh, hello!", musicClips[6] },
            { "Okay, I’m not as dizzy as I normally am. That’s good.", musicClips[7] },
            { "Wow, I’m actually not dizzy!", musicClips[8] }
        };
    }
    public void CheckDialogue(string dialogueLine)
    {
        if (dialogueToMusic.ContainsKey(dialogueLine))
        {
            musicSource.clip = dialogueToMusic[dialogueLine];
            musicSource.Play();
        }
    }
}
