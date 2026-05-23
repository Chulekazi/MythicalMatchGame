using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLine", menuName = "Dialogue/Line")]
public class Dialogue_line : ScriptableObject
{
    public string npc_name = "player";
    [TextArea(2, 5)]
    public string npc_text;
    public dialogue_optionSO[] optionSOs;
    public Dialogue_line Next_Line;
    public Background_changeSO background;
    public Sprite speaker_;
}
