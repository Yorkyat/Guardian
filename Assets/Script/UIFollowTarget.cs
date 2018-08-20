using UnityEngine;

public class UIFollowTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public RectTransform targetCanvas;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        targetCanvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(target.position + offset);
        Vector2 worldObjectScreenPosition = new Vector2(
        ((viewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * 0.5f)),
        ((viewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * 0.5f)));
        //now can set the position of the ui element
        rectTransform.anchoredPosition = worldObjectScreenPosition;
    }
}