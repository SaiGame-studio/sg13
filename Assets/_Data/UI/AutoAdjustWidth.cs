using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class AutoAdjustWidth : MonoBehaviour
{
    public float maxWidth = 300f;
    public float minWidth = 0f; 

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        AdjustWidth();
    }

    void Update()
    {
        AdjustWidth(); 
    }

    void AdjustWidth()
    {
        float screenWidth = Screen.width;

        float newWidth = Mathf.Clamp(screenWidth, minWidth, maxWidth);

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
    }
}
