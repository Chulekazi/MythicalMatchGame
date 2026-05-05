using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class quiz01 : MonoBehaviour
{
    public DialogueManager manager01;
    public Sprite v_normal;
    public Sprite v_intrigued;
    public Sprite v_disappointed;
    

    void Start()
    {
        List<Dialogue> quiz1 = new()
        {
            new Dialogue
            {
                characterName ="Vikram",
                dialogueText = "<color=#DC143C>What would you like to drink?</color>",
                image = v_normal,
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice
                    {
                        quizAnswer = "Water",
                        heartpoints = 0,
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Vikram",
                                dialogueText = "Not very interesting, are we?",
                                image = v_normal
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "Wine",
                        heartpoints=0,
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName= "Vikram",
                                dialogueText = "Ah, my family makes the best wine. I’m personally not a fan though… ",
                                image = v_intrigued
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "Fruit juice",
                        heartpoints = 0,
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Vikram",
                                dialogueText = "...Interesting choice... I'll see what the servants can find.",
                                image=v_disappointed
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "Chai",
                        heartpoints = 1,
                        journal_entry = "Vikram liked your choice of Chai. You gained a heart point!",
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Vikram",
                                dialogueText = "You have fantastic taste. Let me get two cups for us then.",
                                image = v_intrigued,
                                
                            }
                        }
                    }
                }
            }
        }; //last bracket
        manager01.BeginDialogue(quiz1);
    }
}
