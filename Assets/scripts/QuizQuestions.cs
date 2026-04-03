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
                characterName = "Vikram Daruwala",
                dialogueText = "<color=#FF0000>I really like cake nowadays. How about you?</color>",
                image = image,
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice
                    {
                        quizAnswer = "Cake sounds good. Do you have some?",
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = PlayerData.playerName, //this isn't right lol. just ignore it for now becaause the script miraculously works.
                                dialogueText = "...No.",
                                image = image
                            }
                        }
                    },

                    new DialogueChoice
                    {
                        quizAnswer = "I'm allergic.",
                        nextLine = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                characterName = PlayerData.playerName,
                                dialogueText = "...Well, this is awkward.",
                                image = image
                            }
                        }
                    }
                }
            }
        };
        DialogueManager.BeginDialogue( quiz );
     }
}
        








