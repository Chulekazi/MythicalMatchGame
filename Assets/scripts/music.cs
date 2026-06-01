using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
   
    public AudioSource musicSource; 
    public List<AudioClip> musicClips; 
    public List<ParticleSystem> particleEffects;

    private Dictionary<string, AudioClip> dialogueToMusic;
    private Dictionary<string, ParticleSystem> dialogueToParticles;

    void Awake()
    {
        dialogueToMusic = new Dictionary<string, AudioClip>
        {
            { "I think I’m gonna be sick-", musicClips[0] },
            { "YOU’RE HEREEEEEEE!", musicClips[1] },
            { "Surely I’ll get used to this weird travelling method at some point right…eugh…", musicClips[2] },
            { "They rush me to get ready, and Vikram’s not even here yet. ", musicClips[3] },
            { "Hey! It’s soo great to see you again!!", musicClips[4] },
            { "I’ll give you a minute. I know how awful the travel can be. ", musicClips[5] },
            { "Oh, hello!", musicClips[6] },
            { "Sharp inhale", musicClips[7] },
            { "Wow, I’m actually not dizzy!", musicClips[8] }
        };
        dialogueToParticles = new Dictionary<string, ParticleSystem>
        { 
            { "I think I’m gonna be sick-", particleEffects[0] },
            { "YOU’RE HEREEEEEEE!", particleEffects[1] },
            { "Surely I’ll get used to this weird travelling method at some point right…eugh…", particleEffects[2] },
            { "They rush me to get ready, and Vikram’s not even here yet. ", particleEffects[3] },
            { "Hey! It’s soo great to see you again!!", particleEffects[4] },
            { "I’ll give you a minute. I know how awful the travel can be. ", particleEffects[5] },
            { "Oh, hello!", particleEffects[6] },
            { "Sharp inhale", particleEffects[7] },
            { "Wow, I’m actually not dizzy!", particleEffects[8] }
        };
    
    }
    public void CheckDialogue(string dialogueLine)
    {
        if (dialogueToMusic.ContainsKey(dialogueLine))
        {
            musicSource.clip = dialogueToMusic[dialogueLine];
            musicSource.Play();
        }

        if (dialogueToParticles.ContainsKey(dialogueLine))
        {
            dialogueToParticles[dialogueLine].Play();
        }
    }
}
