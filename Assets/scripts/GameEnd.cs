using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
   public void Good_Akira()
    {
        SceneManager.LoadScene("GoodEndingAkira");
    }

    public void Good_Vikram()
    {
        SceneManager.LoadScene("GoodEndingVikram");
    }

    public void Good_Chryseis()
    {
        SceneManager.LoadScene("GoodEndingChryseis");
    }
}
