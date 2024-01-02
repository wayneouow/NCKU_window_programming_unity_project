using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class font_color : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Color startColor = Color.red;
    public Color endColor = Color.blue;
    public float duration = 2f;

    private float elapsedTime = 0f;
    private bool isForward = true; // 用來標記當前漸變方向
    void Update()
    {
        // 增加已經過的時間
        elapsedTime += Time.deltaTime;

        // 計算插值權重，介於0到1之間
        float t = Mathf.Clamp01(elapsedTime / duration);

        // 如果達到指定的持續時間，切換漸變方向
        if (elapsedTime >= duration)
        {
            elapsedTime = 0f;
            isForward = !isForward; // 切換漸變方向
        }

        // 根據漸變方向設定插值權重
        if (isForward)
        {
            t = Mathf.Lerp(0f, 1f, t);
        }
        else
        {
            t = Mathf.Lerp(1f, 0f, t);
        }
    }
}