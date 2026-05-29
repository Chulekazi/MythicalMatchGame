using UnityEngine;

[CreateAssetMenu(fileName = "DialogueOption", menuName = "Dialogue/Option")]
public class dialogue_optionSO : ScriptableObject
{
    public string speakerName;
    public string option_;
    public string response_;
    public Dialogue_line next_line_;
    public bool Is_correct;
    public bool is_default;
    public Sprite journal_image;
}
