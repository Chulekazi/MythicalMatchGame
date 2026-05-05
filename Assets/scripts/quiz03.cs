using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quiz03 : MonoBehaviour
{
    public separateManager manager01;
    public Sprite c_normal;
    public Sprite c_intrigued;
    public Sprite c_disappointed;
    public Sprite c_surprised;
    public AudioClip audio1;
    

    void Start()
    {
        List<Dialogue> quiz3 = new()
        {
            new Dialogue
            {
                characterName = "Chryseis",
                dialogueText ="How about you choose a topic for us?",
                image = c_normal,
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice
                    {
                        quizAnswer = "Your family",
                        heartpoints = 0,
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Chryseis",
                                dialogueText = "Okay, so, I have sooo many siblings, but we all have the same mom and dad! I’m the youngest out of all of us so I get to just run around and do whatever I like all day.",
                                image= c_intrigued,

                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "Your hobbies",
                        heartpoints=0,
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Chryseis",
                                dialogueText = "I mean, there’s so much to do down here! I like to go up to the surface and watch the humans though. I have a little box of trinkets somewhere in here.",
                                image = c_intrigued
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "Where am I?",
                        heartpoints =0,
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Chryseis",
                                dialogueText = "You’re in my room in my family’s palace.",
                                image = c_normal
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "No you choose",
                        heartpoints = 1,
                        sound = audio1,
                        journal_entry = "Chryseis liked that. You gained a heart point!",
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Chryseis",
                                dialogueText = "Oh yay! I love choosing!",
                                image = c_intrigued
                            }
                        }
                    }
                }
            }
        };
        manager01.BeginDialogue(quiz3);
    }
}
