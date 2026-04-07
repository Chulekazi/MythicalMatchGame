using UnityEngine;
using UnityEngine.EventSystems;

public class BiggerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float size_difference = 1.05f;

    private Vector2 orignal_size;
    private RectTransform rect_transform;

    private void Awake()
    {
        rect_transform = GetComponent<RectTransform>();
        orignal_size = rect_transform.localScale;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        rect_transform.localScale = orignal_size * size_difference;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect_transform.localScale = orignal_size;
    }
}
