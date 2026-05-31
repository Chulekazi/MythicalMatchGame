using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class quiz4 : MonoBehaviour
{
    public onemoreManager manager;
    public Sprite c_normal;
    public Sprite c_intrigued;
    public Sprite c_disappointed;
    public Sprite c_surprised;


    void Start()
    {
        List<Dialogue> quiz4 = new()
        {
            new Dialogue
            {
                characterName = "Chryseis",
                dialogueText ="What do you think my intentions are?",
                image = c_normal,
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice
                    {
                        quizAnswer = "You’re going to eat me.",
                        heartpoints = 0,
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Chryseis",
                                dialogueText = "I told you I’m not gonna eat you! Do you not trust me? [sad]",
                                image = c_disappointed
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "Uh, date me?",
                        heartpoints = 1,
                        journal_entry ="Chryseis fancies your response. You gained a heart point!",
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Chryseis",
                                dialogueText = "Mmmmaybe~",
                                image = c_intrigued
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "You just want a friend?",
                        heartpoints=0,
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Chryseis",
                                dialogueText = "Oh…yeah…friends…",
                                image =c_disappointed
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "You’re gonna kill me once you’re bored of me?",
                        heartpoints =0,
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Chryseis",
                                dialogueText = "I don’t even know what to say to that…that’s so mean…",
                                image = c_disappointed
                            }
                        }
                    }
                }
            }
        };
        manager.BeginDialogue(quiz4);
    }
}
