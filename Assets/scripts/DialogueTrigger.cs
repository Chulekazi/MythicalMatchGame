using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;
    public Sprite timerImage;

    void Start()
    {
        List<Dialogue> dialogues = new()
        {
            new Dialogue{
                characterName = PlayerData.playerName,
                dialogueText ="<i>Forty seconds. That's all I've managed to achieve. Forty seconds.</i>"
            },
            new Dialogue { dialogueText = " " },
            new Dialogue{
                characterName = PlayerData.playerName,
                dialogueText = "<i>Maybe this isn't worth it after all...</i>"
            },
            new Dialogue { dialogueText = " " },
            new Dialogue{
                characterName = "???",
                dialogueText = "Fret not mortal, for I will grant you the means necessary to continue your research."
            },
            new Dialogue{
                characterName = PlayerData.playerName,
                dialogueText = "Huh? Great, now I'm ALSO going crazy."
            },
            new Dialogue{
                characterName = "Time",
                dialogueText="Call me Time, mortal one. I possess the power to allow you to study the creatures that you are so fascinated with.",
                image = timerImage
            },
            new Dialogue{
                characterName = PlayerData.playerName,
                dialogueText ="But why? What do you gain from this? Oh my god I <i>am</i> going crazy."
            },
            new Dialogue{
                characterName = "Time",
                dialogueText = "I find it amusing. Now, would you like the opportunity to meet the creatures you have dedicated your life to researching?",
                image = timerImage,
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice{
                        quizAnswer = "You can do that?",
                        nextLine = new List<Dialogue>{
                            new Dialogue{ characterName = "Time", dialogueText = "Splendid. Now, that device you have been working on. How does it work?", image = timerImage }
                        }
                    },
                    new DialogueChoice{
                        quizAnswer = "No. I'm terrified. Who are you and why are you in my house?",
                        nextLine = new List<Dialogue>{
                            new Dialogue{ characterName = "Time", dialogueText = "Splendid. Now, that device you have been working on. How does it work?", image = timerImage }
                        }
                    },
                    new DialogueChoice{
                        quizAnswer = "Uhh okay.",
                        nextLine = new List<Dialogue>{
                            new Dialogue{ characterName = "Time", dialogueText = "Splendid. Now, that device you have been working on. How does it work?", image = timerImage }
                        }
                    }
                }
            },
            new Dialogue {
                characterName = PlayerData.playerName,
                dialogueText = "I, uh, I can rewind time back about 40 seconds but that's it. This is all I've managed to achieve."
            }
        };

        manager.BeginDialogue(dialogues);
    }
}
