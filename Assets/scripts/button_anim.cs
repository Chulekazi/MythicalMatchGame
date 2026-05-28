using UnityEngine;
using UnityEngine.UI;

public class button_anim : MonoBehaviour
{
    //checks for 10 second inactivity
    private Animator anim;
    private float idle_time = 0f;
    private bool has_clicked = false;
    public float reminder = 10f;
    void Awake()
    {
        anim = GetComponent<Animator>();

        Button button = GetComponent<Button>();
        if(button != null)
        {
            button.onClick.AddListener(OnButtonClicked);
        }
    }

    void Update()
    {
        if (!has_clicked)
        {
            idle_time += Time.deltaTime;
            if (idle_time >= reminder)
            {
                Glow();
            }
        }
    }
    private void OnButtonClicked()
    {
        has_clicked = true;
        idle_time = 0f;
        Dim();
    }
    public void Glow()
    {
        anim.SetTrigger("color");
    }

    public void Dim()
    {
        anim.SetTrigger("idle");
    }
}
