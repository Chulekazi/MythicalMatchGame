using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class quiz5 : MonoBehaviour
{
    public DialogueManager manager;
    public Sprite a_normal;
    public Sprite a_disappointed;

    void Start()
    {
        List<Dialogue> quiz5 = new()
        {
            new Dialogue
            {
                characterName = "Akira",
                dialogueText ="What makes it difficult to talk to them?",
                image = a_normal,
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice
                    {
                        quizAnswer ="I’m awkward.",
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Akira",
                                dialogueText = "Well, you have been talking to me with no issues so far, so I will have to take your word for it. ",
                                image = a_normal
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "They scare me.",
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Akira",
                                dialogueText = "Ah, I imagine they can be scary if you do not speak to them frequently.",
                                image = a_normal
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "They’re so interesting it makes me nervous. ",
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Akira",
                                dialogueText = " I…can understand that. I often find myself wondering what it would be like to interact with them on a regular basis.",
                                image =a_normal
                            }
                        }
                    },
                    new DialogueChoice
                    {
                        quizAnswer = "I don’t like them.",
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = "Akira",
                                dialogueText = "Oh, that is not…good…for you to not like those of your own kind.",
                                image = a_disappointed
                            }
                        }
                    }
                }
            }
        };
        manager.BeginDialogue(quiz5);
    }
}
