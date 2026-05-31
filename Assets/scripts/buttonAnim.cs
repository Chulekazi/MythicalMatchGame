using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class buttonAnim : MonoBehaviour
{
    public Animator anim;
    public string name_state = "pulse";

    public void PlayAnim()
    {
        anim.Play(name_state);
    }
}
