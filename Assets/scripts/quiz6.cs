using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class quiz6 : MonoBehaviour
{
    public DialogueManager manager;
    public Sprite a_normal;
    public Sprite a_disappointed;
    public Sprite a_happy;

    void Start()
    {
        List<Dialogue> quiz6 = new()
        {
            new Dialogue
            {
                characterName = "Akira",
                dialogueText ="What, uh. What do you think of the spot I chose for our…encounter?",
                image = a_normal,
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice
                    {
                        quizAnswer = "I like that it overlooks the settlement over there.",
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Akira",
                                dialogueText = "You noticed. I guess I like being able to watch people go about their day from a distance.",
                                image = a_happy
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "I think the spot is peaceful.",
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Akira",
                                dialogueText = "Definitely a good place to think. Sometimes being stuck in your own head too much is a problem, though.",
                                image = a_normal
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "I love cherry blossoms.",
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Akira",
                                dialogueText = "Yes, they are lovely this time in the year. It is a shame they do not bloom all year.",
                                image = a_normal
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "It’s nice.",
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Akira",
                                dialogueText = "…I see.",
                                image = a_disappointed
                            }
                        }
                    }
                }
            }
        };
        manager.BeginDialogue(quiz6);
    }
}
