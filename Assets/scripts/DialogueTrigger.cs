using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;
    public Sprite dateableSprite;
    public Sprite travelerSprite;

    void Start()
    {
        List<Dialogue> dialogues = new()
        {new Dialogue{
            characterName = "Vikram", dialogueText =" yeah man.", image = dateableSprite },
            new Dialogue {
                characterName = PlayerData.playerName, dialogueText = "yeah bro", image = travelerSprite },
        };

        manager.BeginDialogue(dialogues);
        }
}

