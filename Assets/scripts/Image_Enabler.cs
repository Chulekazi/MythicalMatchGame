using UnityEngine;
using UnityEngine.UI;

public class Image_Enabler : MonoBehaviour
{
    public Image image_;
    public int activate;
    public int deactivate = -1;

    public void CheckLineIndex(int current_lineindex)
    {
        if(current_lineindex == activate)
        {
            image_.gameObject.SetActive(true);
        }
        else if(deactivate != -1 && current_lineindex == deactivate)
        {
            image_.gameObject.SetActive(false);
        }
    }

}
