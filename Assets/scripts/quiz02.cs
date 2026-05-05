using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class quiz02 : MonoBehaviour
{
    public DialogueManager manager02;
    public Sprite v_normal01;
    public Sprite v_intrigued01;
    public Sprite v_disappointed01;
   

    void Start()
    {
        List<Dialogue> quiz2 = new() 
        { new Dialogue
        {
            characterName = "Vikram",
            dialogueText = "<color=#DC143C>What about you, journalist? Why the interest in mythical creatures such as myself?</color>",
            image = v_intrigued01,
            choices = new List<DialogueChoice>
            {
                new DialogueChoice
                {
                    quizAnswer = "I think they're interesting.",
                    heartpoints = 0,
                    nextLine = new List<Dialogue>
                    {
                        new Dialogue
                        {
                            characterName = "Vikram",
                            dialogueText = "I’m flattered, but at the end of the day we are just humans with some extra traits.",
                            image = v_intrigued01
                        }
                    }
                },
                new DialogueChoice
                {
                    quizAnswer = "I think they're beautiful.",
                    heartpoints =1,
                    journal_entry = "Vikram appreciated your honesty. You gained 1 heart point!",
                    nextLine = new List<Dialogue>
                    {
                        new Dialogue
                        {
                            characterName = "Vikram",
                            dialogueText = "That's oddly sweet. Here I was taking you for a crazed fan when you're really someone who appreciates us.",
                            image = v_intrigued01
                        }
                    }
                },
                new DialogueChoice
                {
                    quizAnswer = "I'm weirdly attracted to them.",
                    heartpoints = 0,
                    nextLine = new List<Dialogue>
                    {
                        new Dialogue
                        {
                            characterName = "Vikram",
                            dialogueText = "I'm...not sure what to say about that.",
                            image = v_disappointed01
                        }
                    }
                },
                new DialogueChoice
                {
                    quizAnswer = "I want to prove they exist.",
                    heartpoints=0,
                    nextLine = new List<Dialogue>
                    {
                        new Dialogue
                        {
                            characterName = "Vikram",
                            dialogueText = "I mean does it really matter if you do or don't? We've already been around",
                            image = v_normal01
                        }
                    }
                }
            }
        }
        }; //last bracket
        manager02.BeginDialogue(quiz2);
    }
}
