using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class QuizQuestions : MonoBehaviour
{
    public DialogueManager DialogueManager;
    public Sprite image;

    void Start()
    {
        List<Dialogue> quiz = new()
    {
        new Dialogue
        {
            characterName = "Time",
            dialogueText = "<color=#DC143C>What's my favourite color?</color>",
            image = image,
            choices = new List<DialogueChoice>
            {
                new DialogueChoice
                {
                    quizAnswer = "Red",
                    nextLine = new List<Dialogue>
                    {
                        new Dialogue
                        {
                            characterName = PlayerData.playerName,
                            dialogueText = "No. Rewind time and try again.",
                            image = image
                        }
                    }
                },
                new DialogueChoice
                {
                    quizAnswer = "Gold",
                    nextLine = new List<Dialogue>
                    {
                        new Dialogue
                        {
                            characterName = PlayerData.playerName,
                            dialogueText = "That is not a colour, silly. Rewind time and try again.",
                            image = image
                        }
                    }
                },
                new DialogueChoice
                {
                    quizAnswer = "Green",
                    nextLine = new List<Dialogue>
                    {
                        new Dialogue
                        {
                            characterName = PlayerData.playerName,
                            dialogueText = "How did you know? Remember that you can rewind time to try for a different answer if you got the question wrong.",
                            
                            image = image
                        }
                        
                    }
                },
                new DialogueChoice
                {
                    quizAnswer = "That's a trick question.",
                    nextLine = new List<Dialogue>
                    {
                        new Dialogue
                        {
                            characterName = PlayerData.playerName,
                            dialogueText = "It wasn't, but you can rewind time and try again.",
                            image = image
                        }
                    }
                }
            }
        },
        new Dialogue
        {
            characterName = PlayerData.playerName,
            dialogueText = "This is great and all, but it only has enough charge to work once a day.",
            image = image
        },
        new Dialogue
        {
            characterName = "Time",
            dialogueText = "Then you better be wise when you use it.",
            image = image
        },
        new Dialogue
        {
            characterName = "Time",
            dialogueText = "Let us get moving. Your first date is waiting for you.",
            image = image
        },
        new Dialogue
        {
            characterName = PlayerData.playerName,
            dialogueText = "Wait what do you mean by da-",
            image = image
        }
    };

        DialogueManager.BeginDialogue(quiz);
    }

}









